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
            this.miAsistenciaAnual = new cblAsistenciaAnual.cAsistenciaAnual();
        }
        public string nombreMes { get; set; }
        public cAsistenciaAnual miAsistenciaAnual { get; set; }
        public ICollection<cAsistenciaDia> AsistenciaDias { get; set; }
    }
}
