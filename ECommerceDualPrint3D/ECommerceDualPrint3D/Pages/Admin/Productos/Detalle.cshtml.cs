using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceDualPrint3D.Pages.Admin.Productos
{
    public class DetalleModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetalleModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Producto Producto { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Producto = _unitOfWork.Producto.GetFirstOrDefault(c => c.Id == id);
            if (Producto == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
