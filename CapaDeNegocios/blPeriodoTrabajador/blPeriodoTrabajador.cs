using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blPeriodoTrabajador
{
    public class blPeriodoTrabajador
    {
        public ICollection<PeriodoTrabajador> ListarPeriodoTrabajador(Trabajador miTrabajador)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PeriodoTrabajador> consultaPeriodoTrabajador = from d in bd.PeriodoTrabajadorSet select d;
                return consultaPeriodoTrabajador.ToList();
            }
        }

        public void AgregarPeriodoTrabajador(PeriodoTrabajador miAgregarPeriodoTrabajador)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.PeriodoTrabajadorSet.Add(miAgregarPeriodoTrabajador);
                bd.SaveChanges();
            }
        }

        public void ModificarPeriodoTrabajador(PeriodoTrabajador miModificarPeriodoTrabajador)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                PeriodoTrabajador auxiliar = (from c in bd.PeriodoTrabajadorSet
                                              where c.Id == miModificarPeriodoTrabajador.Id
                                              select c).FirstOrDefault();
                auxiliar.Id = miModificarPeriodoTrabajador.Id;
                auxiliar.Inicio = miModificarPeriodoTrabajador.Inicio;
                auxiliar.Fin = miModificarPeriodoTrabajador.Fin;
                bd.SaveChanges();
            }
        }

        public void EliminarPeriodoTrabajador(PeriodoTrabajador miEliminarPeriodoTrabajador)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                PeriodoTrabajador auxiliar = (from c in bd.PeriodoTrabajadorSet
                                              where c.Id == miEliminarPeriodoTrabajador.Id
                                              select c).FirstOrDefault();
                bd.PeriodoTrabajadorSet.Remove(auxiliar);
            }
        }
    }
}
