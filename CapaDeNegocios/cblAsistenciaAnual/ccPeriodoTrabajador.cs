using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;

namespace CapaDeNegocios.cblAsistenciaAnual
{
    public class ccPeriodoTrabajador
    {
        public ccPeriodoTrabajador()
        {
            miListaAsistenciaPeriodoLaborado = new List<cAsistenciaPeriodoLaborado>();
        }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public List<cAsistenciaPeriodoLaborado> miListaAsistenciaPeriodoLaborado { get; set; }
    }
}
