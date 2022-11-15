using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    public partial class Cita
    {
        [Key]
        [Column("Id_Cita")]
        public int IdCita { get; set; }
        [Column(TypeName = "date")]
        public DateTime FechaIngreso { get; set; }
        [Column(TypeName = "date")]
        public DateTime FechaSalida { get; set; }
        [Required]
        [StringLength(40)]
        public string Observaciones { get; set; }
        [Column("Id_Doctor")]
        public int? IdDoctor { get; set; }
        [Column("Id_Paciente")]
        public int? IdPaciente { get; set; }
        [Column("Id_Secretaria")]
        public int? IdSecretaria { get; set; }

        [ForeignKey(nameof(IdDoctor))]
        [InverseProperty(nameof(Doctor.Cita))]
        public virtual Doctor IdDoctorNavigation { get; set; }
        [ForeignKey(nameof(IdPaciente))]
        [InverseProperty(nameof(Paciente.Cita))]
        public virtual Paciente IdPacienteNavigation { get; set; }
        [ForeignKey(nameof(IdSecretaria))]
        [InverseProperty(nameof(Secretaria.Citas))]
        public virtual Secretaria IdSecretariaNavigation { get; set; }
    }
}
