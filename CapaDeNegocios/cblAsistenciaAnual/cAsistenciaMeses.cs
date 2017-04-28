using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.cblAsistenciaAnual
{
    public class cAsistenciaMeses
    {
        public cAsistenciaMeses()
        {
            miListaAsistenciaDias = new List<cAsistenciaDia>();
        }
        public int nombreAño { get; set; }
        public string nombreMes { get; set; }
        public int diasLaborados { set; get; }
        public int diasPermisos { set; get; }
        public int diasFaltas { set; get; }
        public int diasTotal { set; get; }
        public List<cAsistenciaDia> miListaAsistenciaDias { get; set; }
        public cAsistenciaPeriodoLaborado miAsistenciaPeriodoLaborado { get; set; }
    }
}
