using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_Domain.Entities
{

    public class Usuario : EntityBase
    {
        public string Nombre { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!; // simple placeholder

        public ICollection<Descarga> Descargas { get; private set; } = new List<Descarga>();
        public ICollection<Prestamo> Prestamos { get; private set; } = new List<Prestamo>();
        public ICollection<Resena> Resenas { get; private set; } = new List<Resena>();

        protected Usuario() { } // EF

        public Usuario(string nombre, string email, string passwordHash)
        {
            Nombre = nombre;
            Email = email;
            PasswordHash = passwordHash;
        }

        public void Update(string nombre, string email)
        {
            Nombre = nombre;
            Email = email;
            Touch();
        }
    }

}
