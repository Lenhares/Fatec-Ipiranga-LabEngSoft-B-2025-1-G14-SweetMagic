using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetMagic.Models {
    public class Camada {
        [Key]
        public int Id { get; set; }
        public string tipo { get; set; }
        public int ordem { get; set; }
        public string saborMassa { get; set; }
        public string saborRecheio { get; set; }
        public int  BoloId { get; set; }
        [ForeignKey(nameof(BoloId))]
        public Bolo Bolo { get; set; }
    }
}
