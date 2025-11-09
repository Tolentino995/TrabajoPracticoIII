using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ECommerceDualPrint3D.Pages.Cliente.Inicio
{
    public class DetalleModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetalleModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public CarritoCompra CarritoCompra { get; set; }
        public IActionResult OnGet(int id)
        {
            CarritoCompra = new CarritoCompra()
            {
                Producto = _unitOfWork.Producto.GetFirstOrDefault(p => p.Id == id, "Categoria")
            };
            
            if(CarritoCompra == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        //public IActionResult OnPostAgregarAlCarrito()
        //{
        //    if (Cantidad < 1 || Cantidad > Producto.CantidadDisponible)
        //    {
        //        ModelState.AddModelError("Cantidad", $"Debe ingresar un valor entre 1 y {Producto.CantidadDisponible}.");
        //        return Page();
        //    }

        //    // logica de agregar al carrito
        //    TempData["Succes"] = $"{Cantidad} unidad(es) del Producto {Producto.Nombre} añadidas al carrito";
        //    return RedirectToPage("/Index");
        //}
    }
}
