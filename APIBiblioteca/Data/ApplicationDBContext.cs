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

    }
}
