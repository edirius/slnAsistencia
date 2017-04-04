using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blDiaFestivo
{
    public class blDiaFestivo
    {

        public ICollection<DiaFestivo> ListarDiaFestivos()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<DiaFestivo> consultaDiaFestivos = from d in bd.DiaFestivoSet
                                                             select d;
                return consultaDiaFestivos.ToList() ;
            }
        }

        public void AgregarDiaFestivo(DiaFestivo miAgregarDiaFestivo)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.DiaFestivoSet.Add(miAgregarDiaFestivo);
                bd.SaveChanges();
            }
        }

        public void ModificarDiaFestivo(DiaFestivo miModificarDiaFestivo)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                DiaFestivo auxiliar = (from c in bd.DiaFestivoSet
                                       where c.Id == miModificarDiaFestivo.Id
                                       select c).FirstOrDefault();
                auxiliar.Dia = miModificarDiaFestivo.Dia;
                auxiliar.Nombre = miModificarDiaFestivo.Nombre;
                bd.SaveChanges();
            }
        }

        public void EliminarDiaFestivo(DiaFestivo miEliminarDiaFestivo)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                DiaFestivo auxiliar = (from c in bd.DiaFestivoSet
                                       where c.Id == miEliminarDiaFestivo.Id
                                       select c).FirstOrDefault();
                bd.DiaFestivoSet.Remove(auxiliar);
            }
        }
    }
}
