using Biblioteca_Data;
using Biblioteca_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_Aplicacion.Services
{
    public class CategoriaService
    {
        private readonly LibraryDbContext _db;
        public CategoriaService(LibraryDbContext db) => _db = db;

        public async Task<List<Categoria>> Listar() => await _db.Categorias.ToListAsync();
        public async Task<int> Crear(string nombre, string? desc)
        {
            var c = new Categoria(nombre, desc);
            _db.Categorias.Add(c);
            await _db.SaveChangesAsync();
            return c.Id;
        }
        public async Task<bool> Eliminar(int id)
        {
            var c = await _db.Categorias.FindAsync(id);
            if (c == null) return false;
            _db.Categorias.Remove(c);
            await _db.SaveChangesAsync();
            return true;
        }
    }

}
