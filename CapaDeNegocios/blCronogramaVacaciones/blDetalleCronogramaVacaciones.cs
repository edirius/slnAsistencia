using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blCronogramaVacaciones
{
    public class blDetalleCronogramaVacaciones
    {

        public ICollection<DetalleCronogramaVacaciones> ListarDetalleCronogramaVacaciones(CronogramaVacaciones miCronogramaVacaciones)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<DetalleCronogramaVacaciones> consultaDetalleCronogramaVacaciones = from d in bd.DetalleCronogramaVacacionesSet.Include("Trabajador")
                                                                                              where d.CronogramaVacaciones.Id == miCronogramaVacaciones.Id
                                                                                              select d;
                return consultaDetalleCronogramaVacaciones.ToList();
            }
        }

        public void AgregarDetalleCronogramaVacaciones(DetalleCronogramaVacaciones miAgregarDetalleCronogramaVacaciones)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.TrabajadorSet.Attach(miAgregarDetalleCronogramaVacaciones.Trabajador);
                bd.CronogramaVacacionesSet.Attach(miAgregarDetalleCronogramaVacaciones.CronogramaVacaciones);
                bd.DetalleCronogramaVacacionesSet.Add(miAgregarDetalleCronogramaVacaciones);
                bd.SaveChanges();
            }
        }

        public void ModificarDetalleCronogramaVacaciones(DetalleCronogramaVacaciones miModificarDetalleCronogramaVacaciones)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                DetalleCronogramaVacaciones auxiliar = (from c in bd.DetalleCronogramaVacacionesSet
                                                        where c.Id == miModificarDetalleCronogramaVacaciones.Id
                                                        select c).FirstOrDefault();
                auxiliar.Inicio = miModificarDetalleCronogramaVacaciones.Inicio;
                auxiliar.Fin = miModificarDetalleCronogramaVacaciones.Fin;
                bd.SaveChanges();
            }
        }

        public void EliminarDetalleCronogramaVacaciones(DetalleCronogramaVacaciones miEliminarDetalleCronogramaVacaciones)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                DetalleCronogramaVacaciones auxiliar = (from c in bd.DetalleCronogramaVacacionesSet
                                                        where c.Id == miEliminarDetalleCronogramaVacaciones.Id
                                                        select c).FirstOrDefault();
                bd.DetalleCronogramaVacacionesSet.Remove(auxiliar);
            }
        }
    }
}
