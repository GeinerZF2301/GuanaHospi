using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using GuanaHospi.Models;

#nullable disable

namespace GuanaHospi.Data
{
    public partial class GuanaHospiContext : DbContext
    {
        public GuanaHospiContext()
        {
        }

        public GuanaHospiContext(DbContextOptions<GuanaHospiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cita> Cita { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorEspecialidad> DoctorEspecialidades { get; set; }
        public virtual DbSet<DoctorTratamiento> DoctorTratamientos { get; set; }
        public virtual DbSet<Enfermedad> Enfermedads { get; set; }
        public virtual DbSet<EnfermedadPaciente> EnfermedadPacientes { get; set; }
        public virtual DbSet<EnfermedadSintoma> EnfermedadSintomas { get; set; }
        public virtual DbSet<EnfermedadTipo> EnfermedadTipos { get; set; }
        public virtual DbSet<Especialidad> Especialidades { get; set; }
        public virtual DbSet<Intervencion> Intervencions { get; set; }
        public virtual DbSet<IntervencionTipo> IntervencionTipos { get; set; }
        public virtual DbSet<Paciente> Pacientes { get; set; }
        public virtual DbSet<PacienteIntervencion> PacienteIntervencions { get; set; }
        public virtual DbSet<PacienteSintoma> PacienteSintomas { get; set; }
        public virtual DbSet<PacienteTratamiento> PacienteTratamientos { get; set; }
        public virtual DbSet<Secretaria> Secretaria { get; set; }
        public virtual DbSet<Sintoma> Sintomas { get; set; }
        public virtual DbSet<SintomaIntervencion> SintomaIntervencions { get; set; }
        public virtual DbSet<TipoEnfermedad> TipoEnfermedades { get; set; }
        public virtual DbSet<TipoIntervencion> TipoIntervenciones { get; set; }
        public virtual DbSet<TipoTratamiento> TipoTratamientos { get; set; }
        public virtual DbSet<Tratamiento> Tratamientos { get; set; }
        public virtual DbSet<TratamientoIntervencion> TratamientoIntervenciones { get; set; }
        public virtual DbSet<TratamientoTipo> TratamientoTipos { get; set; }
        public virtual DbSet<Unidad> Unidades { get; set; }

    }
}
