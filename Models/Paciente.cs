using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("Paciente")]
    public partial class Paciente
    {
        public Paciente()
        {
            Cita = new HashSet<Cita>();
            EnfermedadPacientes = new HashSet<EnfermedadPaciente>();
            PacienteIntervencions = new HashSet<PacienteIntervencion>();
            PacienteSintomas = new HashSet<PacienteSintoma>();
            PacienteTratamientos = new HashSet<PacienteTratamiento>();
        }

        [Key]
        [Column("Id_Paciente")]
        public int IdPaciente { get; set; }
        [Required]
        [StringLength(10)]
        public string Cedula { get; set; }
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(30)]
        public string Apellido1 { get; set; }
        [Required]
        [StringLength(30)]
        public string Apellido2 { get; set; }
        [Column(TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [StringLength(3)]
        public string Edad { get; set; }
        [Required]
        [Column("carnet")]
        [StringLength(10)]
        public string Carnet { get; set; }
        [Column("Id_Unidad")]
        public int? IdUnidad { get; set; }

        [ForeignKey(nameof(IdUnidad))]
        [InverseProperty(nameof(Unidad.Pacientes))]
        public virtual Unidad IdUnidadNavigation { get; set; }
        [InverseProperty(nameof(Models.Cita.IdPacienteNavigation))]
        public virtual ICollection<Cita> Cita { get; set; }
        [InverseProperty(nameof(EnfermedadPaciente.IdPacienteNavigation))]
        public virtual ICollection<EnfermedadPaciente> EnfermedadPacientes { get; set; }
        [InverseProperty(nameof(PacienteIntervencion.IdPacienteNavigation))]
        public virtual ICollection<PacienteIntervencion> PacienteIntervencions { get; set; }
        [InverseProperty(nameof(PacienteSintoma.IdPacienteNavigation))]
        public virtual ICollection<PacienteSintoma> PacienteSintomas { get; set; }
        [InverseProperty(nameof(PacienteTratamiento.IdPacienteNavigation))]
        public virtual ICollection<PacienteTratamiento> PacienteTratamientos { get; set; }
    }
}
