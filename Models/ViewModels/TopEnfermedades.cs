using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuanaHospi.Models.ViewModels
{
    public class TopEnfermedades
    {
        [Required]
        [Display(Name = "Nombre de la Enfermedad")]
        public string NombreEnfermedad { get; set; }
        [Required]
        [Display(Name = "Sintomas")]
        public int Sintoma { get; set; }
    }
}
