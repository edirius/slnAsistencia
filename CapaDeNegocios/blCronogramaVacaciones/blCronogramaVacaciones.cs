using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blCronogramaVacaciones
{
    public class blCronogramaVacaciones
    {

        public ICollection<CronogramaVacaciones> ListarCronogramaVacaciones()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<CronogramaVacaciones> consultaCronogramaVacaciones = from d in bd.CronogramaVacacionesSet
                                                             select d;
                return consultaCronogramaVacaciones.ToList() ;
            }
        }

        public void AgregarCronogramaVacaciones(CronogramaVacaciones miAgregarCronogramaVacaciones)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.CronogramaVacacionesSet.Add(miAgregarCronogramaVacaciones);
                bd.SaveChanges();
            }
        }

        public void ModificarCronogramaVacaciones(CronogramaVacaciones miModificarCronogramaVacaciones)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                CronogramaVacaciones auxiliar = (from c in bd.CronogramaVacacionesSet
                                       where c.Id == miModificarCronogramaVacaciones.Id
                                       select c).FirstOrDefault();
                auxiliar.Anio = miModificarCronogramaVacaciones.Anio;
                auxiliar.Inicio = miModificarCronogramaVacaciones.Inicio;
                auxiliar.Fin = miModificarCronogramaVacaciones.Fin;
                bd.SaveChanges();
            }
        }

        public void EliminarCronogramaVacaciones(CronogramaVacaciones miEliminarCronogramaVacaciones)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                CronogramaVacaciones auxiliar = (from c in bd.CronogramaVacacionesSet
                                       where c.Id == miEliminarCronogramaVacaciones.Id
                                       select c).FirstOrDefault();
                bd.CronogramaVacacionesSet.Remove(auxiliar);
            }
        }
    }
}
