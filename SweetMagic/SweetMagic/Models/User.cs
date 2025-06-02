using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SweetMagic.Models
{
    public class User : IValidatableObject
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Insira um e-mail válido.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Insira um nome.")]
        [StringLength(100, ErrorMessage = "Nome não pode exceder 100 caracteres")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Insira uma senha.")]
        public string? PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Insira a pergunta de segurança.")]
        public string? PerguntaSeguranca { get; set; }
        [Required(ErrorMessage = "Insira a resposta de segurança.")] 
        public string? RespostaSeguranca { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            var errors = new List<ValidationResult>();

            //Validates if password is strong
            if (!IsStrongPassword(PasswordHash!))
                errors.Add(new ValidationResult(
                    "Senha muito fraca.", new[] { nameof(PasswordHash) }));

            //Validates if email is valid

            if(String.IsNullOrWhiteSpace(Email) || !Email!.Contains('@'))
                    errors.Add(new ValidationResult(
                        "Insira um e-mail válido.", new[] { nameof(Email) }));
            return errors;
        }
        private bool IsStrongPassword(string password) {
            // Password should be at least 8 characters, contain an uppercase letter,
            // a lowercase letter, a digit, and a special character.
            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            return Regex.IsMatch(password, passwordPattern);
        }
    }
}
