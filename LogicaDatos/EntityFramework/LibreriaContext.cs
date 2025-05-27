using DentalLatina;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System.IO;
using System.Collections.Generic;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Threading.Tasks;
using System;

namespace LogicaDatos.EntityFramework
{
    public class LibreriaContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Promocion> Promociones { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Zona> Zonas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Subcategoria> Subcategorias { get; set; }
        public DbSet<CEspecial> CEspecial { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()) // <-- Ahora funciona
                    .AddJsonFile("appsettings.json")
                    .Build();

                string strCon = configuration.GetConnectionString("DefaultConnection");
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));
                optionsBuilder.UseMySql(strCon, serverVersion, b => b.MigrationsAssembly("LogicaDatos"));
            }
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evento>()
                .HasKey(e => e.id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
