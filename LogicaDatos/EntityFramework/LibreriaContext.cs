using DentalLatina;
using LogicaNegocio;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LogicaDatos.EntityFramework
{
    public class LibreriaContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Promocion> Promociones { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Zona> Zonas { get; set; }
  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string strCon = "Data Source=DESKTOP-TF5UE3O\\SQLEXPRESS; Initial Catalog=DentalLatina; Integrated Security=True; TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(strCon);
            base.OnConfiguring(optionsBuilder);
        }

        public LibreriaContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evento>()
        .HasKey(e => e.id); // Configura 'id' como clave primaria

            base.OnModelCreating(modelBuilder);

        }
    }
}