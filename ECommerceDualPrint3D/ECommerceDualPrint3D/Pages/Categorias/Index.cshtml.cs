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

        public async Task<IActionResult> OnPostDeleteAsync([FromBody]int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                TempData["Error"] = "La categor�a no fue encontrada";
                return RedirectToPage("Index");

            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            TempData["Success"]= "Categoria Eliminada Correctamente";
            return new JsonResult(new { success = true });
        }
    }
}
