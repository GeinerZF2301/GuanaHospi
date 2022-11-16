using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuanaHospi.Models.ViewModels
{
    public class DetalleIntervencion
    {
        [Required]
        [Display(Name = "Nombre de la Intervencion")]
        public string NombreIntervencion { get; set; }
        [Required]
        [Display(Name = "Descripcion")]
        public string  Descripcion { get; set; }
        [Required]
        [Display(Name = "Mas Frecuente")]
        public int MasFrecuente { get; set; }
    }
}
