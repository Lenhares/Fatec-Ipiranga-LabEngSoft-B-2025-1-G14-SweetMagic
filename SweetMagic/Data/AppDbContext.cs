using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SweetMagic.Models;

namespace SweetMagic.Data
{
    public class AppDbContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Bolo> Bolos { get; set; }
        public DbSet<Camada> Camadas { get; set; }
        public DbSet<Cobertura> Coberturas { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
