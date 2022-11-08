using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("Especialidad")]
    public partial class Especialidad
    {
        public Especialidad()
        {
            DoctorEspecialidads = new HashSet<DoctorEspecialidad>();
        }

        [Key]
        [Column("Id_Especialidad")]
        public int IdEspecialidad { get; set; }
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(40)]
        public string Descripcion { get; set; }

        [InverseProperty(nameof(DoctorEspecialidad.IdEspecialidadNavigation))]
        public virtual ICollection<DoctorEspecialidad> DoctorEspecialidads { get; set; }
    }
}
