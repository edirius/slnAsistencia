using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blOficina
{
    public class blOficina
    {

        public ICollection<Oficina> ListarOficinas()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Oficina> consultaOficinas = from d in bd.OficinaSet
                                                             select d;
                return consultaOficinas.ToList() ;
            }
        }

        public void AgregarOficina(Oficina miAgregarOficina)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.OficinaSet.Add(miAgregarOficina);
                bd.SaveChanges();
            }
        }

        public void ModificarOficina(Oficina miModificarOficina)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Oficina auxiliar = (from c in bd.OficinaSet
                                       where c.Id == miModificarOficina.Id
                                       select c).FirstOrDefault();
                auxiliar.Nombre = miModificarOficina.Nombre;
                bd.SaveChanges();
            }
        }

        public void EliminarOficina(Oficina miEliminarOficina)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Oficina auxiliar = (from c in bd.OficinaSet
                                       where c.Id == miEliminarOficina.Id
                                       select c).FirstOrDefault();
                bd.OficinaSet.Remove(auxiliar);
            }
        }
    }
}
