using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuanaHospi.Models.ViewModels
{
    public class Historico
    {
        [Required]
        [Display(Name = "Fecha")]
        public string Fecha { get; set; }
        [Required]
        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }
    }
}
