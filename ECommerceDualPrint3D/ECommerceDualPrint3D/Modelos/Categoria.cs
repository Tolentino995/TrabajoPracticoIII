using System.ComponentModel.DataAnnotations;

namespace ECommerceDualPrint3D.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [StringLength(100,ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        [Display(Name="Nombre de la categoría")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [Range(1,int.MaxValue, ErrorMessage = "El orden debe ser mayor que 0")]
        [Display(Name = "Orden de Visualizacion")]
        public int OrdenVisualizacion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;// Fecha predeterminada a la hora de la cració n  
    }
}
