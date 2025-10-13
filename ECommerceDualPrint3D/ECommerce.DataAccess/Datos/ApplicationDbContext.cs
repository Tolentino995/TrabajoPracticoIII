using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAcces
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> dbContext) : base (dbContext) 
        {
        }

        //Aqui Añadiremos los modelos(tabla)
        public DbSet<Categoria> Categorias { get; set; }
    }
}
