using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "La ciudad es obligatorio.")]
        public string Ciudad { get; set; }
        [Required(ErrorMessage = "La dirección es obligatorio.")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El país es obligatorio.")]
        public string Pais { get; set; }
    }
}
