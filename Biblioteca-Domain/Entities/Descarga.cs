using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_Domain.Entities
{

    public class Descarga : EntityBase
    {
        public int UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }

        public int LibroId { get; private set; }
        public Libro? Libro { get; private set; }

        public DateTime FechaDescarga { get; private set; }
        public long TamañoBytes { get; private set; }

        protected Descarga() { } // EF

        public Descarga(int usuarioId, int libroId, long tamañoBytes)
        {
            UsuarioId = usuarioId;
            LibroId = libroId;
            TamañoBytes = tamañoBytes;
            FechaDescarga = DateTime.UtcNow;
        }
    }

}
