using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Security.Claims;

namespace ECommerceDualPrint3D.Pages.Cliente.Inicio
{
    [Authorize]
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
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CarritoCompra = new ()
            {
                ApplicationUserId = claim.Value,
                Producto = _unitOfWork.Producto.GetFirstOrDefault(p => p.Id == id, "Categoria"),
                ProductoId = id
            };
            
            if(CarritoCompra == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
        //    if (Cantidad < 1 || Cantidad > Producto.CantidadDisponible)
        //    {
        //        ModelState.AddModelError("Cantidad", $"Debe ingresar un valor entre 1 y {Producto.CantidadDisponible}.");
        //        return Page();
        //    }
            if (ModelState.IsValid)
            {
                // Carga de producto
                var producto = _unitOfWork.Producto.
                    GetFirstOrDefault(p => p.Id == CarritoCompra.ProductoId);

                if (producto == null)
                {
                    return NotFound("No se encontró el producto relacionado.");
                }
                // Validar si el inventario es 0
                if (producto.CantidadDisponible <= 0)
                {
                    TempData["Error"] = "El producto ya no tiene inventario disponible.";
                    return RedirectToAction("Detalle", new { id = CarritoCompra.ProductoId });
                }


                // Validar cantidad del carrito sea mayo
                if (CarritoCompra.Cantidad < 1 || CarritoCompra.Cantidad > producto.CantidadDisponible)
                {
                    // ModelState.AddModelError("Cantidad", $"Debe ingresar un valor entre 1 y {Producto.CantidadDisponible}.");
                    TempData["Error"] = $"Debe ingresar un valor entre 1 y {producto.CantidadDisponible}";
                    return RedirectToAction("Detalle", new { id = CarritoCompra.ProductoId });
                }
                // Reducir la cantidad disponible
                producto.CantidadDisponible -= CarritoCompra.Cantidad;
                // Logica para Agrega al carrito
                CarritoCompra carritoCompraDesdeBd = _unitOfWork.CarritoCompra.GetFirstOrDefault(
                     filter: u => u.ApplicationUserId == CarritoCompra.ApplicationUserId 
                    && u.ProductoId == CarritoCompra.ProductoId);
                if (carritoCompraDesdeBd == null)
                {
                    _unitOfWork.CarritoCompra.Add(CarritoCompra);
                    _unitOfWork.Save();
                    TempData["Success"] = $"{CarritoCompra.Cantidad} unidad(es) añadidas al carrito";
                }
                else
                {
                    _unitOfWork.CarritoCompra.IncrementarContador(carritoCompraDesdeBd, CarritoCompra.Cantidad);
                    TempData["Success"] = $"{CarritoCompra.Cantidad} unidad(es) actualizadas al carrito";
                }
                return RedirectToPage("/Cliente/Inicio/Index");

            }
            return Page();
        }
    }
}
