using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeNegocios;
using CapaEntities;

namespace CapaDeNegocios.blDia
{
    public class blDia
    {
        //public void AgregarDia(Dia miDia)
        //{
        //    using (mAsistenciaContainer bd = new mAsistenciaContainer())
        //    {
        //        if (miDia.HorarioDia != null)
        //        {
        //            bd.HorarioSemanaSet.Attach(miDia.HorarioSemana);
        //        }
                 
        //        bd.DiaSet.Add(miDia);

        //        bd.SaveChanges();
        //    }
        //}

        //public void ModificarDia(Dia miDia)
        //{
        //    using (mAsistenciaContainer bd = new mAsistenciaContainer())
        //    {
        //        Dia auxiliar = (from c in bd.DiaSet.Include("HorarioDia")
        //                                  where c.Id == miDia.Id
        //                                  select c).FirstOrDefault();
        //        auxiliar.HorarioDia  = miDia.HorarioDia;
        //        bd.SaveChanges();
        //    }
        //}
    }
}
