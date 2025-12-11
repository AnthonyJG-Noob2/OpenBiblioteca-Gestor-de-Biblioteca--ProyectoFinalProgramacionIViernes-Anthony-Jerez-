using Biblioteca_Data;
using Biblioteca_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_Aplicacion.Services
{

    public class LibroService
    {
        private readonly LibraryDbContext _db;
        public LibroService(LibraryDbContext db) => _db = db;

        public async Task<List<Libro>> Listar() => await _db.Libros.Include(l => l.Categoria).ToListAsync();
        public async Task<Libro?> Obtener(int id) => await _db.Libros.FindAsync(id);

        public async Task<int> Crear(string titulo, string isbn, string autor, int paginas, int categoriaId)
        {
            var libro = new Libro(titulo, isbn, autor, paginas, categoriaId);
            _db.Libros.Add(libro);
            await _db.SaveChangesAsync();
            return libro.Id;
        }

        public async Task<bool> Actualizar(int id, string titulo, string isbn, string autor, int paginas, int categoriaId)
        {
            var libro = await _db.Libros.FindAsync(id);
            if (libro == null) return false;
            libro.Update(titulo, isbn, autor, paginas, categoriaId);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            var libro = await _db.Libros.FindAsync(id);
            if (libro == null) return false;
            _db.Libros.Remove(libro);
            await _db.SaveChangesAsync();
            return true;
        }
    }

}
