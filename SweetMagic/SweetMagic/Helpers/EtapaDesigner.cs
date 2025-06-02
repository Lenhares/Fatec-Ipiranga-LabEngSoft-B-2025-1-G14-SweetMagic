using SweetMagic.Components.Pages.DesignerSteps;

namespace SweetMagic.Helpers {
    public class EtapaDesigner {
        public string Nome { get; set; } = String.Empty;
        public Type ComponentType { get; set; } = default!;
        public int Id { get; set; }


        private EtapaDesigner() {
            return;
        }

        public static List<EtapaDesigner> GetEtapas() {

            return new List<EtapaDesigner> {
                new() { Nome = "Tamanho", ComponentType = typeof(PesoBolo), Id = 1 },
                new() { Nome = "Tipo", ComponentType = typeof(TipoBolo), Id = 2 },
                new() { Nome = "Data", ComponentType = typeof(DataBolo), Id = 3 },
                new() { Nome = "Camadas", ComponentType = typeof(CamadasBolo), Id = 4 },
                new() { Nome = "SaborCamada", ComponentType = typeof(SaborCamada), Id = 5 },
                new() { Nome = "RecheioCamada", ComponentType = typeof(RecheioCamada), Id = 6 },
                new() { Nome = "Cobertura", ComponentType = typeof(CoberturaBolo), Id = 7 },
                new() { Nome = "Review", ComponentType = typeof(ReviewBolo), Id = 8 }
             };
        }
    }
}
