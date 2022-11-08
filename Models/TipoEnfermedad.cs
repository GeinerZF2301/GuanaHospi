using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("TipoEnfermedad")]
    public partial class TipoEnfermedad
    {
        public TipoEnfermedad()
        {
            EnfermedadTipos = new HashSet<EnfermedadTipo>();
        }

        [Key]
        [Column("Id_TipoEnfermedad")]
        public int IdTipoEnfermedad { get; set; }
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        [InverseProperty(nameof(EnfermedadTipo.IdTipoEnfermedadNavigation))]
        public virtual ICollection<EnfermedadTipo> EnfermedadTipos { get; set; }
    }
}
