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
            miPrimeraAsistenciaAnual.fechaFin = miPrimeraAsistenciaAnual.fechaInicio.AddDays(365).Date;
            miPrimeraAsistenciaAnual = LlenarAsistencia(miTrabajador, miPrimeraAsistenciaAnual);
            miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual.Add(miPrimeraAsistenciaAnual);
            while (miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual[miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual.Count - 1].fechaFin.Date > DateTime.Today)
            {
                cAsistenciaAnual auxiliar = new cblAsistenciaAnual.cAsistenciaAnual();
                auxiliar.fechaInicio = miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual[miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual.Count - 1].fechaFin.AddDays(1).Date;
                auxiliar.fechaFin = auxiliar.fechaInicio.AddDays(365).Date;
                auxiliar = LlenarAsistencia(miTrabajador, miPrimeraAsistenciaAnual);
                miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual.Add(auxiliar);
            }
            return miAsistenciaPeriodoTrabajador;
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

                cAsistenciaAnual miAsistenciaAnual = new cblAsistenciaAnual.cAsistenciaAnual();
                foreach (Asistencia item in consultaAsistencia)
                {
                    if (item.PicadoReloj.Month == 1)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                        miAsistenciaAnual.miListaAsistenciaMeses[0].miListaAsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 2)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                        miAsistenciaAnual.miListaAsistenciaMeses[1].miListaAsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 3)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                        miAsistenciaAnual.miListaAsistenciaMeses[2].miListaAsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 4)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                        miAsistenciaAnual.miListaAsistenciaMeses[3].miListaAsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 5)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                        miAsistenciaAnual.miListaAsistenciaMeses[4].miListaAsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 6)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                        miAsistenciaAnual.miListaAsistenciaMeses[5].miListaAsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 7)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                        miAsistenciaAnual.miListaAsistenciaMeses[6].miListaAsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 8)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                        miAsistenciaAnual.miListaAsistenciaMeses[7].miListaAsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 9)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                        miAsistenciaAnual.miListaAsistenciaMeses[8].miListaAsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 10)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                        miAsistenciaAnual.miListaAsistenciaMeses[9].miListaAsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 11)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                        miAsistenciaAnual.miListaAsistenciaMeses[10].miListaAsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 12)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                        miAsistenciaAnual.miListaAsistenciaMeses[11].miListaAsistenciaDias.Add(auxiliardia);
                    }
                }
                return miAsistenciaAnual;
            }
        }

        public PermisosDias Permisos(Trabajador miTrabajador, cAsistenciaDia miAsistenciaDia)
        {
            PermisosDias auxiliar = null;
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PermisosDias> consultaPermisos = from d in bd.PermisosDiasSet.Include("PeriodoTrabajador")
                                                            where d.PeriodoTrabajador.Trabajador.Id == miTrabajador.Id
                                                            select d;

                foreach (PermisosDias item in consultaPermisos)
                {
                    if (miAsistenciaDia.fecha >= item.Inicio && miAsistenciaDia.fecha <= item.Fin)
                    {
                        auxiliar = item;
                    }
                }
            }
            return auxiliar;
        }

        public void calculo()
        {

        }
    }
}
