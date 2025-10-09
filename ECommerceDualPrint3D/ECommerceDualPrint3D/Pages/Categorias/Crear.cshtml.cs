using ECommerceDualPrint3D.Datos;
using ECommerceDualPrint3D.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceDualPrint3D.Pages.Categorias
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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Asignar la fecha de creación

            Categoria.FechaCreacion = DateTime.Now;

            _context.Categorias.Add(Categoria); 
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
