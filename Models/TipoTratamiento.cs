using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("TipoTratamiento")]
    public partial class TipoTratamiento
    {
        public TipoTratamiento()
        {
            TratamientoTipos = new HashSet<TratamientoTipo>();
        }

        [Key]
        [Column("Id_TipoTratamiento")]
        public int IdTipoTratamiento { get; set; }
        [Required]
        [StringLength(30)]
        public string NombreTipo { get; set; }

        [InverseProperty(nameof(TratamientoTipo.IdTipoTratamientoNavigation))]
        public virtual ICollection<TratamientoTipo> TratamientoTipos { get; set; }
    }
}
