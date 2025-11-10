using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceDualPrint3D.Pages.Cliente.Inicio
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Producto> Productos { get; set; }
        public void OnGet()
        {
            //Usamos el método GetAll para incluir categorías relacionadas
            Productos = _unitOfWork.Producto.GetAll(filter: null, "Categoria");
        }
    }
}
