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

        public async Task<IActionResult> OnPostAsync()
        {
            // Siempre recargamos categorías, por si hay errores de validación
            Categorias = _unitOfWork.Categoria.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                });

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
            //Subir imagen
            if (Producto.ImagenSubida != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "productos");
                //En el caso de que se suba una imagen con el mismo nombre, se genera un nombre unico
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Producto.ImagenSubida.FileName);

                //Validar si el directorio existe, si no se crearia
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Limitar el tamaño del archivo 
                if (Producto.ImagenSubida.Length > 2 * 1024 * 1024) // 2 MB
                {
                    ModelState.AddModelError("ImagenSubida", "El tamaño del archivo no debe superar los 2 MB.");
                    return Page();
                }

                //Extensiones permitidas
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                if (!allowedExtensions.Contains(Path.GetExtension(Producto.ImagenSubida.FileName).ToLower()))
                {
                    ModelState.AddModelError("ImagenSubida", "Solo se permiten archivos con las siguientes extensiones: .jpg, .jpeg, .png, .gif.");
                    return Page();
                }

                //Guardar la ruta relativa en la base de datos
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Producto.ImagenSubida.CopyTo(fileStream);
                }
                //Eliminar la imagen anterior si existe
                if(!string.IsNullOrEmpty(Producto.Imagen))
                {
                    string oldFilePath = Path.Combine(uploadsFolder, Producto.Imagen);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                //actaliza la ruta de la imagen en el objeto Producto
                Producto.Imagen = uniqueFileName;

            }
            else
            {
                //Recuperar la imagen actual desde la base de datos si no se sube una nueva
                var productoDesdeDb = _unitOfWork.Producto.GetFirstOrDefault(p => p.Id == Producto.Id);
                if (productoDesdeDb != null)
                {
                    Producto.Imagen = productoDesdeDb.Imagen;
                }
            }

            // Quitar etiquetas HTML, espacios y entidades antes de contar caracteres
            string descripcion = Producto.Descripcion ?? "";

            // 1. Eliminar etiquetas HTML
            descripcion = System.Text.RegularExpressions.Regex.Replace(descripcion, "<[^>]+>", string.Empty);

            // 2. Decodificar entidades HTML (&nbsp;, &amp;, etc.)
            descripcion = System.Web.HttpUtility.HtmlDecode(descripcion);

            // 3. Reemplazar saltos de línea y espacios repetidos
            descripcion = System.Text.RegularExpressions.Regex.Replace(descripcion, @"\s+", " ").Trim();

            // 4. Validar longitud limpia
            if (descripcion.Length > 500)
            {
                ModelState.AddModelError("Producto.Descripcion", $"La descripción no puede superar los 500 caracteres (actual: {descripcion.Length}).");
                return Page();
            }

            // Asignar la fecha de creación

            Producto.FechaCreacion = DateTime.Now;

            _unitOfWork.Producto.Update(Producto);
            _unitOfWork.Save();

            //TempData para mensaje al volver al Index
            TempData["Success"] = "El producto actualizado correctamente.";

            return RedirectToPage("Index");
        }
    }
}
