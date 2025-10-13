using ECommerce.Models;
using ECommerce.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceDualPrint3D.Pages.Admin.Categorias
{
    public class CrearModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CrearModel(ApplicationDbContext context)
        {
            _context = context;
        }

        //Propiedad con BindProperty
        [BindProperty]

        public Categoria Categoria { get; set; } = default!;
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validaci�n personalizada: Comprobar si el nombre de la categor�a ya existe

            bool nombreExiste = _context.Categorias.Any(c => c.Nombre == Categoria.Nombre);
            if (nombreExiste)
            {
                ModelState.AddModelError("Categoria.Nombre", "El nombre de la categor�a ya existe. Por favor, elige otro nombre.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Asignar la fecha de creaci�n

            Categoria.FechaCreacion = DateTime.Now;

            _context.Categorias.Add(Categoria); 
            await _context.SaveChangesAsync();

            //TempData para mensaje al volver al Index
            TempData["Success"] = "La categor�a se ha creado correctamente.";

            return RedirectToPage("Index");
        }
    }
}
