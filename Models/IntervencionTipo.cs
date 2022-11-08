using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("IntervencionTipo")]
    public partial class IntervencionTipo
    {
        [Key]
        [Column("Id_IntervencionTipo")]
        public int IdIntervencionTipo { get; set; }
        [Column("Id_Intervencion")]
        public int? IdIntervencion { get; set; }
        [Column("Id_TipoIntervencion")]
        public int? IdTipoIntervencion { get; set; }

        [ForeignKey(nameof(IdIntervencion))]
        [InverseProperty(nameof(Intervencion.IntervencionTipos))]
        public virtual Intervencion IdIntervencionNavigation { get; set; }
        [ForeignKey(nameof(IdTipoIntervencion))]
        [InverseProperty(nameof(TipoIntervencion.IntervencionTipos))]
        public virtual TipoIntervencion IdTipoIntervencionNavigation { get; set; }
    }
}
