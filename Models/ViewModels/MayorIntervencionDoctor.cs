using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuanaHospi.Models.ViewModels
{
    public class MayorIntervencionDoctor
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre del Doctor")]
        public string NombreDoctor { get; set; }
        [Required]
        [Display(Name = "Cantidad de Intervenciones")]
        public int CantidadIntervencion { get; set; }

    }
}
