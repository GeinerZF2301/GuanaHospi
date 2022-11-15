using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("Usuario")]
    public partial class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string NombreUsuario { get; set; }
        [Required]
        [StringLength(50)]
        public string CorreoElectronico { get; set; }
        [Required]
        [StringLength(100)]
        public string Contrasenna { get; set; }
        public string ConfirmarContrasenna { get; set; }
        [NotMapped]
        public string[] Roles { get; set; }
    }
}
