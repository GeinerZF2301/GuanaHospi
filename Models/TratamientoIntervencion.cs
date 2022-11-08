using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("TratamientoIntervencion")]
    public partial class TratamientoIntervencion
    {
        [Key]
        [Column("Id_TratamientoIntervencion")]
        public int IdTratamientoIntervencion { get; set; }
        [Column("Id_Intervencion")]
        public int? IdIntervencion { get; set; }
        [Column("Id_Tratamiento")]
        public int? IdTratamiento { get; set; }

        [ForeignKey(nameof(IdIntervencion))]
        [InverseProperty(nameof(Intervencion.TratamientoIntervencions))]
        public virtual Intervencion IdIntervencionNavigation { get; set; }
        [ForeignKey(nameof(IdTratamiento))]
        [InverseProperty(nameof(Tratamiento.TratamientoIntervencions))]
        public virtual Tratamiento IdTratamientoNavigation { get; set; }
    }
}
