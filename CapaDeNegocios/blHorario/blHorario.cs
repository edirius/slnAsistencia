using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blHorario
{
    public class blHorario
    {

        public ICollection<Horario> ListarHorarios()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Horario> consultaHorarios = from d in bd.HorarioSet
                                                             select d;
                return consultaHorarios.ToList() ;
            }
        }

        public void AgregarHorario(Horario miAgregarHorario)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.HorarioSet.Add(miAgregarHorario);
                bd.SaveChanges();
            }
        }

        public void ModificarHorario(Horario miModificarHorario)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Horario auxiliar = (from c in bd.HorarioSet
                                       where c.Id == miModificarHorario.Id
                                       select c).FirstOrDefault();
                auxiliar.Entrada = miModificarHorario.Entrada;
                auxiliar.Salida = miModificarHorario.Salida;
                auxiliar.Nombre = miModificarHorario.Nombre;
                bd.SaveChanges();
            }
        }

        public void EliminarHorario(Horario miEliminarHorario)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Horario auxiliar = (from c in bd.HorarioSet
                                       where c.Id == miEliminarHorario.Id
                                       select c).FirstOrDefault();
                bd.HorarioSet.Remove(auxiliar);
            }
        }
    }
}
