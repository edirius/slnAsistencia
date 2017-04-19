using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;

namespace CapaDeNegocios.cblAsistenciaAnual
{
    public class cAsistenciaDia
    {
        public DateTime fecha { get; set; }
        public bool asistencia { get; set; }
        public cAsistenciaMeses miAsistenciaMeses {get; set;}
        public PermisosDias miPermiso { get; set; }
    }
}
