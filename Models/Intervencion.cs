using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("Intervencion")]
    public partial class Intervencion
    {
        public Intervencion()
        {
            IntervencionTipos = new HashSet<IntervencionTipo>();
            PacienteIntervencions = new HashSet<PacienteIntervencion>();
            SintomaIntervencions = new HashSet<SintomaIntervencion>();
            TratamientoIntervencions = new HashSet<TratamientoIntervencion>();
        }

        [Key]
        [Column("Id_Intervencion")]
        public int IdIntervencion { get; set; }
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(40)]
        public string Descripcion { get; set; }
        [Column(TypeName = "date")]
        public DateTime FechaIntervencion { get; set; }
        public TimeSpan Hora { get; set; }
        [Column("Id_Doctor")]
        public int? IdDoctor { get; set; }

        [ForeignKey(nameof(IdDoctor))]
        [InverseProperty(nameof(Doctor.Intervencions))]
        public virtual Doctor IdDoctorNavigation { get; set; }
        [InverseProperty(nameof(IntervencionTipo.IdIntervencionNavigation))]
        public virtual ICollection<IntervencionTipo> IntervencionTipos { get; set; }
        [InverseProperty(nameof(PacienteIntervencion.IdIntervencionNavigation))]
        public virtual ICollection<PacienteIntervencion> PacienteIntervencions { get; set; }
        [InverseProperty(nameof(SintomaIntervencion.IdIntervencionNavigation))]
        public virtual ICollection<SintomaIntervencion> SintomaIntervencions { get; set; }
        [InverseProperty(nameof(TratamientoIntervencion.IdIntervencionNavigation))]
        public virtual ICollection<TratamientoIntervencion> TratamientoIntervencions { get; set; }
    }
}
