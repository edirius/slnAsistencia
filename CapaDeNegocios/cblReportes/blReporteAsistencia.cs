using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.cblReportes
{
    public class blReporteAsistencia
    {
        public void ReporteAsistencia(List<Trabajador> miListaTrabajadores, DateTime miFechaInicio, DateTime miFechaFin)
        {
            foreach (Trabajador item in miListaTrabajadores)
            {
                List<Asistencia> miAsistenciaTrabajador = LlenarAsistencia(item, miFechaInicio, miFechaFin);
                List<PermisosDias> miPermisoDiasTrabajador = LlenarPermisos(item, miFechaInicio, miFechaFin);

            }
        }

        public List<Asistencia> LlenarAsistencia(Trabajador miTrabajador, DateTime miFechaInicio, DateTime miFechaFin)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Asistencia> consultaAsistencia = from d in bd.AsistenciaSet
                                                            where d.Trabajador.Id == miTrabajador.Id
                                                            && d.PicadoReloj >= miFechaInicio
                                                            && d.PicadoReloj <= miFechaFin
                                                            select d;
                return consultaAsistencia.ToList();
            }
        }

        public List<PermisosDias> LlenarPermisos(Trabajador miTrabajador, DateTime miFechaInicio, DateTime miFechaFin)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PermisosDias> consultaPermisos = from d in bd.PermisosDiasSet
                                                            where d.PeriodoTrabajador.Trabajador.Id == miTrabajador.Id
                                                            && d.Inicio >= miFechaInicio
                                                            && d.Inicio <= miFechaFin
                                                            select d;
                return consultaPermisos.ToList();
            }
        }
    }
}
