using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blHorarioSemana
{
    public class blHorarioSemana
    {

        public ICollection<HorarioSemana> ListarHorarioSemanas()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<HorarioSemana> consultaHorarioSemanas = from d in bd.HorarioSemanaSet
                                                             select d;
                return consultaHorarioSemanas.ToList() ;
            }
        }

        public void AgregarHorarioSemana(HorarioSemana miAgregarHorarioSemana)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.HorarioSemanaSet.Add(miAgregarHorarioSemana);
                bd.SaveChanges();
            }
        }

        public void ModificarHorarioSemana(HorarioSemana miModificarHorarioSemana)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                HorarioSemana auxiliar = (from c in bd.HorarioSemanaSet
                                       where c.Id == miModificarHorarioSemana.Id
                                       select c).FirstOrDefault();
                auxiliar.Nombre = miModificarHorarioSemana.Nombre;
                bd.SaveChanges();
            }
        }

        public void EliminarHorarioSemana(HorarioSemana miEliminarHorarioSemana)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                HorarioSemana auxiliar = (from c in bd.HorarioSemanaSet
                                       where c.Id == miEliminarHorarioSemana.Id
                                       select c).FirstOrDefault();
                bd.HorarioSemanaSet.Remove(auxiliar);
            }
        }
    }
}
