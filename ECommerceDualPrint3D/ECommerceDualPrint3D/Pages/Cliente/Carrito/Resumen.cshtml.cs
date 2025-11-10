using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ECommerceDualPrint3D.Pages.Cliente.Carrito
{
    [Authorize]
    public class ResumenModel : PageModel
    {
        public IEnumerable<CarritoCompra> ListaCarritoCompra { get; set; }

        public Orden Orden { get; set; }

        public double TotalCarrito { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public ResumenModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Orden = new Orden();
        }
        public void OnGet()
        {
            //Obtenemos el Id del usuario logueado
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                //Obtenemos la lista del carrito de compras del usuario logueado
                ListaCarritoCompra = _unitOfWork.CarritoCompra.GetAll(
                    filter: u => u.ApplicationUserId == claim.Value,
                    "Producto,Producto.Categoria");

                foreach (var itemCarrito in ListaCarritoCompra)
                {
                    Orden.TotalOrden += (double)(itemCarrito.Producto.Precio * itemCarrito.Cantidad);
                }
                //Obtenemos los datos del usuario logueado
                ApplicationUser applicationUser = _unitOfWork.ApplicationUser
                    .GetFirstOrDefault(u => u.Id == claim.Value);

                Orden.NombreUsuario = applicationUser.Nombres + " " + applicationUser.Apellidos;
                Orden.Direccion = applicationUser.Direccion;
                Orden.Telefono = applicationUser.PhoneNumber;
                Orden.InstruccionesAdicionales = Orden.InstruccionesAdicionales;
            }
        }
    }
}
