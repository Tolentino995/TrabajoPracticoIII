using ECommerce.DataAccess;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceDualPrint3D.Pages.Categorias
{
    public class EditarModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditarModel(ApplicationDbContext context)
        {
            _context = context;
        }

        //Propiedad con BindProperty
        [BindProperty]

        public Categoria Categoria { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Categoria = await _context.Categorias.FindAsync(id);

            if (Categoria == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var categoriaBd = await _context.Categorias.FindAsync(Categoria.Id);
            if (categoriaBd == null)
            {
                return NotFound();
            }

            // Actualizalos camplos modificables

            categoriaBd.OrdenVisualizacion=Categoria.OrdenVisualizacion;
            categoriaBd.Nombre=Categoria.Nombre;

            //Guardar Cambios

            await _context.SaveChangesAsync();
            TempData["Success"] = "La categoría se ha editado correctamente.";
            return RedirectToPage("Index");
        }
    }
}
