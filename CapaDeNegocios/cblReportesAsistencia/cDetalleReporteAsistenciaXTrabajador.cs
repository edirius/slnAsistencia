using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;

namespace CapaDeNegocios.cblReportesAsistencia
{
    public class cDetalleReporteAsistenciaXTrabajador
    {
        public Trabajador miTrabajador { get; set; }
        public List<cDetalleAsistenciaXDia> detallesAsistenciasXDia  { get; set; }
        public int SumaMinutosTardanza { get; set; }
        public int SumaFalta { get; set; }

        public cDetalleReporteAsistenciaXTrabajador()
        {
            detallesAsistenciasXDia = new List<cDetalleAsistenciaXDia>();
        }    
    }
}
