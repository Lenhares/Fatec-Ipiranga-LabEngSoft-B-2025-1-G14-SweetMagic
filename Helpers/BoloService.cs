using Microsoft.EntityFrameworkCore;
using SweetMagic.Models;
using SweetMagic.Data;

namespace SweetMagic.Helpers {
    public class BoloService {
        private readonly AppDbContext _dbContext; // Injeção do DbContext
        public Bolo BoloAtual { get; set; } = new Bolo();
        public event Action OnBoloChanged;
        
        public BoloService(AppDbContext dbContext) // Construtor para receber o DbContext
        {
            _dbContext = dbContext;
        }
        public void AtualizarBolo(Bolo novoBolo) {
            BoloAtual = novoBolo;
            NotifyStateChanged();
        }
        public void AtualizarPropriedadeBolo<T>(string propertyName, T value) {
            var propertyInfo = BoloAtual.GetType().GetProperty(propertyName);
            if (propertyInfo != null && propertyInfo.PropertyType == typeof(T)) {
                propertyInfo.SetValue(BoloAtual, value);
                NotifyStateChanged();
            }
        }
        private void NotifyStateChanged() {
            OnBoloChanged?.Invoke();
        }

        public async Task<bool> AdicionarBoloAoBanco() {
            try {
                _dbContext.Bolos.Add(BoloAtual); // Adiciona a entidade BoloAtual ao DbSet de Bolos
                await _dbContext.SaveChangesAsync(); // Salva as mudanças no banco de dados
                return true; // Indica que a operação foi bem-sucedida
            }
            catch (Exception ex) {
                // Registre o erro (opcional)
                Console.WriteLine($"Erro ao salvar o bolo: {ex.Message}");
                return false; // Indica que houve um erro
            }
        }

        public async Task<bool> AtualizarBoloNoBanco() {
            try {
                var boloExistente = await _dbContext.Bolos.FindAsync(BoloAtual.Id);
                if (boloExistente != null) {

                    _dbContext.Entry(boloExistente).CurrentValues.SetValues(BoloAtual);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false; // Bolo não encontrado
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro ao atualizar o bolo com ID {BoloAtual.Id}: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Bolo>> ObterBolosUsuarioAsync(User criador)
        {
            return await _dbContext.Bolos
                .Where(b => b.criador.Id == criador.Id)
                .ToListAsync();
        }

        public async Task<Bolo?> ObterBoloPorIdAsync(int id) {
            return await _dbContext.Bolos
                .Include(b => b.camadas) // Incluir as camadas relacionadas
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> ExcluirBoloAsync(int id) {
            try {
                var boloParaExcluir = await _dbContext.Bolos.FindAsync(id);
                if (boloParaExcluir != null) {
                    _dbContext.Bolos.Remove(boloParaExcluir);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false; // Bolo não encontrado
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro ao excluir o bolo com ID {id}: {ex.Message}");
                return false;
            }
        }
    }
}
