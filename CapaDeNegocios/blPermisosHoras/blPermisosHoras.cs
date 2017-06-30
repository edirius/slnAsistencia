using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blPermisosHoras
{
    public class blPermisosHoras
    {

        public ICollection<PermisosHoras> ListarPermisosHoras()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PermisosHoras> consultaPermisosHoras = from d in bd.PermisosHorasSet
                                                             select d;
                return consultaPermisosHoras.ToList() ;
            }
        }

        public void AgregarPermisosHoras(PermisosHoras miAgregarPermisosHoras)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.TipoPermisosSet.Attach(miAgregarPermisosHoras.TipoPermisos);
                bd.PeriodoTrabajadorSet.Attach(miAgregarPermisosHoras.PeriodoTrabajador);
                bd.PermisosHorasSet.Add(miAgregarPermisosHoras);
                bd.SaveChanges();
            }
        }

        public void ModificarPermisosHoras(PermisosHoras miModificarPermisosHoras)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                PermisosHoras auxiliar = (from c in bd.PermisosHorasSet
                                       where c.Id == miModificarPermisosHoras.Id
                                       select c).FirstOrDefault();
                auxiliar.Id = miModificarPermisosHoras.Id;
                auxiliar.Fecha = miModificarPermisosHoras.Fecha;
                auxiliar.Inicio = miModificarPermisosHoras.Inicio;
                auxiliar.Fin = miModificarPermisosHoras.Fin;
                bd.SaveChanges();
            }
        }

        public void EliminarPermisosHoras(PermisosHoras miEliminarPermisosHoras)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                PermisosHoras auxiliar = (from c in bd.PermisosHorasSet
                                       where c.Id == miEliminarPermisosHoras.Id
                                       select c).FirstOrDefault();
                bd.PermisosHorasSet.Remove(auxiliar);
            }
        }
    }
}
