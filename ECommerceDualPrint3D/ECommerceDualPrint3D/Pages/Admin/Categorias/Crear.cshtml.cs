using ECommerce.DataAccess;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceDualPrint3D.Pages.Admin.Categorias
{
    public class CrearModel : PageModel
    {
        //Inyeccion de dependencias
        private readonly IUnitOfWork _unitOfWork;

        public CrearModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            // Validación personalizada: Comprobar si el nombre de la categoría ya existe

            //bool nombreExiste = _bdCategoria.Categorias.Any(c => c.Nombre == Categoria.Nombre);
            //if (nombreExiste)
            //{
            //    ModelState.AddModelError("Categoria.Nombre", "El nombre de la categoría ya existe. Por favor, elige otro nombre.");
            //    return Page();
            //}

            //Validacion personalizada: Comprobar si el nombre de la categoría ya existe 2.2v

            if (_unitOfWork.Categoria.ExisteNombre(Categoria.Nombre))
            {
                ModelState.AddModelError("Categoria.Nombre", "El nombre de la categoría ya existe. Por favor, elige otro nombre.");
                return Page();

            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Asignar la fecha de creación

            Categoria.FechaCreacion = DateTime.Now;

            _unitOfWork.Categoria.Add(Categoria);
            _unitOfWork.Save();

            //TempData para mensaje al volver al Index
            TempData["Success"] = "La categoría se ha creado correctamente.";

            return RedirectToPage("Index");
        }
    }
}
