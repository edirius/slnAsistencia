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
                    if (item.PicadoReloj.Month == 1)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        miAsistenciaAnual.AsistenciaMeses[0].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 2)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        miAsistenciaAnual.AsistenciaMeses[1].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 3)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        miAsistenciaAnual.AsistenciaMeses[2].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 4)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        miAsistenciaAnual.AsistenciaMeses[3].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 5)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        miAsistenciaAnual.AsistenciaMeses[4].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 6)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        miAsistenciaAnual.AsistenciaMeses[5].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 7)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        miAsistenciaAnual.AsistenciaMeses[6].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 8)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        miAsistenciaAnual.AsistenciaMeses[7].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 9)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        miAsistenciaAnual.AsistenciaMeses[8].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 10)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        miAsistenciaAnual.AsistenciaMeses[9].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 11)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        miAsistenciaAnual.AsistenciaMeses[10].AsistenciaDias.Add(auxiliardia);
                    }
                    else if (item.PicadoReloj.Month == 12)
                    {
                        cAsistenciaDia auxiliardia = new cAsistenciaDia();
                        auxiliardia.fecha = item.PicadoReloj;
                        auxiliardia.asistencia = true;
                        miAsistenciaAnual.AsistenciaMeses[11].AsistenciaDias.Add(auxiliardia);
                    }
                }
                return miAsistenciaAnual;
            }
        }
    }
}
