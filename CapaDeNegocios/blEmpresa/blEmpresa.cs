using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blEmpresa
{
    public class blEmpresa
    {

        public ICollection<Empresa> ListarEmpresas()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Empresa> consultaEmpresas = from d in bd.EmpresaSet
                                                       select d;

                return consultaEmpresas.ToList();
            }
        }

        public void AgregarEmpresa(Empresa miAgregarEmpresa)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.EmpresaSet.Add(miAgregarEmpresa);
                bd.SaveChanges();
            }
        }

        public void ModificarEmpresa(Empresa miModificarEmpresa)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Empresa auxiliar = (from c in bd.EmpresaSet
                                       where c.Id == miModificarEmpresa.Id
                                       select c).FirstOrDefault();
                auxiliar.Nombre = miModificarEmpresa.Nombre;
                bd.SaveChanges();
            }
        }

        public void EliminarEmpresa(Empresa miEliminarEmpresa)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Empresa auxiliar = (from c in bd.EmpresaSet
                                       where c.Id == miEliminarEmpresa.Id
                                       select c).FirstOrDefault();
                bd.EmpresaSet.Remove(auxiliar);
            }
        }
    }
}
