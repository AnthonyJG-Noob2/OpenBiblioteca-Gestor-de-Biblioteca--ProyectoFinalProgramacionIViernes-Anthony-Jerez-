using Biblioteca_Data;
using Biblioteca_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_Aplicacion.Services
{

    public class UsuarioService
    {
        private readonly LibraryDbContext _db;
        public UsuarioService(LibraryDbContext db) => _db = db;

        public async Task<List<Usuario>> Listar() => await _db.Usuarios.ToListAsync();
        public async Task<int> Crear(string nombre, string email, string passwordHash)
        {
            var u = new Usuario(nombre, email, passwordHash);
            _db.Usuarios.Add(u);
            await _db.SaveChangesAsync();
            return u.Id;
        }
    }

}
