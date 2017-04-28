using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeNegocios;
using CapaEntities;

namespace CapaDeNegocios.blVacaciones
{
    public class blVacaciones
    {
        public ICollection<Vacaciones> ListarVacaciones()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Vacaciones> consultaVacaciones = from d in bd.VacacionesSet
                                                                                          select d;
                return consultaVacaciones.ToList();
            }
        }

        public void AgregarVacaciones(Vacaciones miAgregarVacaciones)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.AsistenciaPeriodoLaboradoSet.Attach(miAgregarVacaciones.AsistenciaPeriodoLaborado);
                bd.VacacionesSet.Add(miAgregarVacaciones);
                bd.SaveChanges();
            }
        }

        public void ModificarVacaciones(Vacaciones miModificarVacaciones)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Vacaciones auxiliar = (from c in bd.VacacionesSet
                                                      where c.Id == miModificarVacaciones.Id
                                                      select c).FirstOrDefault();
                auxiliar.Id = miModificarVacaciones.Id;
                auxiliar.Inicio = miModificarVacaciones.Inicio;
                auxiliar.Fin = miModificarVacaciones.Fin;
                auxiliar.DiasVacacionesAdelantadas = miModificarVacaciones.DiasVacacionesAdelantadas;
                auxiliar.DiasVacacionesDisponibles = miModificarVacaciones.DiasVacacionesDisponibles;
                bd.SaveChanges();
            }
        }

        public void EliminarVacaciones(Vacaciones miEliminarVacaciones)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Vacaciones auxiliar = (from c in bd.VacacionesSet
                                                      where c.Id == miEliminarVacaciones.Id
                                                      select c).FirstOrDefault();
                bd.VacacionesSet.Remove(auxiliar);
            }
        }
    }
}
