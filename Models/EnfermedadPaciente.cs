using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("EnfermedadPaciente")]
    public partial class EnfermedadPaciente
    {
        [Key]
        [Column("Id_EnfermedadPaciente")]
        public int IdEnfermedadPaciente { get; set; }
        [Column("Id_Enfermedad")]
        [Display(Name = "Enfermedad")]
        public int? IdEnfermedad { get; set; }
        [Column("Id_Paciente")]
        [Display(Name = "Identificacion del Paciente")]
        public int? IdPaciente { get; set; }

        [ForeignKey(nameof(IdEnfermedad))]
        [InverseProperty(nameof(Enfermedad.EnfermedadPacientes))]
        public virtual Enfermedad IdEnfermedadNavigation { get; set; }
        [ForeignKey(nameof(IdPaciente))]
        [InverseProperty(nameof(Paciente.EnfermedadPacientes))]
        public virtual Paciente IdPacienteNavigation { get; set; }
    }
}
