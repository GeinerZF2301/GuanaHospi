using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("DoctorTratamiento")]
    public partial class DoctorTratamiento
    {
        [Key]
        [Column("Id_DoctorTratamiento")]
        public int IdDoctorTratamiento { get; set; }
        [Column("Id_Doctor")]
        public int? IdDoctor { get; set; }
        [Column("Id_Tratamiento")]
        public int? IdTratamiento { get; set; }

        [ForeignKey(nameof(IdDoctor))]
        [InverseProperty(nameof(Doctor.DoctorTratamientos))]
        public virtual Doctor IdDoctorNavigation { get; set; }
        [ForeignKey(nameof(IdTratamiento))]
        [InverseProperty(nameof(Tratamiento.DoctorTratamientos))]
        public virtual Tratamiento IdTratamientoNavigation { get; set; }
    }
}
