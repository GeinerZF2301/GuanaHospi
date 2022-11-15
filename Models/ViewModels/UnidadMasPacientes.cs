using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuanaHospi.Models.ViewModels
{
    public class UnidadMasPacientes
    {
        [Required]
        [Display(Name ="Id Unidad")]
        public int IdUnidad { get; set; }
        [Required]
        [Display(Name = "Nombre de la Unidad")]
        public string NombreUnidad { get; set; }
        [Required]
        [Display(Name = "Cantidad de Pacientes")]
        public int CantidadPacientes { get; set; }
    }
}
