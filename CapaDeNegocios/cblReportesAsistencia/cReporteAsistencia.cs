using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.cblReportesAsistencia
{
    public class cReporteAsistencia
    {
        public string titulo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        
        public List<cDetalleReporteAsistenciaXTrabajador> detallesReporteAsistenciaXTrabajador { get; set; }
        
        public cReporteAsistencia ()
        {
            detallesReporteAsistenciaXTrabajador = new List<cDetalleReporteAsistenciaXTrabajador>();
        }
    }
}
