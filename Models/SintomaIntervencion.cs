using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("SintomaIntervencion")]
    public partial class SintomaIntervencion
    {
        [Key]
        [Column("Id_SintomaIntervencion")]
        public int IdSintomaIntervencion { get; set; }
        [Column("Id_Intervencion")]
        public int? IdIntervencion { get; set; }
        [Column("Id_Sintoma")]
        public int? IdSintoma { get; set; }

        [ForeignKey(nameof(IdIntervencion))]
        [InverseProperty(nameof(Intervencion.SintomaIntervencions))]
        [Display(Name = "Intervencion")]
        public virtual Intervencion IdIntervencionNavigation { get; set; }
        [Display(Name = "Sintoma")]
        [ForeignKey(nameof(IdSintoma))]
        [InverseProperty(nameof(Sintoma.SintomaIntervencions))]
        public virtual Sintoma IdSintomaNavigation { get; set; }
    }
}
