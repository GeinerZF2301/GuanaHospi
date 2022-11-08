using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("DoctorEspecialidad")]
    public partial class DoctorEspecialidad
    {
        [Key]
        [Column("Id_DoctorEspecialidad")]
        public int IdDoctorEspecialidad { get; set; }
        [Column("Id_Especialidad")]
        public int? IdEspecialidad { get; set; }
        [Column("Id_Doctor")]
        public int? IdDoctor { get; set; }

        [ForeignKey(nameof(IdDoctor))]
        [InverseProperty(nameof(Doctor.DoctorEspecialidads))]
        public virtual Doctor IdDoctorNavigation { get; set; }
        [ForeignKey(nameof(IdEspecialidad))]
        [InverseProperty(nameof(Especialidad.DoctorEspecialidads))]
        public virtual Especialidad IdEspecialidadNavigation { get; set; }
    }
}
