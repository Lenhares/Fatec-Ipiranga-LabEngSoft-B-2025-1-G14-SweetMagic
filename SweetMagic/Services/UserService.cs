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
            return user != null && PasswordHelper.VerifyPassword(password, user.PasswordHash!);
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
    }
}
