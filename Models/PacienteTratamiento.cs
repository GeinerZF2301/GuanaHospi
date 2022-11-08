using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("PacienteTratamiento")]
    public partial class PacienteTratamiento
    {
        [Key]
        [Column("Id_PacienteTratamiento")]
        public int IdPacienteTratamiento { get; set; }
        [Column("Id_Paciente")]
        public int? IdPaciente { get; set; }
        [Column("Id_Tratamiento")]
        public int? IdTratamiento { get; set; }

        [ForeignKey(nameof(IdPaciente))]
        [InverseProperty(nameof(Paciente.PacienteTratamientos))]
        public virtual Paciente IdPacienteNavigation { get; set; }
        [ForeignKey(nameof(IdTratamiento))]
        [InverseProperty(nameof(Tratamiento.PacienteTratamientos))]
        public virtual Tratamiento IdTratamientoNavigation { get; set; }
    }
}
