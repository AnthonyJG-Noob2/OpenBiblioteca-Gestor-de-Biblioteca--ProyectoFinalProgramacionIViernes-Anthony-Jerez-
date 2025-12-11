using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_Domain.Entities
{

    public class Prestamo : EntityBase
    {
        public int UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }

        public int LibroId { get; private set; }
        public Libro? Libro { get; private set; }

        public DateTime FechaInicio { get; private set; }
        public DateTime? FechaFin { get; private set; }
        public bool Devuelto { get; private set; }

        protected Prestamo() { } // EF

        public Prestamo(int usuarioId, int libroId, DateTime fechaFinEsperada)
        {
            UsuarioId = usuarioId;
            LibroId = libroId;
            FechaInicio = DateTime.UtcNow;
            FechaFin = fechaFinEsperada;
            Devuelto = false;
        }

        public void MarcarDevuelto()
        {
            Devuelto = true;
            Touch();
        }
    }

}
