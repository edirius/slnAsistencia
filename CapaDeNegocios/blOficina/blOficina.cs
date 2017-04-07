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

        public ICollection<Oficina> ListarOficinas(Local miLocal)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                var consultaOficinas = from d in bd.OficinaSet.Include("OficinasHijas")
                                                        where (d.Local.Id == miLocal.Id) /*&& (d.Oficina2 == null)*/
                                                        select d;

               
                return  consultaOficinas.ToList() ;
            }
        }

       

        public void AgregarOficina(Oficina miAgregarOficina)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.LocalSet.Attach(miAgregarOficina.Local);
                if (miAgregarOficina.OficinaPadre != null)
                {
                    bd.OficinaSet.Attach(miAgregarOficina.OficinaPadre);
                }

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
