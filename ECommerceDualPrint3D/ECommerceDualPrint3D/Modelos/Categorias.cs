using System.ComponentModel.DataAnnotations;

namespace ECommerceDualPrint3D.Modelos
{
    public class Categorias
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [StringLength(100,ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        [Display(Nombre="Nombre de la categoría")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        [Display(Name = "Nombre de la categoría")]
        public string OrdenVisualizacion { get; set; }

    }
}
