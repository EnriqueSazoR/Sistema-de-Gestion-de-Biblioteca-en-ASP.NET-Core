using APIBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        // Colocar todos los modelos que se creen
        DbSet<Libro> Libros { get; set; }
        DbSet<Categoria> Categorias { get; set; }
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Rol> Roles { get; set; }
             
    }
}
