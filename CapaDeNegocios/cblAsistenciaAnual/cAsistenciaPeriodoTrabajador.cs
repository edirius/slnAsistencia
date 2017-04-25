using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;

namespace CapaDeNegocios.cblAsistenciaAnual
{
    public class cAsistenciaPeriodoTrabajador
    {
        public cAsistenciaPeriodoTrabajador()
        {
            miListaAsistenciaAnual = new List<cAsistenciaAnual>();
        }
        public List<cAsistenciaAnual> miListaAsistenciaAnual { get; set; }
    }
}
