using Biblioteca_Data;
using Biblioteca_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_Aplicacion.Services
{
    public class ResenaService
    {
        private readonly LibraryDbContext _db;
        public ResenaService(LibraryDbContext db) => _db = db;

        public async Task<List<Resena>> Listar() => await _db.Resenas.Include(r => r.Libro).Include(r => r.Usuario).ToListAsync();

        public async Task<int> Crear(int usuarioId, int libroId, int puntuacion, string comentario)
        {
            var r = new Resena(usuarioId, libroId, puntuacion, comentario);
            _db.Resenas.Add(r);
            await _db.SaveChangesAsync();
            return r.Id;
        }
    }

}
