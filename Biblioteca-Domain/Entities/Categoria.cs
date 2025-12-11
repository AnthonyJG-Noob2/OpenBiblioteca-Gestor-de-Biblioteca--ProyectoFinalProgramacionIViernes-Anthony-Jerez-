using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_Domain.Entities
{
    public class Categoria : EntityBase
    {
        public string Nombre { get; private set; } = null!;
        public string? Descripcion { get; private set; }

        // colección inversa
        public ICollection<Libro> Libros { get; private set; } = new List<Libro>();

        protected Categoria() { } // EF

        public Categoria(string nombre, string? descripcion = null)
        {
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public void Update(string nombre, string? descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Touch();
        }
    }
}
