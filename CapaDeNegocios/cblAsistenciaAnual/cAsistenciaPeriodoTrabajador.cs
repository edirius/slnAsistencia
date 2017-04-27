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
            miListaAsistenciaPeriodoLaborado = new List<cAsistenciaPeriodoLaborado>();
        }
        public List<cAsistenciaPeriodoLaborado> miListaAsistenciaPeriodoLaborado { get; set; }
    }
}
