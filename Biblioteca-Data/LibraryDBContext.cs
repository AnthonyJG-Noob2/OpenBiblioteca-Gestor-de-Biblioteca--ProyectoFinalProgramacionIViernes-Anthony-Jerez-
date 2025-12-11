using Biblioteca_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Biblioteca_Data
{

    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<Libro> Libros { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Descarga> Descargas { get; set; } = null!;
        public DbSet<Prestamo> Prestamos { get; set; } = null!;
        public DbSet<Resena> Resenas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Libro <-> Categoria
            modelBuilder.Entity<Libro>()
                .HasOne(l => l.Categoria)
                .WithMany(c => c.Libros)
                .HasForeignKey(l => l.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Descargas
            modelBuilder.Entity<Descarga>()
                .HasOne(d => d.Usuario).WithMany(u => u.Descargas).HasForeignKey(d => d.UsuarioId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Descarga>()
                .HasOne(d => d.Libro).WithMany(l => l.Descargas).HasForeignKey(d => d.LibroId).OnDelete(DeleteBehavior.Cascade);

            // Prestamos
            modelBuilder.Entity<Prestamo>()
                .HasOne(p => p.Usuario).WithMany(u => u.Prestamos).HasForeignKey(p => p.UsuarioId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Prestamo>()
                .HasOne(p => p.Libro).WithMany(l => l.Prestamos).HasForeignKey(p => p.LibroId).OnDelete(DeleteBehavior.Cascade);

            // Resenas
            modelBuilder.Entity<Resena>()
                .HasOne(r => r.Usuario).WithMany(u => u.Resenas).HasForeignKey(r => r.UsuarioId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Resena>()
                .HasOne(r => r.Libro).WithMany(l => l.Resenas).HasForeignKey(r => r.LibroId).OnDelete(DeleteBehavior.Cascade);

            // Indices y restricciones
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Libro>().HasIndex(l => l.ISBN).IsUnique(false);

            // Seed básico (opcional)
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria("General") { Id = 1 },
                new Categoria("Ficción") { Id = 2 },
                new Categoria("Programación") { Id = 3 }
            );

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario("Admin", "admin@local", "hash") { Id = 1 }
            );

            // NOTE: Entities have protected constructors; to seed complex entities you may need value conversions or use raw SQL.
        }
    }

}
