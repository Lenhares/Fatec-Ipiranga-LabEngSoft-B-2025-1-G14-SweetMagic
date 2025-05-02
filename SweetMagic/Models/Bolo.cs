using Microsoft.AspNetCore.Http.Timeouts;

namespace SweetMagic.Models {
    internal class Bolo {
        internal List<Camada> camadas { get; set; }
        internal string imagemFinal { get; set; }
        internal int peso { get; set; }
        internal string tipo { get; set; }
        internal DateTime dataEntrega { get; set; }
        internal Cobertura cobertura { get; set; }
        internal string titulo { get; set; }
        internal User criador { get; set; }
        
    }
}
