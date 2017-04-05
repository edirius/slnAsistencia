using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blAsistencia
{
    public class blAsistencia
    {

        public ICollection<Asistencia> ListarAsistencias(Trabajador miTrabajador)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Asistencia> consultaAsistencias = from d in bd.AsistenciaSet
                                                             where d.Trabajador.Id  == miTrabajador.Id
                                                             select d;
                return consultaAsistencias.ToList() ;
            }
        }

        public void AgregarAsistencia(Asistencia miAgregarAsistencia)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.AsistenciaSet.Add(miAgregarAsistencia);
                bd.SaveChanges();
            }
        }

        public void ModificarAsistencia(Asistencia miModificarAsistencia)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Asistencia auxiliar = (from c in bd.AsistenciaSet
                                       where c.Id == miModificarAsistencia.Id
                                       select c).FirstOrDefault();
                auxiliar.PicadoReloj = miModificarAsistencia.PicadoReloj;
                bd.SaveChanges();
            }
        }

        public void EliminarAsistencia (Asistencia miEliminarAsistencia)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Asistencia auxiliar = (from c in bd.AsistenciaSet
                                       where c.Id == miEliminarAsistencia.Id
                                       select c).FirstOrDefault();
                bd.AsistenciaSet.Remove(auxiliar);
            }
        }
    }
}
