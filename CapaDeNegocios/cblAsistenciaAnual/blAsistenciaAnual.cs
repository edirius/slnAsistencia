using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.cblAsistenciaAnual
{
    public class blAsistenciaAnual
    {
        public cAsistenciaPeriodoTrabajador CalcularAsistenciaPeriodoTrabajador(Trabajador miTrabajador, PeriodoTrabajador miPeriodoTrabajador)
        {
            cAsistenciaPeriodoTrabajador miAsistenciaPeriodoTrabajador = new cblAsistenciaAnual.cAsistenciaPeriodoTrabajador();
            cAsistenciaPeriodoLaborado miPrimeraAsistenciaAnual = new cblAsistenciaAnual.cAsistenciaPeriodoLaborado();
            miPrimeraAsistenciaAnual.fechaInicio = miPeriodoTrabajador.Inicio.Date;
            if (miPrimeraAsistenciaAnual.fechaInicio.AddDays(364).Date >= DateTime.Today.Date)
            { miPrimeraAsistenciaAnual.fechaFin = DateTime.Today.Date; }
            else
            { miPrimeraAsistenciaAnual.fechaFin = miPrimeraAsistenciaAnual.fechaInicio.AddDays(364).Date; }
            miPrimeraAsistenciaAnual = CrearMeses(miPrimeraAsistenciaAnual);
            miPrimeraAsistenciaAnual = LlenarAsistencia(miTrabajador, miPrimeraAsistenciaAnual);
            miPrimeraAsistenciaAnual = LlenarPermisos(miTrabajador, miPrimeraAsistenciaAnual);
            miAsistenciaPeriodoTrabajador.miListaAsistenciaPeriodoLaborado.Add(miPrimeraAsistenciaAnual);
            while (miAsistenciaPeriodoTrabajador.miListaAsistenciaPeriodoLaborado[miAsistenciaPeriodoTrabajador.miListaAsistenciaPeriodoLaborado.Count - 1].fechaFin.Date < DateTime.Today.Date)
            {
                cAsistenciaPeriodoLaborado auxiliar = new cblAsistenciaAnual.cAsistenciaPeriodoLaborado();
                auxiliar.fechaInicio = miAsistenciaPeriodoTrabajador.miListaAsistenciaPeriodoLaborado[miAsistenciaPeriodoTrabajador.miListaAsistenciaPeriodoLaborado.Count - 1].fechaFin.AddDays(1).Date;
                auxiliar.fechaFin = auxiliar.fechaInicio.AddDays(364).Date;
                auxiliar = CrearMeses(auxiliar);
                auxiliar = LlenarAsistencia(miTrabajador, auxiliar);
                auxiliar = LlenarPermisos(miTrabajador, auxiliar);
                miAsistenciaPeriodoTrabajador.miListaAsistenciaPeriodoLaborado.Add(auxiliar);
            }
            return miAsistenciaPeriodoTrabajador;
        }

        public cAsistenciaPeriodoLaborado LlenarPermisos(Trabajador miTrabajador, cAsistenciaPeriodoLaborado miAsistenciaPeriodoLaboradoAnterior)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PermisosDias> consultaPermisos = from d in bd.PermisosDiasSet.Include("TipoPermisos")
                                                            where d.PeriodoTrabajador.Trabajador.Id == miTrabajador.Id
                                                            select d;

                cAsistenciaPeriodoLaborado miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaboradoAnterior;
                int nroMeses = Math.Abs((miAsistenciaPeriodoLaborado.fechaFin.Month - miAsistenciaPeriodoLaborado.fechaInicio.Month) + 12 * (miAsistenciaPeriodoLaborado.fechaFin.Year - miAsistenciaPeriodoLaborado.fechaInicio.Year));
                foreach (PermisosDias item in consultaPermisos)
                {
                    int nroDiasPermiso = (item.Fin - item.Inicio).Days;
                    for (int i = 0; i <= nroDiasPermiso; i++)
                    {
                        DateTime fechaauxiliar = item.Inicio.AddDays(i);
                        for (int j = 0; j <= nroMeses; j++)
                        {
                            if (fechaauxiliar.Year == miAsistenciaPeriodoLaborado.miListaAsistenciaMeses[j].nombreAño && fechaauxiliar.ToString("MMMM") == miAsistenciaPeriodoLaborado.miListaAsistenciaMeses[j].nombreMes)
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = fechaauxiliar;
                                auxiliardia.asistencia = false;
                                auxiliardia.miPermiso = item;
                                miAsistenciaPeriodoLaborado.miListaAsistenciaMeses[j].miListaAsistenciaDias.Add(auxiliardia);
                            }
                        }
                    }
                }
                return miAsistenciaPeriodoLaborado;
            }
        }

        public cAsistenciaPeriodoLaborado LlenarAsistencia(Trabajador miTrabajador, cAsistenciaPeriodoLaborado miAsistenciaPeriodoLaboradoAnterior)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Asistencia> consultaAsistencia = from d in bd.AsistenciaSet
                                                            where d.Trabajador.Id == miTrabajador.Id
                                                            && d.PicadoReloj >= miAsistenciaPeriodoLaboradoAnterior.fechaInicio
                                                            && d.PicadoReloj <= miAsistenciaPeriodoLaboradoAnterior.fechaFin
                                                            select d;

                cAsistenciaPeriodoLaborado miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaboradoAnterior;
                int nroMeses = Math.Abs((miAsistenciaPeriodoLaborado.fechaFin.Month - miAsistenciaPeriodoLaborado.fechaInicio.Month) + 12 * (miAsistenciaPeriodoLaborado.fechaFin.Year - miAsistenciaPeriodoLaborado.fechaInicio.Year));
                foreach (Asistencia item in consultaAsistencia)
                {
                    for (int j = 0; j <= nroMeses; j++)
                    {
                        if (item.PicadoReloj.Year == miAsistenciaPeriodoLaborado.miListaAsistenciaMeses[j].nombreAño && item.PicadoReloj.ToString("MMMM") == miAsistenciaPeriodoLaborado.miListaAsistenciaMeses[j].nombreMes)
                        {
                            cAsistenciaDia auxiliardia = new cAsistenciaDia();
                            auxiliardia.fecha = item.PicadoReloj;
                            auxiliardia.asistencia = true;
                            //auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                            miAsistenciaPeriodoLaborado.miListaAsistenciaMeses[j].miListaAsistenciaDias.Add(auxiliardia);
                        }
                    }
                }
                return miAsistenciaPeriodoLaborado;
            }
        }

        public cAsistenciaPeriodoLaborado CrearMeses(cAsistenciaPeriodoLaborado miAsistenciaPeriodoLaboradoAnterior)
        {
            cAsistenciaPeriodoLaborado miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaboradoAnterior;
            DateTime fechaInicio = miAsistenciaPeriodoLaborado.fechaInicio;
            DateTime fechaFin = miAsistenciaPeriodoLaborado.fechaFin;
            int nroAños = fechaFin.Year - fechaInicio.Year;
            int año = 0;
            int mesinicio = 0;
            int mesfin = 0;
            for (int i = 0; i <= nroAños; i++)
            {
                año = fechaInicio.Year + i;
                if (nroAños == 0)
                {
                    mesinicio = fechaInicio.Month;
                    mesfin = fechaFin.Month;
                }
                else
                {
                    if (año == fechaInicio.Year)
                    {
                        mesinicio = fechaInicio.Month;
                        mesfin = 12;
                    }
                    else if (año > fechaInicio.Year && año < fechaFin.Year)
                    {
                        mesinicio = 1;
                        mesfin = 12;
                    }
                    else if (año == fechaFin.Year)
                    {
                        mesinicio = 1;
                        mesfin = fechaFin.Month;
                    }
                }
                for (int j = mesinicio; j <= mesfin; j++)
                {
                    if (j == 1)
                    {
                        cAsistenciaMeses Enero = new cblAsistenciaAnual.cAsistenciaMeses();
                        Enero.nombreAño = año;
                        Enero.nombreMes = "Enero";
                        Enero.miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaborado;
                        miAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Enero);
                    }
                    else if (j == 2)
                    {
                        cAsistenciaMeses Febrero = new cblAsistenciaAnual.cAsistenciaMeses();
                        Febrero.nombreAño = año;
                        Febrero.nombreMes = "Febrero";
                        Febrero.miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaborado;
                        miAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Febrero);
                    }
                    else if (j == 3)
                    {
                        cAsistenciaMeses Marzo = new cblAsistenciaAnual.cAsistenciaMeses();
                        Marzo.nombreAño = año;
                        Marzo.nombreMes = "Marzo";
                        Marzo.miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaborado;
                        miAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Marzo);
                    }
                    else if (j == 4)
                    {
                        cAsistenciaMeses Abril = new cblAsistenciaAnual.cAsistenciaMeses();
                        Abril.nombreAño = año;
                        Abril.nombreMes = "Abril";
                        Abril.miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaborado;
                        miAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Abril);
                    }
                    else if (j == 5)
                    {
                        cAsistenciaMeses Mayo = new cblAsistenciaAnual.cAsistenciaMeses();
                        Mayo.nombreAño = año;
                        Mayo.nombreMes = "Mayo";
                        Mayo.miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaborado;
                        miAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Mayo);
                    }
                    else if (j == 6)
                    {
                        cAsistenciaMeses Junio = new cblAsistenciaAnual.cAsistenciaMeses();
                        Junio.nombreAño = año;
                        Junio.nombreMes = "Junio";
                        Junio.miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaborado;
                        miAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Junio);
                    }
                    else if (j == 7)
                    {
                        cAsistenciaMeses Julio = new cblAsistenciaAnual.cAsistenciaMeses();
                        Julio.nombreAño = año;
                        Julio.nombreMes = "Julio";
                        Julio.miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaborado;
                        miAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Julio);
                    }
                    else if (j == 8)
                    {
                        cAsistenciaMeses Agosto = new cblAsistenciaAnual.cAsistenciaMeses();
                        Agosto.nombreAño = año;
                        Agosto.nombreMes = "Agosto";
                        Agosto.miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaborado;
                        miAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Agosto);
                    }
                    else if (j == 9)
                    {
                        cAsistenciaMeses Setiembre = new cblAsistenciaAnual.cAsistenciaMeses();
                        Setiembre.nombreAño = año;
                        Setiembre.nombreMes = "Setiembre";
                        Setiembre.miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaborado;
                        miAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Setiembre);
                    }
                    else if (j == 10)
                    {
                        cAsistenciaMeses Octubre = new cblAsistenciaAnual.cAsistenciaMeses();
                        Octubre.nombreAño = año;
                        Octubre.nombreMes = "Octubre";
                        Octubre.miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaborado;
                        miAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Octubre);
                    }
                    else if (j == 11)
                    {
                        cAsistenciaMeses Noviembre = new cblAsistenciaAnual.cAsistenciaMeses();
                        Noviembre.nombreAño = año;
                        Noviembre.nombreMes = "Noviembre";
                        Noviembre.miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaborado;
                        miAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Noviembre);
                    }
                    else if (j == 12)
                    {
                        cAsistenciaMeses Diciembre = new cblAsistenciaAnual.cAsistenciaMeses();
                        Diciembre.nombreAño = año;
                        Diciembre.nombreMes = "Diciembre";
                        Diciembre.miAsistenciaPeriodoLaborado = miAsistenciaPeriodoLaborado;
                        miAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Diciembre);
                    }
                }
            }
            return miAsistenciaPeriodoLaborado;
        }
    }
}
