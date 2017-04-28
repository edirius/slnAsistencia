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
        public ICollection<Vacaciones> ListarVacaciones()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Vacaciones> consultaVacaciones = from d in bd.VacacionesSet
                                                                                          select d;
                return consultaVacaciones.ToList();
            }
        }

        public void AgregarVacaciones(Vacaciones miAgregarVacaciones)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.AsistenciaPeriodoLaboradoSet.Attach(miAgregarVacaciones.AsistenciaPeriodoLaborado);
                bd.VacacionesSet.Add(miAgregarVacaciones);
                bd.SaveChanges();
            }
        }

        public void ModificarVacaciones(Vacaciones miModificarVacaciones)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Vacaciones auxiliar = (from c in bd.VacacionesSet
                                                      where c.Id == miModificarVacaciones.Id
                                                      select c).FirstOrDefault();
                auxiliar.Id = miModificarVacaciones.Id;
                auxiliar.Inicio = miModificarVacaciones.Inicio;
                auxiliar.Fin = miModificarVacaciones.Fin;
                auxiliar.DiasVacacionesAdelantadas = miModificarVacaciones.DiasVacacionesAdelantadas;
                auxiliar.DiasVacacionesDisponibles = miModificarVacaciones.DiasVacacionesDisponibles;
                bd.SaveChanges();
            }
        }

        public void EliminarVacaciones(Vacaciones miEliminarVacaciones)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Vacaciones auxiliar = (from c in bd.VacacionesSet
                                                      where c.Id == miEliminarVacaciones.Id
                                                      select c).FirstOrDefault();
                bd.VacacionesSet.Remove(auxiliar);
            }
        }

        public bool AsignarVacaciones(CapaDeNegocios.cblAsistenciaAnual.cAsistenciaPeriodoTrabajador miAsistenciaPeriodo, CapaDeNegocios.cblAsistenciaAnual.cAsistenciaPeriodoLaborado miAsistenciaAnual, CapaDeNegocios.cblVacaciones.cVacaciones miVacaciones)
        {
            if (miAsistenciaPeriodo.miListaAsistenciaPeriodoLaborado.Count >= 3)
            {
                if (miAsistenciaPeriodo.miListaAsistenciaPeriodoLaborado[miAsistenciaPeriodo.miListaAsistenciaPeriodoLaborado.Count - 2].miVacaciones.vacacionesEfectuadas == false && miAsistenciaPeriodo.miListaAsistenciaPeriodoLaborado[miAsistenciaPeriodo.miListaAsistenciaPeriodoLaborado.Count - 3].miVacaciones.vacacionesEfectuadas == false && miVacaciones.vacacionesEfectuadas == false)
                {
                    throw new cReglaNegociosException("No se puede postergar xq ya existen 2 periodos acumulados.");
                }
                else
                {
                    miAsistenciaAnual.miVacaciones = miVacaciones;
                }
            }
            else
            {
                miAsistenciaAnual.miVacaciones = miVacaciones;
            }
            return true;
        }

        public cVacaciones CalculoDiasAsistenciaAnual(cblAsistenciaAnual.cAsistenciaPeriodoTrabajador miAsistenciaPeriodoTrabajador)
        {
            cblVacaciones.cVacaciones miVacaciones = new cblVacaciones.cVacaciones();
            cblAsistenciaAnual.cAsistenciaPeriodoLaborado miAsistenciaAnual = miAsistenciaPeriodoTrabajador.miListaAsistenciaPeriodoLaborado[miAsistenciaPeriodoTrabajador.miListaAsistenciaPeriodoLaborado.Count - 1];
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
            miVacaciones.diasTotalComputables = miVacaciones.diasLaborados + miVacaciones.diasPermisosNoComputables;
            miVacaciones.diasVacacionesAdelantadas = miVacaciones.diasPermisosComputables;
            ///

            if (miVacaciones.diasTotalComputables >= 210)
            {
                miVacaciones.diasVacacionesDisponibles = 30 - miVacaciones.diasVacacionesAdelantadas;
            }
            return miVacaciones;
        }

        public CapaDeNegocios.cblAsistenciaAnual.cAsistenciaMeses CalculoDiasAsistenciaMeses(int Año, int Mes, cblAsistenciaAnual.cAsistenciaPeriodoTrabajador miAsistenciaPeriodoTrabajador)
        {
            cblAsistenciaAnual.cAsistenciaPeriodoLaborado miAsistenciaAnual = miAsistenciaPeriodoTrabajador.miListaAsistenciaPeriodoLaborado[miAsistenciaPeriodoTrabajador.miListaAsistenciaPeriodoLaborado.Count - 1];
            cblAsistenciaAnual.cAsistenciaMeses miAsistenciaMeses = new cblAsistenciaAnual.cAsistenciaMeses();

            int nroMeses = Math.Abs((miAsistenciaAnual.fechaFin.Month - miAsistenciaAnual.fechaInicio.Month) + 12 * (miAsistenciaAnual.fechaFin.Year - miAsistenciaAnual.fechaInicio.Year));
            for (int i = 0; i <= nroMeses; i++)
            {
                if (Año == miAsistenciaAnual.miListaAsistenciaMeses[i].nombreAño && Convert.ToDateTime("01/" + Mes + "/" + Año).ToString("MMMM") == miAsistenciaAnual.miListaAsistenciaMeses[i].nombreMes)
                {
                    foreach (cblAsistenciaAnual.cAsistenciaDia item in miAsistenciaAnual.miListaAsistenciaMeses[i].miListaAsistenciaDias)
                    {
                        if (item.asistencia == true)
                        {
                            miAsistenciaAnual.miListaAsistenciaMeses[i].diasLaborados += 1;
                        }
                        if (item.miPermiso != null)
                        {
                            if (item.miPermiso.TipoPermisos.Computable == false)
                            {
                                miAsistenciaAnual.miListaAsistenciaMeses[i].diasPermisos += 1;
                            }
                            else
                            {
                                miAsistenciaAnual.miListaAsistenciaMeses[i].diasFaltas += 1;
                            }
                        }
                    }
                    miAsistenciaAnual.miListaAsistenciaMeses[i].diasTotal = miAsistenciaAnual.miListaAsistenciaMeses[i].diasLaborados + miAsistenciaAnual.miListaAsistenciaMeses[i].diasPermisos;
                    miAsistenciaMeses = miAsistenciaAnual.miListaAsistenciaMeses[i];
                }
            }
            return miAsistenciaMeses;
        }
    }
}
