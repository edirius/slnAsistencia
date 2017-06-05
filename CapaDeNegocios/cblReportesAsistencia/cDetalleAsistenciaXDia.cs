using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;

namespace CapaDeNegocios.cblReportesAsistencia
{
    public class cDetalleAsistenciaXDia
    {
        public bool DiaSinPeriodo { get; set; }
        public bool DiaLibre { get; set; }
        public int SubTotalMinutosTarde { get; set; }
        public int SubTotalFalta { get; set; }
        public DateTime Dia { get; set; }
        public List<Asistencia> ListaAsistencia { get; set; }
        public List<Horario> ListaHorario { get; set; }
        public List<PermisosDias> ListaPermisos { get; set; }
        public List<PermisosHoras> ListaPermisosHoras { get; set; }
        public List<DiaFestivo> ListaDiaFestivo { get; set; }      
        
    }
}
