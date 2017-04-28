using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;

namespace CapaDeNegocios.cblAsistenciaAnual
{
    public class cAsistenciaPeriodoLaborado
    {
        public cAsistenciaPeriodoLaborado()
        {
            miListaAsistenciaMeses = new List<cAsistenciaMeses>();
        }
        public List<cAsistenciaMeses> miListaAsistenciaMeses { get; set; }
        public AsistenciaPeriodoLaborado miAsistenciaPeriodoLaborado{ get; set; }
    }
}
