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
        public int diasPermisosComputables { set; get; }
        public int diasPermisosNoComputables { set; get; }
        public int diasTotalComputables { set; get; }
        public List<cAsistenciaDia> miListaAsistenciaDias { get; set; }
        public cAsistenciaAnual miAsistenciaAnual { get; set; }
    }
}
