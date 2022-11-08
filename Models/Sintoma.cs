using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("Sintoma")]
    public partial class Sintoma
    {
        public Sintoma()
        {
            EnfermedadSintomas = new HashSet<EnfermedadSintoma>();
            PacienteSintomas = new HashSet<PacienteSintoma>();
            SintomaIntervencions = new HashSet<SintomaIntervencion>();
        }

        [Key]
        [Column("Id_Sintoma")]
        public int IdSintoma { get; set; }
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(40)]
        public string Descripcion { get; set; }

        [InverseProperty(nameof(EnfermedadSintoma.IdSintomaNavigation))]
        public virtual ICollection<EnfermedadSintoma> EnfermedadSintomas { get; set; }
        [InverseProperty(nameof(PacienteSintoma.IdSintomaNavigation))]
        public virtual ICollection<PacienteSintoma> PacienteSintomas { get; set; }
        [InverseProperty(nameof(SintomaIntervencion.IdSintomaNavigation))]
        public virtual ICollection<SintomaIntervencion> SintomaIntervencions { get; set; }
    }
}
