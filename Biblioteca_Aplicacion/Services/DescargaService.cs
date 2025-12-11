using Biblioteca_Data;
using Biblioteca_Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Biblioteca_Aplicacion.Services
{
    public class DescargaService
    {
        private readonly LibraryDbContext _db;
        public DescargaService(LibraryDbContext db) => _db = db;

        public async Task<List<Descarga>> Listar() => await _db.Descargas.Include(d => d.Libro).Include(d => d.Usuario).ToListAsync();

        public async Task<int> Registrar(int usuarioId, int libroId, long tamañoBytes)
        {
            var d = new Descarga(usuarioId, libroId, tamañoBytes);
            _db.Descargas.Add(d);
            await _db.SaveChangesAsync();
            return d.Id;
        }
    }

}
