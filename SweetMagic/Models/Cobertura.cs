using System.ComponentModel.DataAnnotations;

namespace SweetMagic.Models {
    public class Cobertura {
        [Key]
        public int Id { get; set; }
        public string tipo { get; set; }
        public string tema { get; set; }
        public bool papelArroz { get; set; } = false;
    }
}
