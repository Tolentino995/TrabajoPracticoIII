using ECommerce.DataAccess;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceDualPrint3D.Pages.Admin.Categorias
{
    public class EditarModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditarModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Propiedad con BindProperty
        [BindProperty]

        public Categoria Categoria { get; set; } 
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Categoria = _unitOfWork.Categoria.GetFirstOrDefault(c => c.Id == id);

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

            var categoriaBd = _unitOfWork.Categoria.GetFirstOrDefault(c => c.Id == Categoria.Id);
            if (categoriaBd == null)
            {
                return NotFound();
            }

            // Actualizalos camplos modificables

            categoriaBd.OrdenVisualizacion=Categoria.OrdenVisualizacion;
            categoriaBd.Nombre=Categoria.Nombre;

            //Guardar Cambios

            _unitOfWork.Save();
            TempData["Success"] = "La categoría se ha editado correctamente.";
            return RedirectToPage("Index");
        }
    }
}
