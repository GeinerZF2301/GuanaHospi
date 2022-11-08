using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("Tratamiento")]
    public partial class Tratamiento
    {
        public Tratamiento()
        {
            DoctorTratamientos = new HashSet<DoctorTratamiento>();
            PacienteTratamientos = new HashSet<PacienteTratamiento>();
            TratamientoIntervencions = new HashSet<TratamientoIntervencion>();
            TratamientoTipos = new HashSet<TratamientoTipo>();
        }

        [Key]
        [Column("Id_Tratamiento")]
        public int IdTratamiento { get; set; }
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(40)]
        public string Descripcion { get; set; }

        [InverseProperty(nameof(DoctorTratamiento.IdTratamientoNavigation))]
        public virtual ICollection<DoctorTratamiento> DoctorTratamientos { get; set; }
        [InverseProperty(nameof(PacienteTratamiento.IdTratamientoNavigation))]
        public virtual ICollection<PacienteTratamiento> PacienteTratamientos { get; set; }
        [InverseProperty(nameof(TratamientoIntervencion.IdTratamientoNavigation))]
        public virtual ICollection<TratamientoIntervencion> TratamientoIntervencions { get; set; }
        [InverseProperty(nameof(TratamientoTipo.IdTratamientoNavigation))]
        public virtual ICollection<TratamientoTipo> TratamientoTipos { get; set; }
    }
}
