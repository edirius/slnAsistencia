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
    
    public partial class Horario
    {
        public int Id { get; set; }
        public System.DateTime Entrada { get; set; }
        public System.DateTime Salida { get; set; }
        public string Nombre { get; set; }
    
        public virtual HorarioDia HorarioDia { get; set; }
        public virtual ReglasTardanza ReglasTardanza { get; set; }
    }
}
