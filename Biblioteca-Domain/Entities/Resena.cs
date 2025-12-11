using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_Domain.Entities
{

    public class Resena : EntityBase
    {
        public int UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }

        public int LibroId { get; private set; }
        public Libro? Libro { get; private set; }

        public int Puntuacion { get; private set; } // 1..5
        public string Comentario { get; private set; } = null!;
        public DateTime Fecha { get; private set; }

        protected Resena() { } // EF

        public Resena(int usuarioId, int libroId, int puntuacion, string comentario)
        {
            UsuarioId = usuarioId;
            LibroId = libroId;
            Puntuacion = puntuacion;
            Comentario = comentario;
            Fecha = DateTime.UtcNow;
        }

        public void Update(int puntuacion, string comentario)
        {
            Puntuacion = puntuacion;
            Comentario = comentario;
            Touch();
        }
    }

}
