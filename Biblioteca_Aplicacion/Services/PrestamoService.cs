using Biblioteca_Data;
using Biblioteca_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_Aplicacion.Services
{ 
    public class PrestamoService
    {
        private readonly LibraryDbContext _db;
        public PrestamoService(LibraryDbContext db) => _db = db;

        public async Task<List<Prestamo>> Listar() => await _db.Prestamos.Include(p => p.Libro).Include(p => p.Usuario).ToListAsync();

        public async Task<int> Crear(int usuarioId, int libroId, DateTime fechaFin)
        {
            var p = new Prestamo(usuarioId, libroId, fechaFin);
            _db.Prestamos.Add(p);
            await _db.SaveChangesAsync();
            return p.Id;
        }

        public async Task<bool> MarcarDevuelto(int id)
        {
            var p = await _db.Prestamos.FindAsync(id);
            if (p == null) return false;
            p.MarcarDevuelto();
            await _db.SaveChangesAsync();
            return true;
        }
    }

}
