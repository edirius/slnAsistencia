//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapaEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class PeriodoTrabajador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PeriodoTrabajador()
        {
            this.PermisosHoras = new HashSet<PermisosHoras>();
            this.PermisosDias = new HashSet<PermisosDias>();
        }
    
        public int Id { get; set; }
        public System.DateTime Inicio { get; set; }
        public System.DateTime Fin { get; set; }
    
        public virtual Trabajador Trabajador { get; set; }
        public virtual Oficina Oficina { get; set; }
        public virtual HorarioSemana HorarioSemana { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PermisosHoras> PermisosHoras { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PermisosDias> PermisosDias { get; set; }
    }
}
