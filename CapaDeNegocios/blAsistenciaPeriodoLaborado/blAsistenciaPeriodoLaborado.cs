using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blAsistenciaPeriodoLaborado
{
    public class blAsistenciaPeriodoLaborado
    {
        public ICollection<AsistenciaPeriodoLaborado> ListarAsistenciaPeriodoLaborado()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<AsistenciaPeriodoLaborado> consultaAsistenciaPeriodoLaborado = from d in bd.AsistenciaPeriodoLaboradoSet
                                                                                          select d;
                return consultaAsistenciaPeriodoLaborado.ToList() ;
            }
        }

        public void AgregarAsistenciaPeriodoLaborado(AsistenciaPeriodoLaborado miAgregarAsistenciaPeriodoLaborado)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.PeriodoTrabajadorSet.Attach(miAgregarAsistenciaPeriodoLaborado.PeriodoTrabajador);
                bd.AsistenciaPeriodoLaboradoSet.Add(miAgregarAsistenciaPeriodoLaborado);
                bd.SaveChanges();
            }
        }

        public void ModificarAsistenciaPeriodoLaborado(AsistenciaPeriodoLaborado miModificarAsistenciaPeriodoLaborado)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                AsistenciaPeriodoLaborado auxiliar = (from c in bd.AsistenciaPeriodoLaboradoSet
                                       where c.Id == miModificarAsistenciaPeriodoLaborado.Id
                                       select c).FirstOrDefault();
                auxiliar.Id = miModificarAsistenciaPeriodoLaborado.Id;
                auxiliar.Inicio = miModificarAsistenciaPeriodoLaborado.Inicio;
                auxiliar.Fin = miModificarAsistenciaPeriodoLaborado.Fin;
                bd.SaveChanges();
            }
        }

        public void EliminarAsistenciaPeriodoLaborado(AsistenciaPeriodoLaborado miEliminarAsistenciaPeriodoLaborado)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                AsistenciaPeriodoLaborado auxiliar = (from c in bd.AsistenciaPeriodoLaboradoSet
                                       where c.Id == miEliminarAsistenciaPeriodoLaborado.Id
                                       select c).FirstOrDefault();
                bd.AsistenciaPeriodoLaboradoSet.Remove(auxiliar);
            }
        }
    }
}
