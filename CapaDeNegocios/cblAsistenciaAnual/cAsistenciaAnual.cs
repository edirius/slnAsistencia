using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;

namespace CapaDeNegocios.cblAsistenciaAnual
{
    public class cAsistenciaAnual
    {
        public cAsistenciaAnual()
        {
            miListaAsistenciaMeses = new List<cAsistenciaMeses>();
        }
        public Trabajador miTrabajador { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public List<cAsistenciaMeses> miListaAsistenciaMeses { get; set; }
        public cAsistenciaPeriodoTrabajador miPeriodoTrabajador { get; set; }
        public CapaDeNegocios.cblVacaciones.cVacaciones miVacaciones { get; set; }
    }
}
