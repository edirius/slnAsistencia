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
        public string nombreMes { get; set; }
        public List<cAsistenciaDia> miListaAsistenciaDias { get; set; }
        public cAsistenciaAnual miAsistenciaAnual { get; set; }
    }
}
