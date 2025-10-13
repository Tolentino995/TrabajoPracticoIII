using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage="El nombre es obligatorio.")]
        [StringLength(100,ErrorMessage = "El nombre no puede superar las 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatorio")]
        [StringLength(100, ErrorMessage = "El descripción no puede superar las 500 caracteres.")]
        public string Descripsion { get; set; }

        [Required(ErrorMessage = "La imagen es obligatorio")]
        [StringLength(300, ErrorMessage = "El ruta de la imagen no puede superar las 300 caracteres.")]
        public string Imagen { get; set; }

        [Required(ErrorMessage = "La precio es obligatorio")]
        [Range(0.01,double.MaxValue ,ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        // En caso de llevar el control a otro nivel se puede cambiar el inventario de forma externa
        [Required(ErrorMessage = "La cantidad es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "La cantidad no puede ser negativo")]
        public int CantidadDisponible { get; set; } // Controlar el inventario
        [Required(ErrorMessage = "La categoría es obligatorio")]
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
    }
}
