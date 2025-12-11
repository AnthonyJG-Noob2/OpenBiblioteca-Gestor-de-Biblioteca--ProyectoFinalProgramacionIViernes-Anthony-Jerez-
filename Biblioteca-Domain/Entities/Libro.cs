
namespace Biblioteca_Domain.Entities
{

    public class Libro : EntityBase
    {
        public string Titulo { get; private set; } = null!;
        public string ISBN { get; private set; } = null!;
        public string Autor { get; private set; } = null!;
        public int Paginas { get; private set; }

        public int CategoriaId { get; private set; }
        public Categoria? Categoria { get; private set; }

        public ICollection<Descarga> Descargas { get; private set; } = new List<Descarga>();
        public ICollection<Prestamo> Prestamos { get; private set; } = new List<Prestamo>();
        public ICollection<Resena> Resenas { get; private set; } = new List<Resena>();

        protected Libro() { } // EF

        public Libro(string titulo, string isbn, string autor, int paginas, int categoriaId)
        {
            Titulo = titulo;
            ISBN = isbn;
            Autor = autor;
            Paginas = paginas;
            CategoriaId = categoriaId;
        }

        public void Update(string titulo, string isbn, string autor, int paginas, int categoriaId)
        {
            Titulo = titulo;
            ISBN = isbn;
            Autor = autor;
            Paginas = paginas;
            CategoriaId = categoriaId;
            Touch();
        }
    }


}
