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
            AsistenciaMeses = new List<cAsistenciaMeses>(); 
            cAsistenciaMeses Enero = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Enero";
            Enero.miAsistenciaAnual = this;
            AsistenciaMeses.Add(Enero);
            cAsistenciaMeses Febrero = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Febrero";
            Enero.miAsistenciaAnual = this;
            AsistenciaMeses.Add(Febrero);
            cAsistenciaMeses Marzo = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Marzo";
            Enero.miAsistenciaAnual = this;
            AsistenciaMeses.Add(Marzo);
            cAsistenciaMeses Abril = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Abril";
            Enero.miAsistenciaAnual = this;
            AsistenciaMeses.Add(Abril);
            cAsistenciaMeses Mayo = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Mayo";
            Enero.miAsistenciaAnual = this;
            AsistenciaMeses.Add(Mayo);
            cAsistenciaMeses Junio = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Junio";
            Enero.miAsistenciaAnual = this;
            AsistenciaMeses.Add(Junio);
            cAsistenciaMeses Julio = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Julio";
            Enero.miAsistenciaAnual = this;
            AsistenciaMeses.Add(Julio);
            cAsistenciaMeses Agosto = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Agosto";
            Enero.miAsistenciaAnual = this;
            AsistenciaMeses.Add(Agosto);
            cAsistenciaMeses Setiembre = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Setiembre";
            Enero.miAsistenciaAnual = this;
            AsistenciaMeses.Add(Setiembre);
            cAsistenciaMeses Octubre = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Octubre";
            Enero.miAsistenciaAnual = this;
            AsistenciaMeses.Add(Octubre);
            cAsistenciaMeses Noviembre = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Noviembre";
            Enero.miAsistenciaAnual = this;
            AsistenciaMeses.Add(Noviembre);
            cAsistenciaMeses Diciembre = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Diciembre";
            Enero.miAsistenciaAnual = this;
            AsistenciaMeses.Add(Diciembre);
        }
        public Trabajador miTrabajador { get; set; }
        public DateTime fechaInicioPeriodo { get; set; }
        public DateTime fechaFinPeriodo { get; set; }
        public int diasLaborados { set; get; }
        public int diasPermisosComputables { set; get; }
        public int diasPermisosNoComputables { set; get; }
        public int totalDiasComputables { set; get; }
        public List<cAsistenciaMeses> AsistenciaMeses { get; set; }
    }
}
