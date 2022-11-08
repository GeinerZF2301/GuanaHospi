using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("Unidad")]
    public partial class Unidad
    {
        public Unidad()
        {
            Pacientes = new HashSet<Paciente>();
        }

        [Key]
        [Column("Id_Unidad")]
        public int IdUnidad { get; set; }
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(20)]
        public string Planta { get; set; }
        [Column("Id_Doctor")]
        public int? IdDoctor { get; set; }

        [ForeignKey(nameof(IdDoctor))]
        [InverseProperty(nameof(Doctor.Unidads))]
        public virtual Doctor IdDoctorNavigation { get; set; }
        [InverseProperty(nameof(Paciente.IdUnidadNavigation))]
        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
