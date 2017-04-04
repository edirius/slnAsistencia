using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blLocal
{
    public class blLocal
    {

        public ICollection<Local> ListarLocales()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Local> consultaLocales = from d in bd.LocalSet
                                                             select d;
                return consultaLocales.ToList() ;
            }
        }

        public void AgregarLocal(Local miAgregarLocal)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.LocalSet.Add(miAgregarLocal);
                bd.SaveChanges();
            }
        }

        public void ModificarLocal(Local miModificarLocal)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Local auxiliar = (from c in bd.LocalSet
                                       where c.Id == miModificarLocal.Id
                                       select c).FirstOrDefault();
                auxiliar.Nombre = miModificarLocal.Nombre;
                bd.SaveChanges();
            }
        }

        public void EliminarLocal(Local miEliminarLocal)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Local auxiliar = (from c in bd.LocalSet
                                       where c.Id == miEliminarLocal.Id
                                       select c).FirstOrDefault();
                bd.LocalSet.Remove(auxiliar);
            }
        }
    }
}
