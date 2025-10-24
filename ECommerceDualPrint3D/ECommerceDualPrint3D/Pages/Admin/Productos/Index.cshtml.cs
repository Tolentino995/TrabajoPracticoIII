using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceDualPrint3D.Pages.Admin.Productos
{
    public class IndexModel : PageModel
    {
        //Inyeccion de dependencias
        private readonly IUnitOfWork _unitOfWork;
        

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Producto> Productos { get; set; } = default;
        public void OnGet()
        {
            //Cargamos todas las categorias desde la base de datos
            Productos = _unitOfWork.Producto.GetAll("Categoria");
        }

        public async Task<IActionResult> OnPostDeleteAsync([FromBody] int id)
        {
            var producto = _unitOfWork.Producto.GetFirstOrDefault(c => c.Id == id);
            if (producto == null)
            {
                TempData["Error"] = "El producto no fue encontrada";
                return RedirectToPage("Index");

            }

            _unitOfWork.Producto.Remove(producto);
            _unitOfWork.Save();
            TempData["Success"] = "Producto Eliminado Correctamente";
            return new JsonResult(new { success = true });
        }
    }
}

