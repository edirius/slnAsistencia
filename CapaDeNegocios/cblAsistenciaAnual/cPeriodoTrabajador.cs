using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;

namespace CapaDeNegocios.cblAsistenciaAnual
{
    public class cPeriodoTrabajador
    {
        public PeriodoTrabajador miPeriodoTrabajador { get; set; }
        public List<cAsistenciaAnual> miAsistenciaAnual { get; set; }
    }
}
