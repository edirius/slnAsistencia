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
        public cAsistenciaAnual LlenarAsistencia(Trabajador miTrabajador)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Asistencia> consultaAsistencia = from d in bd.AsistenciaSet
                                                            where d.Trabajador.Id == miTrabajador.Id
                                                            select d;

                cAsistenciaAnual miAsistenciaAnual = new cblAsistenciaAnual.cAsistenciaAnual();
                foreach (Asistencia item in consultaAsistencia)
                {
                    if (item.PicadoReloj.Month == 0)
                    {
                        foreach (cAsistenciaMeses item2 in miAsistenciaAnual.AsistenciaMeses)
                        {
                            if (item2.nombreMes == "Enero")
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = item.PicadoReloj;
                                auxiliardia.asistencia = true;
                                item2.AsistenciaDias.Add(auxiliardia);
                            }
                            else if(item2.nombreMes == "Febrero")
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = item.PicadoReloj;
                                auxiliardia.asistencia = true;
                                item2.AsistenciaDias.Add(auxiliardia);
                            }
                            else if (item2.nombreMes == "Marzo")
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = item.PicadoReloj;
                                auxiliardia.asistencia = true;
                                item2.AsistenciaDias.Add(auxiliardia);
                            }
                            else if (item2.nombreMes == "Abril")
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = item.PicadoReloj;
                                auxiliardia.asistencia = true;
                                item2.AsistenciaDias.Add(auxiliardia);
                            }
                            else if (item2.nombreMes == "Mayo")
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = item.PicadoReloj;
                                auxiliardia.asistencia = true;
                                item2.AsistenciaDias.Add(auxiliardia);
                            }
                            else if (item2.nombreMes == "Junio")
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = item.PicadoReloj;
                                auxiliardia.asistencia = true;
                                item2.AsistenciaDias.Add(auxiliardia);
                            }
                            else if (item2.nombreMes == "Julio")
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = item.PicadoReloj;
                                auxiliardia.asistencia = true;
                                item2.AsistenciaDias.Add(auxiliardia);
                            }
                            else if (item2.nombreMes == "Agosto")
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = item.PicadoReloj;
                                auxiliardia.asistencia = true;
                                item2.AsistenciaDias.Add(auxiliardia);
                            }
                            else if (item2.nombreMes == "Setiembre")
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = item.PicadoReloj;
                                auxiliardia.asistencia = true;
                                item2.AsistenciaDias.Add(auxiliardia);
                            }
                            else if (item2.nombreMes == "Octubre")
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = item.PicadoReloj;
                                auxiliardia.asistencia = true;
                                item2.AsistenciaDias.Add(auxiliardia);
                            }
                            else if (item2.nombreMes == "Noviembre")
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = item.PicadoReloj;
                                auxiliardia.asistencia = true;
                                item2.AsistenciaDias.Add(auxiliardia);
                            }
                            else if (item2.nombreMes == "Diciembre")
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = item.PicadoReloj;
                                auxiliardia.asistencia = true;
                                item2.AsistenciaDias.Add(auxiliardia);
                            }
                        }
                    }
                }
                return miAsistenciaAnual;
            }
        }
    }
}
