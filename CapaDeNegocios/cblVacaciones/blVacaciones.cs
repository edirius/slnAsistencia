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
            cblAsistenciaAnual.cAsistenciaAnual miAsistenciaAnual = new cblAsistenciaAnual.cAsistenciaAnual();
            //List<cblAsistenciaAnual.cAsistenciaAnual> miListaAsistenciaAnual = new List<cblAsistenciaAnual.cAsistenciaAnual>();
            miAsistenciaAnual = miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual[miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual.Count - 1];

            for (int i = 0; i < 12; i++)
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
            miVacaciones.totalDiasComputables = miVacaciones.diasPermisosComputables + miVacaciones.diasPermisosComputables;
            ///

            if (miVacaciones.totalDiasComputables >= 210)
            {
                miVacaciones.diasVacacionesDisponibles = 30 - miVacaciones.diasVacacionesAdelantadas;
            }
            return miVacaciones;
        }
    }
}
