using Microsoft.EntityFrameworkCore;

namespace ECommerceDualPrint3D.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> dbContext) : base (dbContext) 
        {
        }

        //Aqui Añadiremos los modelos(tabla)
    }
}
