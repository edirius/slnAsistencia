using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blTipoPermisos
{
    public class blTipoPermisos
    {

        public ICollection<TipoPermisos> ListarTipoPermisos()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<TipoPermisos> consultaTipoPermisos = from d in bd.TipoPermisosSet
                                                             select d;
                return consultaTipoPermisos.ToList() ;
            }
        }

        public void AgregarTipoPermisos(TipoPermisos miAgregarTipoPermisos)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.TipoPermisosSet.Add(miAgregarTipoPermisos);
                bd.SaveChanges();
            }
        }

        public void ModificarTipoPermisos(TipoPermisos miModificarTipoPermisos)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                TipoPermisos auxiliar = (from c in bd.TipoPermisosSet
                                       where c.Id == miModificarTipoPermisos.Id
                                       select c).FirstOrDefault();
                auxiliar.Nombre = miModificarTipoPermisos.Nombre;
                auxiliar.Computable = miModificarTipoPermisos.Computable;
                bd.SaveChanges();
            }
        }

        public void EliminarTipoPermisos(TipoPermisos miEliminarTipoPermisos)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                TipoPermisos auxiliar = (from c in bd.TipoPermisosSet
                                       where c.Id == miEliminarTipoPermisos.Id
                                       select c).FirstOrDefault();
                bd.TipoPermisosSet.Remove(auxiliar);
            }
        }
    }
}
