using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeNegocios;
using CapaEntities;

namespace CapaDeNegocios.cblVacaciones
{
    public class blVacaciones
    {
        public cVacaciones CalculoDiasAsistencia(cblAsistenciaAnual.cAsistenciaPeriodoTrabajador miAsistenciaPeriodoTrabajador)
        {
            cblVacaciones.cVacaciones miVacaciones = new cblVacaciones.cVacaciones();
            cblAsistenciaAnual.cAsistenciaAnual miAsistenciaAnual = miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual[miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual.Count - 1];
            int nroMeses = Math.Abs((miAsistenciaAnual.fechaFin.Month - miAsistenciaAnual.fechaInicio.Month) + 12 * (miAsistenciaAnual.fechaFin.Year - miAsistenciaAnual.fechaInicio.Year));
            for (int i = 0; i <= nroMeses; i++)
            {
                foreach (cblAsistenciaAnual.cAsistenciaDia item in miAsistenciaAnual.miListaAsistenciaMeses[i].miListaAsistenciaDias)
                {
                    if (item.asistencia == true)
                    {
                        miVacaciones.diasLaborados += 1;
                    }
                    if (item.miPermiso != null)
                    {
                        if (item.miPermiso.TipoPermisos.Computable == true)
                        {
                            miVacaciones.diasPermisosComputables += 1;
                        }
                        else
                        {
                            miVacaciones.diasPermisosNoComputables += 1;
                        }
                    }
                }
            }
            miVacaciones.totalDiasComputables = miVacaciones.diasLaborados + miVacaciones.diasPermisosNoComputables;
            miVacaciones.diasVacacionesAdelantadas = miVacaciones.diasPermisosComputables;
            ///

            if (miVacaciones.totalDiasComputables >= 210)
            {
                miVacaciones.diasVacacionesDisponibles = 30 - miVacaciones.diasVacacionesAdelantadas;
            }
            return miVacaciones;
        }
    }
}
