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
        public List<cAsistenciaAnual> CalcularPeriodos(Trabajador miTrabajador, cPeriodoTrabajador miPeriodoTrabajador)
        {
            List<cAsistenciaAnual> miListaAsistenciaAnual = new List<cAsistenciaAnual>();
            cAsistenciaAnual miPrimeraAsistenciaAnual = new cblAsistenciaAnual.cAsistenciaAnual();
            miPrimeraAsistenciaAnual.fechaInicioPeriodo = miPeriodoTrabajador.miPeriodoTrabajador.Inicio.Date;
            miPrimeraAsistenciaAnual.fechaFinPeriodo = miPrimeraAsistenciaAnual.fechaInicioPeriodo.AddDays(365).Date;
            miPrimeraAsistenciaAnual = LlenarAsistencia(miTrabajador, miPrimeraAsistenciaAnual);
            miListaAsistenciaAnual.Add(miPrimeraAsistenciaAnual);
            while (miListaAsistenciaAnual[miListaAsistenciaAnual.Count - 1].fechaFinPeriodo.Date > DateTime.Today)
            {
                cAsistenciaAnual auxiliar = new cblAsistenciaAnual.cAsistenciaAnual();
                auxiliar.fechaInicioPeriodo = miListaAsistenciaAnual[miListaAsistenciaAnual.Count - 1].fechaFinPeriodo.AddDays(1).Date;
                auxiliar.fechaFinPeriodo = auxiliar.fechaInicioPeriodo.AddDays(365).Date;
                auxiliar = LlenarAsistencia(miTrabajador, miPrimeraAsistenciaAnual);
                miListaAsistenciaAnual.Add(auxiliar);
            }
            return miListaAsistenciaAnual;
        }

        public cAsistenciaAnual LlenarAsistencia(Trabajador miTrabajador, cAsistenciaAnual miAsistenciaAnualAnterior)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Asistencia> consultaAsistencia = from d in bd.AsistenciaSet
                                                            where d.Trabajador.Id == miTrabajador.Id
                                                            && d.PicadoReloj.Date >= miAsistenciaAnualAnterior.fechaInicioPeriodo.Date
                                                            && d.PicadoReloj.Date <= miAsistenciaAnualAnterior.fechaFinPeriodo.Date
                                                            select d;

                cAsistenciaAnual miAsistenciaAnual = new cblAsistenciaAnual.cAsistenciaAnual();
                foreach (Asistencia item in consultaAsistencia)
                {
                    if (item.PicadoReloj.Month == 1)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(auxiliardia);
                        miAsistenciaAnual.AsistenciaMeses[0].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 2)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(auxiliardia);
                        miAsistenciaAnual.AsistenciaMeses[1].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 3)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(auxiliardia);
                        miAsistenciaAnual.AsistenciaMeses[2].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 4)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(auxiliardia);
                        miAsistenciaAnual.AsistenciaMeses[3].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 5)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(auxiliardia);
                        miAsistenciaAnual.AsistenciaMeses[4].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 6)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(auxiliardia);
                        miAsistenciaAnual.AsistenciaMeses[5].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 7)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(auxiliardia);
                        miAsistenciaAnual.AsistenciaMeses[6].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 8)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(auxiliardia);
                        miAsistenciaAnual.AsistenciaMeses[7].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 9)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(auxiliardia);
                        miAsistenciaAnual.AsistenciaMeses[8].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 10)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(auxiliardia);
                        miAsistenciaAnual.AsistenciaMeses[9].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 11)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(auxiliardia);
                        miAsistenciaAnual.AsistenciaMeses[10].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 12)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        auxiliardia.miPermiso = Permisos(auxiliardia);
                        miAsistenciaAnual.AsistenciaMeses[11].AsistenciaDias.Add(auxiliardia);
                    }
                }
                return miAsistenciaAnual;
            }
        }

        public PermisosDias Permisos(cAsistenciaDia miAsistenciaDia)
        {
            PermisosDias auxiliar = null;
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PermisosDias> consultaPermisos = from d in bd.PermisosDiasSet.Include("PeriodoTrabajador")
                                                            where d.PeriodoTrabajador.Trabajador.Id == miAsistenciaDia.miAsistenciaMeses.miAsistenciaAnual.miTrabajador.Id
                                                            select d;
                foreach (PermisosDias item in consultaPermisos)
                {
                    if (miAsistenciaDia.fecha.Date >= item.Inicio.Date && miAsistenciaDia.fecha.Date <= item.Fin.Date)
                    {
                        auxiliar = item;
                    }
                }
            }
            return auxiliar;
        }
    }
}
