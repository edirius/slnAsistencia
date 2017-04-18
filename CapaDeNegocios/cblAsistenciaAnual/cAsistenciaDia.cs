using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocios.cblAsistenciaAnual
{
    public class cAsistenciaDia
    {
        //public cAsistenciaDia()
        //{
        //    this.miAsistenciaMeses = new cAsistenciaMeses();
        //}
        public DateTime fecha { get; set; }
        public bool asistencia { get; set; }
        public bool permiso { get; set; }
        public bool computable { get; set; }
        public cAsistenciaMeses miAsistenciaMeses { get; set; }
    }
}
