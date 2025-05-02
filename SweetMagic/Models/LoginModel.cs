using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using SweetMagic.Services;

namespace SweetMagic.Models {
    public class LoginModel {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
