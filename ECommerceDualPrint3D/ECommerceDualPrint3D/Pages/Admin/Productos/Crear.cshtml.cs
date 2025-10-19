using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceDualPrint3D.Pages.Admin.Productos
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

        public Producto Producto { get; set; } = default!;
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //Validacion personalizada: Comprobar si el nombre de la categoría ya existe 2.2v

            if (_unitOfWork.Categoria.ExisteNombre(Producto.Nombre))
            {
                ModelState.AddModelError("Producto.Nombre", "El nombre del producto ya existe. Por favor, elige otro nombre.");
                return Page();

            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Asignar la fecha de creación

            Producto.FechaCreacion = DateTime.Now;

            _unitOfWork.Producto.Add(Producto);
            _unitOfWork.Save();

            //TempData para mensaje al volver al Index
            TempData["Success"] = "El producto se ha creado correctamente.";

            return RedirectToPage("Index");
        }
    }
}
