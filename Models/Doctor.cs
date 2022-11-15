using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("Doctor")]
    public partial class Doctor
    {
        public Doctor()
        {
            Cita = new HashSet<Cita>();
            DoctorEspecialidads = new HashSet<DoctorEspecialidad>();
            DoctorTratamientos = new HashSet<DoctorTratamiento>();
            Intervencions = new HashSet<Intervencion>();
            Unidads = new HashSet<Unidad>();
        }

        [Key]
        [Column("Id_Doctor")]
        public int IdDoctor { get; set; }
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
        [StringLength(30)]
        public string Email { get; set; }
        [Required]
        [StringLength(10)]
        public string Salario { get; set; }
        [Column("Fecha_Contratacion", TypeName = "date")]
        public DateTime FechaContratacion { get; set; }

        [InverseProperty(nameof(Models.Cita.IdDoctorNavigation))]
        public virtual ICollection<Cita> Cita { get; set; }
        [InverseProperty(nameof(DoctorEspecialidad.IdDoctorNavigation))]
        public virtual ICollection<DoctorEspecialidad> DoctorEspecialidads { get; set; }
        [InverseProperty(nameof(DoctorTratamiento.IdDoctorNavigation))]
        public virtual ICollection<DoctorTratamiento> DoctorTratamientos { get; set; }
        [InverseProperty(nameof(Intervencion.IdDoctorNavigation))]
        public virtual ICollection<Intervencion> Intervencions { get; set; }
        [InverseProperty(nameof(Unidad.IdDoctorNavigation))]
        public virtual ICollection<Unidad> Unidads { get; set; }
    }
}
