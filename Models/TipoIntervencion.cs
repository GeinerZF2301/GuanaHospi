using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("TipoIntervencion")]
    public partial class TipoIntervencion
    {
        public TipoIntervencion()
        {
            IntervencionTipos = new HashSet<IntervencionTipo>();
        }

        [Key]
        [Column("Id_TipoIntervencion")]
        public int IdTipoIntervencion { get; set; }
        [Required]
        [StringLength(20)]
        public string NombreTipo { get; set; }

        [InverseProperty(nameof(IntervencionTipo.IdTipoIntervencionNavigation))]
        public virtual ICollection<IntervencionTipo> IntervencionTipos { get; set; }
    }
}
