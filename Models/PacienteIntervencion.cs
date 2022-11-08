using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("PacienteIntervencion")]
    public partial class PacienteIntervencion
    {
        [Key]
        [Column("Id_PacienteIntervencion")]
        public int IdPacienteIntervencion { get; set; }
        [Column("Id_Intervencion")]
        public int? IdIntervencion { get; set; }
        [Column("Id_Paciente")]
        public int? IdPaciente { get; set; }

        [ForeignKey(nameof(IdIntervencion))]
        [InverseProperty(nameof(Intervencion.PacienteIntervencions))]
        public virtual Intervencion IdIntervencionNavigation { get; set; }
        [ForeignKey(nameof(IdPaciente))]
        [InverseProperty(nameof(Paciente.PacienteIntervencions))]
        public virtual Paciente IdPacienteNavigation { get; set; }
    }
}
