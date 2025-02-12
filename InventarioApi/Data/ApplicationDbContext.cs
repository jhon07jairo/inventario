using Microsoft.EntityFrameworkCore;
using InventarioApi.Models;
using System;

namespace InventarioApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<MovimientoInventario> MovimientosInventario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 📌 Configuración de la tabla Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.Rol).HasDefaultValue("Usuario");
            });

            // 📌 Configuración de la tabla Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Stock).IsRequired().HasDefaultValue(0);
            });

            // 📌 Configuración de la tabla MovimientoInventario
            modelBuilder.Entity<MovimientoInventario>(entity =>
            {
                entity.ToTable("MovimientosInventario");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.TipoMovimiento)
                      .IsRequired()
                      .HasMaxLength(10);
                entity.Property(m => m.Fecha)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Relación con Producto
                entity.HasOne(m => m.Producto)
                      .WithMany()
                      .HasForeignKey(m => m.ProductoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 📌 SEED DATA (Datos iniciales)
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = 1,
                Nombre = "Admin",
                Email = "admin@ccl.com",
                PasswordHash = "hashedpassword123", // En un sistema real, esto debería ser un hash seguro
                Rol = "Admin"
            });

            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Laptop Dell", Stock = 10 },
                new Producto { Id = 2, Nombre = "Monitor Samsung", Stock = 5 }
            );

            modelBuilder.Entity<MovimientoInventario>().HasData(
                new MovimientoInventario
                {
                    Id = 1,
                    ProductoId = 1,
                    Cantidad = 10,
                    TipoMovimiento = "Entrada",
                    Fecha = new DateTime(2024, 2, 1, 8, 30, 0)
                },
                new MovimientoInventario
                {
                    Id = 2,
                    ProductoId = 2,
                    Cantidad = 5,
                    TipoMovimiento = "Entrada",
                    Fecha = new DateTime(2024, 2, 2, 10, 15, 0)
                }
            );
        }
    }
}
