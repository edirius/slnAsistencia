using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blHorarioDia
{
    public class blHorarioDia
    {

        public ICollection<HorarioDia> ListarHorarioDias()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<HorarioDia> consultaHorarioDias = from d in bd.HorarioDiaSet
                                                             select d;
                return consultaHorarioDias.ToList() ;
            }
        }

        public void AgregarHorarioDia(HorarioDia miAgregarHorarioDia)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.HorarioDiaSet.Add(miAgregarHorarioDia);
                bd.SaveChanges();
            }
        }

        public void ModificarHorarioDia(HorarioDia miModificarHorarioDia)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                HorarioDia auxiliar = (from c in bd.HorarioDiaSet
                                       where c.Id == miModificarHorarioDia.Id
                                       select c).FirstOrDefault();
                auxiliar.Dia = miModificarHorarioDia.Dia;
                bd.SaveChanges();
            }
        }

        public void EliminarHorarioDia(HorarioDia miEliminarHorarioDia)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                HorarioDia auxiliar = (from c in bd.HorarioDiaSet
                                       where c.Id == miEliminarHorarioDia.Id
                                       select c).FirstOrDefault();
                bd.HorarioDiaSet.Remove(auxiliar);
            }
        }
    }
}
