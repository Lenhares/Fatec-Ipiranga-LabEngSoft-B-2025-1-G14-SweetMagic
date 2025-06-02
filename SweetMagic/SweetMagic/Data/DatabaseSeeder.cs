using SweetMagic.Models;

namespace SweetMagic.Data {
    public class DatabaseSeeder {
        private readonly AppDbContext _context;

        public DatabaseSeeder(AppDbContext context) {
            _context = context;
        }

        public void Seed() {
            if (!_context.Bolos.Any()) {
                var user = _context.Users.FirstOrDefault();

                if (user == null) {
                    // Caso não tenha um usuário, cria um fake
                    user = new User {
                        Nome = "admin",
                        Email = "admin@sweetmagic.com",
                        PasswordHash = "senha_fake" // De preferência, hasheie!
                    };
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }

                var cobertura = new Cobertura {
                    tipo = "Chantilly",
                    tema = "Praia",
                    papelArroz = true
                };

                var bolo = new Bolo {
                    peso = 2000,
                    tipo = "Redondo",
                    dataEntrega = DateTime.Now.AddDays(7),
                    imagemFinal = "",
                    titulo = "Feliz Aniversário!",
                    criador = user,
                    cobertura = cobertura,
                    camadas = new List<Camada>
                    {
                    new Camada
                    {
                        tipo = "Tradicional",
                        ordem = 1,
                        saborMassa = "Baunilha",
                        saborRecheio = "Morango"
                    },
                    new Camada
                    {
                        tipo = "Especial",
                        ordem = 2,
                        saborMassa = "Chocolate",
                        saborRecheio = "Brigadeiro"
                    }
                }
                };

                _context.Bolos.Add(bolo);
                _context.SaveChanges();
            }
        }
    }
}
