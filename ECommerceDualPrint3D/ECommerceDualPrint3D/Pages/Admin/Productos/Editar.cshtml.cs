using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceDualPrint3D.Pages.Admin.Productos
{
    public class EditarModel : PageModel
    {
        //Inyeccion de dependencias
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public EditarModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        //Propiedad con BindProperty
        [BindProperty]
        public Producto Producto { get; set; } = default!;
        [BindProperty]
        public IFormFile? ImagenSubida { get; set; }

        // Lista de categoria para un dropdown
        public IEnumerable<SelectListItem> Categorias { get; set; }

        public IActionResult OnGet(int id)
        {
            // Cargar el producto desde la base de datos
            Producto = _unitOfWork.Producto.GetFirstOrDefault(p => p.Id == id)!;

            if (Producto == null)
            {
                return NotFound();
            }
            // Cargar las categorias desde la base de datos 
            Categorias = _unitOfWork.Categoria.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                });
            //Validacion de la tabla Categoria si no hay categorias
            if (!Categorias.Any())
            {
                ModelState.AddModelError(string.Empty, "No hay categorías disponibles. Por favor, crea una categoría antes de añadir productos.");
            }
            return Page();
        }
    }
}
