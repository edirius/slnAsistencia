﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapaEntities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class mAsistenciaContainer : DbContext
    {
        public mAsistenciaContainer()
            : base("name=mAsistenciaContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Trabajador> TrabajadorSet { get; set; }
        public virtual DbSet<Oficina> OficinaSet { get; set; }
        public virtual DbSet<Asistencia> AsistenciaSet { get; set; }
        public virtual DbSet<Local> LocalSet { get; set; }
        public virtual DbSet<Horario> HorarioSet { get; set; }
        public virtual DbSet<HorarioDia> HorarioDiaSet { get; set; }
        public virtual DbSet<HorarioSemana> HorarioSemanaSet { get; set; }
        public virtual DbSet<PermisosHoras> PermisosHorasSet { get; set; }
        public virtual DbSet<TipoPermisos> TipoPermisosSet { get; set; }
        public virtual DbSet<PermisosDias> PermisosDiasSet { get; set; }
        public virtual DbSet<DiaFestivo> DiaFestivoSet { get; set; }
        public virtual DbSet<CronogramaVacaciones> CronogramaVacacionesSet { get; set; }
        public virtual DbSet<Empresa> EmpresaSet { get; set; }
        public virtual DbSet<Prueba> PruebaSet { get; set; }
        public virtual DbSet<PeriodoTrabajador> PeriodoTrabajadorSet { get; set; }
        public virtual DbSet<Dia> DiaSet { get; set; }
    }
}
