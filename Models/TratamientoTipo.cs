using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("TratamientoTipo")]
    public partial class TratamientoTipo
    {
        [Key]
        [Column("Id_TratamientoTipo")]
        public int IdTratamientoTipo { get; set; }
        [Column("Id_TipoTratamiento")]
        [Display(Name ="Tipo de Tratamiento")]
        public int? IdTipoTratamiento { get; set; }
        [Display(Name = "Nombre del Tratamiento")]
        [Column("Id_Tratamiento")]
        public int? IdTratamiento { get; set; }

        [ForeignKey(nameof(IdTipoTratamiento))]
        [InverseProperty(nameof(TipoTratamiento.TratamientoTipos))]
        [Display(Name = "Tipo de Tratamiento")]
        public virtual TipoTratamiento IdTipoTratamientoNavigation { get; set; }
        [ForeignKey(nameof(IdTratamiento))]
        [InverseProperty(nameof(Tratamiento.TratamientoTipos))]
        [Display(Name = "Nombre del Tratamiento")]
        public virtual Tratamiento IdTratamientoNavigation { get; set; }
    }
}
