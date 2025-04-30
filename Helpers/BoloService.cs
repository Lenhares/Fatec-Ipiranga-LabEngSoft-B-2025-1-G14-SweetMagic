using Microsoft.EntityFrameworkCore;
using SweetMagic.Models;
using SweetMagic.Data;
using SweetMagic.Helpers;

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
    }
}
