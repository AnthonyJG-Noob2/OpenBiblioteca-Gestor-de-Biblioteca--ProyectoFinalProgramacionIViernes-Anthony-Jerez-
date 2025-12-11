using Microsoft.Extensions.DependencyInjection;
using Biblioteca_Data;
using Microsoft.EntityFrameworkCore;
using Biblioteca_Aplicacion.Services;
using Biblioteca_Presentacion.Helpers;

var services = new ServiceCollection();

// Ajusta la cadena a tu servidor
var connectionString = "Data Source=DESKTOP-3CQAOA9;Initial Catalog=OpenBibliotecaDB;Integrated Security=True;TrustServerCertificate=True";

services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(connectionString));

// Registrar servicios
services.AddScoped<LibroService>();
services.AddScoped<CategoriaService>();
services.AddScoped<UsuarioService>();
services.AddScoped<PrestamoService>();
services.AddScoped<DescargaService>();
services.AddScoped<ResenaService>();

var provider = services.BuildServiceProvider();

// Crear BD si no existe (o usar migrations)
using (var scope = provider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    db.Database.EnsureCreated();
}

var libroSvc = provider.GetRequiredService<LibroService>();
var catSvc = provider.GetRequiredService<CategoriaService>();
var usuarioSvc = provider.GetRequiredService<UsuarioService>();
var prestamoSvc = provider.GetRequiredService<PrestamoService>();
var descargaSvc = provider.GetRequiredService<DescargaService>();
var resenaSvc = provider.GetRequiredService<ResenaService>();

await MainMenu();

async Task MainMenu()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== OPENBIBLIOTECA: TU GESTOR DE BIBLIOTECA DIGITAL ===");
        Console.WriteLine("1. Libros");
        Console.WriteLine("2. Usuarios");
        Console.WriteLine("3. Categorías");
        Console.WriteLine("4. Préstamos");
        Console.WriteLine("5. Descargas");
        Console.WriteLine("6. Reseñas");
        Console.WriteLine("0. Salir");
        var opt = Console.ReadLine();

        switch (opt)
        {
            case "1": await MenuLibros(); break;
            case "2": await MenuUsuarios(); break;
            case "3": await MenuCategorias(); break;
            case "4": await MenuPrestamos(); break;
            case "5": await MenuDescargas(); break;
            case "6": await MenuResenas(); break;
            case "0": return;
        }
    }
}

// ---------- LIBROS ----------
async Task MenuLibros()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== LIBROS ===");
        Console.WriteLine("1. Listar");
        Console.WriteLine("2. Crear");
        Console.WriteLine("3. Editar");
        Console.WriteLine("4. Eliminar");
        Console.WriteLine("0. Volver");
        var opt = Console.ReadLine();

        if (opt == "1")
        {
            var list = await libroSvc.Listar();
            Console.Clear();
            foreach (var l in list)
                Console.WriteLine($"{l.Id} | {l.Titulo} | {l.ISBN} | {l.Autor} | {l.Paginas} | Cat:{l.CategoriaId}");
            Console.WriteLine("Enter para continuar...");
            Console.ReadLine();
        }
        else if (opt == "2")
        {
            var titulo = InputHelper.LeerTexto("Título: ");
            var isbn = InputHelper.LeerTexto("ISBN: ");
            var autor = InputHelper.LeerTexto("Autor: ");
            var paginas = InputHelper.LeerEntero("Páginas: ");
            var categorias = await catSvc.Listar();
            Console.WriteLine("Categorias disponibles:");
            foreach (var c in categorias) Console.WriteLine($"{c.Id} - {c.Nombre}");
            var catId = InputHelper.LeerEntero("CategoriaId: ");
            var id = await libroSvc.Crear(titulo, isbn, autor, paginas, catId);
            Console.WriteLine($"Libro creado. Id={id}");
            Console.ReadLine();
        }
        else if (opt == "3")
        {
            var id = InputHelper.LeerEntero("ID del libro: ");
            var libro = await libroSvc.Obtener(id);
            if (libro == null) { Console.WriteLine("No existe."); Console.ReadLine(); continue; }
            var titulo = InputHelper.LeerTexto($"Título ({libro.Titulo}): ");
            var isbn = InputHelper.LeerTexto($"ISBN ({libro.ISBN}): ");
            var autor = InputHelper.LeerTexto($"Autor ({libro.Autor}): ");
            var paginas = InputHelper.LeerEntero($"Páginas ({libro.Paginas}): ");
            var catId = InputHelper.LeerEntero($"CategoriaId ({libro.CategoriaId}): ");
            var ok = await libroSvc.Actualizar(id,
                string.IsNullOrWhiteSpace(titulo) ? libro.Titulo : titulo,
                string.IsNullOrWhiteSpace(isbn) ? libro.ISBN : isbn,
                string.IsNullOrWhiteSpace(autor) ? libro.Autor : autor,
                paginas == 0 ? libro.Paginas : paginas,
                catId == 0 ? libro.CategoriaId : catId);
            Console.WriteLine(ok ? "Actualizado." : "No encontrado.");
            Console.ReadLine();
        }
        else if (opt == "4")
        {
            var id = InputHelper.LeerEntero("ID a eliminar: ");
            var ok = await libroSvc.Eliminar(id);
            Console.WriteLine(ok ? "Eliminado." : "No encontrado.");
            Console.ReadLine();
        }
        else break;
    }
}

// ---------- USUARIOS ----------
async Task MenuUsuarios()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== USUARIOS ===");
        Console.WriteLine("1. Listar");
        Console.WriteLine("2. Crear");
        Console.WriteLine("0. Volver");
        var opt = Console.ReadLine();

        if (opt == "1")
        {
            var list = await usuarioSvc.Listar();
            foreach (var u in list) Console.WriteLine($"{u.Id} | {u.Nombre} | {u.Email}");
            Console.ReadLine();
        }
        else if (opt == "2")
        {
            var nombre = InputHelper.LeerTexto("Nombre: ");
            var email = InputHelper.LeerTexto("Email: ");
            var pwd = InputHelper.LeerTexto("Password (plain, will hash?): ");
            var id = await usuarioSvc.Crear(nombre, email, pwd);
            Console.WriteLine($"Usuario creado Id={id}");
            Console.ReadLine();
        }
        else break;
    }
}

// ---------- CATEGORIAS ----------
async Task MenuCategorias()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== CATEGORIAS ===");
        Console.WriteLine("1. Listar");
        Console.WriteLine("2. Crear");
        Console.WriteLine("3. Eliminar");
        Console.WriteLine("0. Volver");
        var opt = Console.ReadLine();

        if (opt == "1")
        {
            var list = await catSvc.Listar();
            foreach (var c in list) Console.WriteLine($"{c.Id} | {c.Nombre} | {c.Descripcion}");
            Console.ReadLine();
        }
        else if (opt == "2")
        {
            var nombre = InputHelper.LeerTexto("Nombre: ");
            var desc = InputHelper.LeerTexto("Descripcion: ");
            var id = await catSvc.Crear(nombre, desc);
            Console.WriteLine($"Creada Id={id}");
            Console.ReadLine();
        }
        else if (opt == "3")
        {
            var id = InputHelper.LeerEntero("Id a eliminar: ");
            var ok = await catSvc.Eliminar(id);
            Console.WriteLine(ok ? "Eliminada." : "No encontrada.");
            Console.ReadLine();
        }
        else break;
    }
}

// ---------- PRESTAMOS ----------
async Task MenuPrestamos()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== PRESTAMOS ===");
        Console.WriteLine("1. Listar");
        Console.WriteLine("2. Crear");
        Console.WriteLine("3. Marcar devuelto");
        Console.WriteLine("0. Volver");
        var opt = Console.ReadLine();

        if (opt == "1")
        {
            var list = await prestamoSvc.Listar();
            foreach (var p in list) Console.WriteLine($"{p.Id} | Libro:{p.LibroId} | Usuario:{p.UsuarioId} | Inicio:{p.FechaInicio} | Devuelto:{p.Devuelto}");
            Console.ReadLine();
        }
        else if (opt == "2")
        {
            var usuarioId = InputHelper.LeerEntero("UsuarioId: ");
            var libroId = InputHelper.LeerEntero("LibroId: ");
            var fechaFin = InputHelper.LeerFecha("Fecha fin (yyyy-mm-dd): ");
            var id = await prestamoSvc.Crear(usuarioId, libroId, fechaFin);
            Console.WriteLine($"Prestamo creado Id={id}");
            Console.ReadLine();
        }
        else if (opt == "3")
        {
            var id = InputHelper.LeerEntero("Id prestamo: ");
            var ok = await prestamoSvc.MarcarDevuelto(id);
            Console.WriteLine(ok ? "Marcado devuelto." : "No encontrado.");
            Console.ReadLine();
        }
        else break;
    }
}

// ---------- DESCARGAS ----------
async Task MenuDescargas()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== DESCARGAS ===");
        Console.WriteLine("1. Listar");
        Console.WriteLine("2. Registrar");
        Console.WriteLine("0. Volver");
        var opt = Console.ReadLine();

        if (opt == "1")
        {
            var list = await descargaSvc.Listar();
            foreach (var d in list) Console.WriteLine($"{d.Id} | Libro:{d.LibroId} | Usuario:{d.UsuarioId} | Fecha:{d.FechaDescarga} | Tamaño:{d.TamañoBytes}");
            Console.ReadLine();
        }
        else if (opt == "2")
        {
            var usuarioId = InputHelper.LeerEntero("UsuarioId: ");
            var libroId = InputHelper.LeerEntero("LibroId: ");
            var tamaño = InputHelper.LeerLong("Tamaño bytes: ");
            var id = await descargaSvc.Registrar(usuarioId, libroId, tamaño);
            Console.WriteLine($"Descarga registrada Id={id}");
            Console.ReadLine();
        }
        else break;
    }
}

// ---------- RESENAS ----------
async Task MenuResenas()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== RESENAS ===");
        Console.WriteLine("1. Listar");
        Console.WriteLine("2. Crear");
        Console.WriteLine("0. Volver");
        var opt = Console.ReadLine();

        if (opt == "1")
        {
            var list = await resenaSvc.Listar();
            foreach (var r in list) Console.WriteLine($"{r.Id} | Libro:{r.LibroId} | Usuario:{r.UsuarioId} | P:{r.Puntuacion} | {r.Comentario}");
            Console.ReadLine();
        }
        else if (opt == "2")
        {
            var usuarioId = InputHelper.LeerEntero("UsuarioId: ");
            var libroId = InputHelper.LeerEntero("LibroId: ");
            var puntuacion = InputHelper.LeerEntero("Puntuacion (1..5): ");
            var comentario = InputHelper.LeerTexto("Comentario: ");
            var id = await resenaSvc.Crear(usuarioId, libroId, puntuacion, comentario);
            Console.WriteLine($"Reseña creada Id={id}");
            Console.ReadLine();
        }
        else break;
    }
}
