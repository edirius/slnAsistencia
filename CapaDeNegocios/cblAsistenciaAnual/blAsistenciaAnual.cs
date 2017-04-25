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
            cAsistenciaAnual miPrimeraAsistenciaAnual = new cblAsistenciaAnual.cAsistenciaAnual();
            miPrimeraAsistenciaAnual.fechaInicio = miPeriodoTrabajador.Inicio.Date;
            miPrimeraAsistenciaAnual.fechaFin = miPrimeraAsistenciaAnual.fechaInicio.AddDays(364).Date;
            miPrimeraAsistenciaAnual = LlenarAsistencia(miTrabajador, miPrimeraAsistenciaAnual);
            miPrimeraAsistenciaAnual = LlenarPermisos(miTrabajador, miPrimeraAsistenciaAnual);
            miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual.Add(miPrimeraAsistenciaAnual);
            while (miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual[miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual.Count - 1].fechaFin.Date < DateTime.Today.Date)
            {
                cAsistenciaAnual auxiliar = new cblAsistenciaAnual.cAsistenciaAnual();
                auxiliar.fechaInicio = miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual[miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual.Count - 1].fechaFin.AddDays(1).Date;
                auxiliar.fechaFin = auxiliar.fechaInicio.AddDays(364).Date;
                auxiliar = LlenarAsistencia(miTrabajador, auxiliar);
                auxiliar = LlenarPermisos(miTrabajador, auxiliar);
                miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual.Add(auxiliar);
            }
            return miAsistenciaPeriodoTrabajador;
        }

        public bool AsignarVacaciones(cAsistenciaPeriodoTrabajador miAsistenciaPeriodo, cAsistenciaAnual miAsistenciaAnual, CapaDeNegocios.cblVacaciones.cVacaciones miVacaciones)
        {
            if (miAsistenciaPeriodo.miListaAsistenciaAnual.Count >= 3)
            {
                if (miAsistenciaPeriodo.miListaAsistenciaAnual[miAsistenciaPeriodo.miListaAsistenciaAnual.Count - 2].miVacaciones.vacacionesEfectuadas == false && miAsistenciaPeriodo.miListaAsistenciaAnual[miAsistenciaPeriodo.miListaAsistenciaAnual.Count - 3].miVacaciones.vacacionesEfectuadas == false && miVacaciones.vacacionesEfectuadas == false)
                {
                    throw new cReglaNegociosException("No se puede postergar xq ya existen 2 periodos acumulados.");
                }
                else
                {
                    miAsistenciaAnual.miVacaciones = miVacaciones;
                }
            }
            else
            {
                miAsistenciaAnual.miVacaciones = miVacaciones;
            }
            return true;
        }

        public void calculo()
        {

        }

        public cAsistenciaAnual LlenarPermisos(Trabajador miTrabajador, cAsistenciaAnual miAsistenciaAnualAnterior)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PermisosDias> consultaPermisos = from d in bd.PermisosDiasSet.Include("PeriodoTrabajador")
                                                            where d.PeriodoTrabajador.Trabajador.Id == miTrabajador.Id
                                                            select d;

                cAsistenciaAnual miAsistenciaAnual = miAsistenciaAnualAnterior;
                int nroMeses = Math.Abs((miAsistenciaAnual.fechaFin.Month - miAsistenciaAnual.fechaInicio.Month) + 12 * (miAsistenciaAnual.fechaFin.Year - miAsistenciaAnual.fechaInicio.Year));
                foreach (PermisosDias item in consultaPermisos)
                {
                    int nroDiasPermiso = (item.Fin - item.Inicio).Days;
                    for (int i = 0; i <= nroDiasPermiso; i++)
                    {
                        DateTime fechaauxiliar = item.Inicio.AddDays(i);
                        for (int j = 0; j <= nroMeses; j++)
                        {
                            if (fechaauxiliar.Year == miAsistenciaAnual.miListaAsistenciaMeses[j].nombreAño && fechaauxiliar.ToString("MMMM") == miAsistenciaAnual.miListaAsistenciaMeses[j].nombreMes)
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = fechaauxiliar;
                                auxiliardia.asistencia = false;
                                auxiliardia.miPermiso = item;
                                miAsistenciaAnual.miListaAsistenciaMeses[j].miListaAsistenciaDias.Add(auxiliardia);
                            }
                        }
                    }
                }
                return miAsistenciaAnual;
            }
        }

        public cAsistenciaAnual LlenarAsistencia(Trabajador miTrabajador, cAsistenciaAnual miAsistenciaAnualAnterior)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Asistencia> consultaAsistencia = from d in bd.AsistenciaSet
                                                            where d.Trabajador.Id == miTrabajador.Id
                                                            && d.PicadoReloj >= miAsistenciaAnualAnterior.fechaInicio
                                                            && d.PicadoReloj <= miAsistenciaAnualAnterior.fechaFin
                                                            select d;

                cAsistenciaAnual miAsistenciaAnual = miAsistenciaAnualAnterior;
                CrearMeses(miAsistenciaAnual);
                int nroMeses = Math.Abs((miAsistenciaAnual.fechaFin.Month - miAsistenciaAnual.fechaInicio.Month) + 12 * (miAsistenciaAnual.fechaFin.Year - miAsistenciaAnual.fechaInicio.Year));
                foreach (Asistencia item in consultaAsistencia)
                {
                    for (int j = 0; j <= nroMeses; j++)
                    {
                        if (item.PicadoReloj.Year == miAsistenciaAnual.miListaAsistenciaMeses[j].nombreAño && item.PicadoReloj.ToString("MMMM") == miAsistenciaAnual.miListaAsistenciaMeses[j].nombreMes)
                        {
                            cAsistenciaDia auxiliardia = new cAsistenciaDia();
                            auxiliardia.fecha = item.PicadoReloj;
                            auxiliardia.asistencia = true;
                            //auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                            miAsistenciaAnual.miListaAsistenciaMeses[j].miListaAsistenciaDias.Add(auxiliardia);
                        }
                    }
                }
                return miAsistenciaAnual;
            }
        }

        public void CrearMeses(cAsistenciaAnual miAsistenciaAnual)
        {
            DateTime fechaInicio = miAsistenciaAnual.fechaInicio;
            DateTime fechaFin = miAsistenciaAnual.fechaFin;
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
                        Enero.miAsistenciaAnual = miAsistenciaAnual;
                        miAsistenciaAnual.miListaAsistenciaMeses.Add(Enero);
                    }
                    else if (j == 2)
                    {
                        cAsistenciaMeses Febrero = new cblAsistenciaAnual.cAsistenciaMeses();
                        Febrero.nombreAño = año;
                        Febrero.nombreMes = "Febrero";
                        Febrero.miAsistenciaAnual = miAsistenciaAnual;
                        miAsistenciaAnual.miListaAsistenciaMeses.Add(Febrero);
                    }
                    else if (j == 3)
                    {
                        cAsistenciaMeses Marzo = new cblAsistenciaAnual.cAsistenciaMeses();
                        Marzo.nombreAño = año;
                        Marzo.nombreMes = "Marzo";
                        Marzo.miAsistenciaAnual = miAsistenciaAnual;
                        miAsistenciaAnual.miListaAsistenciaMeses.Add(Marzo);
                    }
                    else if (j == 4)
                    {
                        cAsistenciaMeses Abril = new cblAsistenciaAnual.cAsistenciaMeses();
                        Abril.nombreAño = año;
                        Abril.nombreMes = "Abril";
                        Abril.miAsistenciaAnual = miAsistenciaAnual;
                        miAsistenciaAnual.miListaAsistenciaMeses.Add(Abril);
                    }
                    else if (j == 5)
                    {
                        cAsistenciaMeses Mayo = new cblAsistenciaAnual.cAsistenciaMeses();
                        Mayo.nombreAño = año;
                        Mayo.nombreMes = "Mayo";
                        Mayo.miAsistenciaAnual = miAsistenciaAnual;
                        miAsistenciaAnual.miListaAsistenciaMeses.Add(Mayo);
                    }
                    else if (j == 6)
                    {
                        cAsistenciaMeses Junio = new cblAsistenciaAnual.cAsistenciaMeses();
                        Junio.nombreAño = año;
                        Junio.nombreMes = "Junio";
                        Junio.miAsistenciaAnual = miAsistenciaAnual;
                        miAsistenciaAnual.miListaAsistenciaMeses.Add(Junio);
                    }
                    else if (j == 7)
                    {
                        cAsistenciaMeses Julio = new cblAsistenciaAnual.cAsistenciaMeses();
                        Julio.nombreAño = año;
                        Julio.nombreMes = "Julio";
                        Julio.miAsistenciaAnual = miAsistenciaAnual;
                        miAsistenciaAnual.miListaAsistenciaMeses.Add(Julio);
                    }
                    else if (j == 8)
                    {
                        cAsistenciaMeses Agosto = new cblAsistenciaAnual.cAsistenciaMeses();
                        Agosto.nombreAño = año;
                        Agosto.nombreMes = "Agosto";
                        Agosto.miAsistenciaAnual = miAsistenciaAnual;
                        miAsistenciaAnual.miListaAsistenciaMeses.Add(Agosto);
                    }
                    else if (j == 9)
                    {
                        cAsistenciaMeses Setiembre = new cblAsistenciaAnual.cAsistenciaMeses();
                        Setiembre.nombreAño = año;
                        Setiembre.nombreMes = "Setiembre";
                        Setiembre.miAsistenciaAnual = miAsistenciaAnual;
                        miAsistenciaAnual.miListaAsistenciaMeses.Add(Setiembre);
                    }
                    else if (j == 10)
                    {
                        cAsistenciaMeses Octubre = new cblAsistenciaAnual.cAsistenciaMeses();
                        Octubre.nombreAño = año;
                        Octubre.nombreMes = "Octubre";
                        Octubre.miAsistenciaAnual = miAsistenciaAnual;
                        miAsistenciaAnual.miListaAsistenciaMeses.Add(Octubre);
                    }
                    else if (j == 11)
                    {
                        cAsistenciaMeses Noviembre = new cblAsistenciaAnual.cAsistenciaMeses();
                        Noviembre.nombreAño = año;
                        Noviembre.nombreMes = "Noviembre";
                        Noviembre.miAsistenciaAnual = miAsistenciaAnual;
                        miAsistenciaAnual.miListaAsistenciaMeses.Add(Noviembre);
                    }
                    else if (j == 12)
                    {
                        cAsistenciaMeses Diciembre = new cblAsistenciaAnual.cAsistenciaMeses();
                        Diciembre.nombreAño = año;
                        Diciembre.nombreMes = "Diciembre";
                        Diciembre.miAsistenciaAnual = miAsistenciaAnual;
                        miAsistenciaAnual.miListaAsistenciaMeses.Add(Diciembre);
                    }
                }
            }
        }
    }
}
