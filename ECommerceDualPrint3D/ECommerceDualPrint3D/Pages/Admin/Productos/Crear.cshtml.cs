using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceDualPrint3D.Pages.Admin.Productos
{
    public class CrearModel : PageModel
    {
        //Inyeccion de dependencias
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public CrearModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
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

        public IActionResult OnGet()
        {
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
               ModelState.AddModelError(string.Empty, "No hay categor�as disponibles. Por favor, crea una categor�a antes de a�adir productos.");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //Validacion personalizada: Comprobar si el nombre de la categor�a ya existe 2.2v

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

                // Limitar el tama�o del archivo 
                if (Producto.ImagenSubida.Length > 2 * 1024 * 1024) // 2 MB
                {
                    ModelState.AddModelError("ImagenSubida", "El tama�o del archivo no debe superar los 2 MB.");
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
                    await Producto.ImagenSubida.CopyToAsync(fileStream);
                }
                //Guardar la ruta de la imagen en el objeto Producto
                Producto.Imagen = uniqueFileName;
            }

            // Asignar la fecha de creaci�n

            Producto.FechaCreacion = DateTime.Now;

            _unitOfWork.Producto.Add(Producto);
            _unitOfWork.Save();

            //TempData para mensaje al volver al Index
            TempData["Success"] = "El producto se ha creado correctamente.";

            return RedirectToPage("Index");
        }
    }
}
