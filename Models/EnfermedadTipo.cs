using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("EnfermedadTipo")]
    public partial class EnfermedadTipo
    {
        [Key]
        [Column("Id_EnfermedadTipo")]
        public int IdEnfermedadTipo { get; set; }
        [Column("Id_TipoEnfermedad")]
        public int? IdTipoEnfermedad { get; set; }
        [Column("Id_Enfermedad")]
        public int? IdEnfermedad { get; set; }

        [ForeignKey(nameof(IdEnfermedad))]
        [InverseProperty(nameof(Enfermedad.EnfermedadTipos))]
        public virtual Enfermedad IdEnfermedadNavigation { get; set; }
        [ForeignKey(nameof(IdTipoEnfermedad))]
        [InverseProperty(nameof(TipoEnfermedad.EnfermedadTipos))]
        public virtual TipoEnfermedad IdTipoEnfermedadNavigation { get; set; }
    }
}
