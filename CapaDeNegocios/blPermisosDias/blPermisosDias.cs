using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blPermisosDias
{
    public class blPermisosDias
    {

        public ICollection<PermisosDias> ListarPermisosDias()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PermisosDias> consultaPermisosDias = from d in bd.PermisosDiasSet
                                                             select d;
                return consultaPermisosDias.ToList() ;
            }
        }

        public void AgregarPermisosDias(PermisosDias miAgregarPermisosDias)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.PermisosDiasSet.Add(miAgregarPermisosDias);
                bd.SaveChanges();
            }
        }

        public void ModificarPermisosDias(PermisosDias miModificarPermisosDias)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                PermisosDias auxiliar = (from c in bd.PermisosDiasSet
                                       where c.Id == miModificarPermisosDias.Id
                                       select c).FirstOrDefault();
                bd.SaveChanges();
            }
        }

        public void EliminarPermisosDias(PermisosDias miEliminarPermisosDias)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                PermisosDias auxiliar = (from c in bd.PermisosDiasSet
                                       where c.Id == miEliminarPermisosDias.Id
                                       select c).FirstOrDefault();
                bd.PermisosDiasSet.Remove(auxiliar);
            }
        }
    }
}
