using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.cblVacaciones
{
    public class cVacaciones
    {
        public DateTime fechaMinimaInicioVacaciones { set; get; }
        public int diasLaborados { set; get; }
        public int diasPermisosComputables { set; get; }
        public int diasPermisosNoComputables { set; get; }
        public int diasTotalComputables { set; get; }
        public int diasVacacionesAdelantadas { set; get; }
        public int diasVacacionesDisponibles { set; get; }
        public bool vacacionesEfectuadas { set; get; }
    }
}
