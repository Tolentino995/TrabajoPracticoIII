using ECommerceDualPrint3D.Datos;
using ECommerceDualPrint3D.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceDualPrint3D.Pages.Categorias
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel (ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Categoria> Categorias { get; set; } = default;
        public async Task OnGetAsync()
        {
            //Cargamos todas las categorias desde la base de datos
            Categorias = await _context.Categorias
                .OrderBy(c => c.OrdenVisualizacion) //Ordena por campo el OrdenVisuali..
                .ToListAsync();
        }
    }
}
