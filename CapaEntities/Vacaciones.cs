//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Vacaciones
    {
        public int Id { get; set; }
        public System.DateTime Inicio { get; set; }
        public System.DateTime Fin { get; set; }
        public int DiasVacacionesAdelantadas { get; set; }
        public int DiasVacacionesDisponibles { get; set; }
    
        public virtual AsistenciaPeriodoLaborado AsistenciaPeriodoLaborado { get; set; }
    }
}
