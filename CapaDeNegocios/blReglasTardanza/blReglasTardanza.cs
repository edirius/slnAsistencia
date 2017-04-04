using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blReglasTardanza
{
    public class blReglasTardanza
    {

        public ICollection<ReglasTardanza> ListarReglasTardanzas()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<ReglasTardanza> consultaReglasTardanzas = from d in bd.ReglasTardanzaSet
                                                             select d;
                return consultaReglasTardanzas.ToList() ;
            }
        }

        public void AgregarReglasTardanza(ReglasTardanza miAgregarReglasTardanza)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.ReglasTardanzaSet.Add(miAgregarReglasTardanza);
                bd.SaveChanges();
            }
        }

        public void ModificarReglasTardanza(ReglasTardanza miModificarReglasTardanza)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                ReglasTardanza auxiliar = (from c in bd.ReglasTardanzaSet
                                       where c.Id == miModificarReglasTardanza.Id
                                       select c).FirstOrDefault();
                auxiliar.TiempoTardanza = miModificarReglasTardanza.TiempoTardanza;
                bd.SaveChanges();
            }
        }

        public void EliminarReglasTardanza(ReglasTardanza miEliminarReglasTardanza)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                ReglasTardanza auxiliar = (from c in bd.ReglasTardanzaSet
                                       where c.Id == miEliminarReglasTardanza.Id
                                       select c).FirstOrDefault();
                bd.ReglasTardanzaSet.Remove(auxiliar);
            }
        }
    }
}
