using Microsoft.EntityFrameworkCore;
using WebApiComidas.Entidades;

namespace WebApiComidas
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Comida> Comidas { get; set; }
        public DbSet<Restaurante> Restaurantes { get; set; }
    }
}
