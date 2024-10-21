using Microsoft.EntityFrameworkCore;
using GymApp.MVC.Entidades;

namespace GymApp.MVC.Data
{
    public class GymContext : DbContext
    {
        public GymContext(DbContextOptions<GymContext> options) : base(options)
        {
        }

        public DbSet<Miembro> Miembros { get; set; }
        public DbSet<Clase> Clases { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Membresia> Membresias { get; set; }
        public DbSet<Entrenador> Entrenadores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-EGFV38O\\SQLEXPRESS;" +
                                            "Initial Catalog=GymDB;" +
                                            "TrustServerCertificate=true;" +
                                            "Encrypt=true;" +
                                            "Integrated Security=true;");
            }
        }
    }
}