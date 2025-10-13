using ECommerce.DataAccess;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceDualPrint3D.Pages.Admin.Categorias
{
    public class IndexModel : PageModel
    {
        //Inyeccion de dependencias
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Categoria> Categorias { get; set; } = default;
        public void OnGet()
        {
            //Cargamos todas las categorias desde la base de datos
            Categorias = _unitOfWork.Categoria.GetAll();
        }
        public async Task<IActionResult> OnPostDeleteAsync([FromBody]int id)
        {
            var categoria = _unitOfWork.Categoria.GetFirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                TempData["Error"] = "La categoría no fue encontrada";
                return RedirectToPage("Index");

            }

            _unitOfWork.Categoria.Remove(categoria);
            _unitOfWork.Save();
            TempData["Success"] = "Categoria Eliminada Correctamente";
            return new JsonResult(new { success = true });
        }
    }
}
