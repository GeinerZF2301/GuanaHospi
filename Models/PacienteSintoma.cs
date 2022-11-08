using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("PacienteSintoma")]
    public partial class PacienteSintoma
    {
        [Key]
        [Column("Id_PacienteSintoma")]
        public int IdPacienteSintoma { get; set; }
        [Column("Id_Paciente")]
        public int? IdPaciente { get; set; }
        [Column("Id_Sintoma")]
        public int? IdSintoma { get; set; }

        [ForeignKey(nameof(IdPaciente))]
        [InverseProperty(nameof(Paciente.PacienteSintomas))]
        public virtual Paciente IdPacienteNavigation { get; set; }
        [ForeignKey(nameof(IdSintoma))]
        [InverseProperty(nameof(Sintoma.PacienteSintomas))]
        public virtual Sintoma IdSintomaNavigation { get; set; }
    }
}
