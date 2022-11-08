using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    public partial class Secretaria
    {
        public Secretaria()
        {
            Citas = new HashSet<Cita>();
        }

        [Key]
        [Column("Id_Secretaria")]
        public int IdSecretaria { get; set; }
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
        [StringLength(30)]
        public string Email { get; set; }
        [Column("Fecha_Contratacion", TypeName = "date")]
        public DateTime FechaContratacion { get; set; }

        [InverseProperty(nameof(Cita.IdSecretariaNavigation))]
        public virtual ICollection<Cita> Citas { get; set; }
    }
}
