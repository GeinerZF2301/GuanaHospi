using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("Enfermedad")]
    public partial class Enfermedad
    {
        public Enfermedad()
        {
            EnfermedadPacientes = new HashSet<EnfermedadPaciente>();
            EnfermedadSintomas = new HashSet<EnfermedadSintoma>();
            EnfermedadTipos = new HashSet<EnfermedadTipo>();
        }

        [Key]
        [Column("Id_Enfermedad")]
        public int IdEnfermedad { get; set; }
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(40)]
        public string Descripcion { get; set; }

        [InverseProperty(nameof(EnfermedadPaciente.IdEnfermedadNavigation))]
        public virtual ICollection<EnfermedadPaciente> EnfermedadPacientes { get; set; }
        [InverseProperty(nameof(EnfermedadSintoma.IdEnfermedadNavigation))]
        public virtual ICollection<EnfermedadSintoma> EnfermedadSintomas { get; set; }
        [InverseProperty(nameof(EnfermedadTipo.IdEnfermedadNavigation))]
        public virtual ICollection<EnfermedadTipo> EnfermedadTipos { get; set; }
    }
}
