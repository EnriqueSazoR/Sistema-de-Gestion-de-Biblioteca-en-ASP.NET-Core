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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>().HasData(
              new Rol { Id = 1, NombreRol = "Cliente"},
              new Rol { Id = 2, NombreRol = "Admin"},
              new Rol { Id = 3, NombreRol = "Bibliotecario"}
            
            );
        }


        // Colocar todos los modelos que se creen
       public  DbSet<Libro> Libros { get; set; }
       public DbSet<Categoria> Categorias { get; set; }
       public  DbSet<Usuario> Usuarios { get; set; }
       public DbSet<Rol> Roles { get; set; }
       public  DbSet<Prestamo> Prestamos { get; set; }
             
    }
}
