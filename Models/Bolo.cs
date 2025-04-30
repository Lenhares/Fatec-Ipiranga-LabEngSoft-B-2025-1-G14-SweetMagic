using Microsoft.AspNetCore.Http.Timeouts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetMagic.Models {
    public class Bolo {
        [Key]
        public int Id { get; set; }
        public List<Camada> camadas { get; set; } = new();
        public string imagemFinal { get; set; }
        public int peso { get; set; }
        public string tipo { get; set; }
        public DateTime dataEntrega { get; set; }
        public Cobertura cobertura { get; set; } = new();
        public string titulo { get; set; }
        public User criador { get; set; }
        
    }
}
