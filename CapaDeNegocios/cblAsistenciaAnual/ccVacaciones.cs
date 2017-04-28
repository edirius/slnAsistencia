using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.cblAsistenciaAnual
{
    public class ccVacaciones
    {
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public int diasVacacionesAdelantadas { set; get; }
        public int diasVacacionesDisponibles { set; get; }
        public cAsistenciaPeriodoLaborado miAsistenciaPeriodoLaborado { get; set; }
    }
}
