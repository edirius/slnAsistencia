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
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public List<cAsistenciaMeses> miListaAsistenciaMeses { get; set; }
        public cAsistenciaPeriodoTrabajador miPeriodoTrabajador { get; set; }
        public CapaDeNegocios.cblVacaciones.cVacaciones miVacaciones { get; set; }
    }
}
