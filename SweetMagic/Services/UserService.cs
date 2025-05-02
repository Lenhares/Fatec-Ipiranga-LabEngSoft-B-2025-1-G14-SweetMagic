using Microsoft.EntityFrameworkCore;
using SweetMagic.Models;
using SweetMagic.Data;
using SweetMagic.Helpers;
using Org.BouncyCastle.Crypto.Digests;
using System.ComponentModel.DataAnnotations;

namespace SweetMagic.Services
{
    public class UserService {
        
        private readonly AppDbContext _context;
        public UserService(AppDbContext context) {
            _context = context;
        }
        public User? UsuarioAtual { get; private set; }
        public bool Logado { get; private set; }
        public event Action StatusUsuarioAtualizado;
        public void NotificarStatusAtualizado() => StatusUsuarioAtualizado.Invoke();
        public async Task<bool> RegisterUserAsync(string email, string nome, string password, string securityQ, string securityA) {
            if (await _context.Users.AnyAsync(u => u.Email == email))
                return false; // User already exists

            var user = new User {
                Email = email,
                Nome = nome,
                PasswordHash = PasswordHelper.HashPassword(password),
                PerguntaSeguranca = securityQ,
                RespostaSeguranca = securityA
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> LoginUserAsync(string email, string password) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null && PasswordHelper.VerifyPassword(password, user.PasswordHash!)) {
                UsuarioAtual = user;
                Logado = true;
                NotificarStatusAtualizado();
                return true;
            }
            UsuarioAtual = null;
            Logado = false;
            NotificarStatusAtualizado();
            return false;
        }
        public void LogoutUser() {
            UsuarioAtual = null;
            Logado = false;
            NotificarStatusAtualizado();
        }

        public async Task<bool> VerifySecurityAnswerAsync(User user, string pergunta, string resposta) {

            return user.PerguntaSeguranca == pergunta && user.RespostaSeguranca == resposta;
        }

        public async Task<bool> ResetPasswordAsync(User user, string newPassword) {

            if (user == null)
                return false; // User not found
            user.PasswordHash = PasswordHelper.HashPassword(newPassword); // Hash the new password

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true; // Password updated successfully
        }
        public async Task<User?> GetUserByEmailAsync(string email) {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> DeleteUserDataAsync(string email) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false; // User not found

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestoreSessionAsync(string email) {

            var user = await GetUserByEmailAsync(email);
            if (user == null) {
                UsuarioAtual = null;
                Logado = false;
                NotificarStatusAtualizado();
                return false;
            }
            UsuarioAtual = user;
            Logado = true;
            NotificarStatusAtualizado();
            return true;
        }
    }
}
