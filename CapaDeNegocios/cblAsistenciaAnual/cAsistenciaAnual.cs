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

            cAsistenciaMeses Enero = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Enero";
            Enero.miAsistenciaAnual = this;
            miListaAsistenciaMeses.Add(Enero);
            cAsistenciaMeses Febrero = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Febrero";
            Enero.miAsistenciaAnual = this;
            miListaAsistenciaMeses.Add(Febrero);
            cAsistenciaMeses Marzo = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Marzo";
            Enero.miAsistenciaAnual = this;
            miListaAsistenciaMeses.Add(Marzo);
            cAsistenciaMeses Abril = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Abril";
            Enero.miAsistenciaAnual = this;
            miListaAsistenciaMeses.Add(Abril);
            cAsistenciaMeses Mayo = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Mayo";
            Enero.miAsistenciaAnual = this;
            miListaAsistenciaMeses.Add(Mayo);
            cAsistenciaMeses Junio = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Junio";
            Enero.miAsistenciaAnual = this;
            miListaAsistenciaMeses.Add(Junio);
            cAsistenciaMeses Julio = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Julio";
            Enero.miAsistenciaAnual = this;
            miListaAsistenciaMeses.Add(Julio);
            cAsistenciaMeses Agosto = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Agosto";
            Enero.miAsistenciaAnual = this;
            miListaAsistenciaMeses.Add(Agosto);
            cAsistenciaMeses Setiembre = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Setiembre";
            Enero.miAsistenciaAnual = this;
            miListaAsistenciaMeses.Add(Setiembre);
            cAsistenciaMeses Octubre = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Octubre";
            Enero.miAsistenciaAnual = this;
            miListaAsistenciaMeses.Add(Octubre);
            cAsistenciaMeses Noviembre = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Noviembre";
            Enero.miAsistenciaAnual = this;
            miListaAsistenciaMeses.Add(Noviembre);
            cAsistenciaMeses Diciembre = new cblAsistenciaAnual.cAsistenciaMeses();
            Enero.nombreMes = "Diciembre";
            Enero.miAsistenciaAnual = this;
            miListaAsistenciaMeses.Add(Diciembre);
        }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        //public int diasLaborados { set; get; }
        //public int diasPermisosComputables { set; get; }
        //public int diasPermisosNoComputables { set; get; }
        //public int totalDiasComputables { set; get; }
        public List<cAsistenciaMeses> miListaAsistenciaMeses { get; set; }
        public cAsistenciaPeriodoTrabajador miPeriodoTrabajador { get; set; }
    }
}
