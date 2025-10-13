using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using ECommerce.DataAccess;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceDualPrint3D.Pages.Categorias
{
    public class DetalleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetalleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Categoria Categoria { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Categoria = await _context.Categorias.FindAsync(id);
            if (Categoria == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
