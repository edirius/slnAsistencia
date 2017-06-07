using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;

namespace CapaDeNegocios.cblReportesAsistencia
{
    public class blReporteAsistencia
    {
        public cReporteAsistencia LlenarReporteAsistencia(ICollection<Trabajador> MisTrabajadores, DateTime FechaInicio, DateTime FechaFin)
        {
            TimeSpan ts = FechaFin - FechaInicio;
            int dias = ts.Days;
            cReporteAsistencia auxReporte = new cReporteAsistencia();
            auxReporte.FechaInicio = FechaInicio;
            auxReporte.FechaFin = FechaFin;
            auxReporte.titulo = "Reporte de Asistencia";
            foreach (Trabajador auxTrabajador in MisTrabajadores)
            {
                //creando nuevo detalleReporteAsistencia que corresponde por cada trabajador para añadir al objeto reporte 
                cDetalleReporteAsistenciaXTrabajador auxDetalleReporteAsistenciaXTrabajador = new cDetalleReporteAsistenciaXTrabajador();
                auxDetalleReporteAsistenciaXTrabajador.miTrabajador = auxTrabajador;
                
                DateTime auxFecha = auxReporte.FechaInicio;
                for (int i = 0; i <= dias; i++)
                {
                    //creando nuevo detalleAsistencia por dia para el objeto detalleReporteAsistencia
                    cDetalleAsistenciaXDia auxDetalleAsistenciaXDia = new cDetalleAsistenciaXDia();
                    auxDetalleAsistenciaXDia.Dia = auxFecha;
                    //llenando las Asistencias para la fecha
                    auxDetalleAsistenciaXDia.ListaAsistencia = LlenarAsistencia(auxTrabajador, auxFecha);
                    //llenando los horarios
                    auxDetalleAsistenciaXDia.ListaHorario = TraerHorarioSemana(auxFecha.DayOfWeek, auxTrabajador, auxFecha);
                    //llenando los permisos
                    auxDetalleAsistenciaXDia.ListaPermisos = TraerListaPermisosDias(auxFecha, auxTrabajador);
                    //llenando los permisosHoras
                    auxDetalleAsistenciaXDia.ListaPermisosHoras = TraerListaPermisosHoras(auxFecha, auxTrabajador);
                    //llenando los dias festivos
                    auxDetalleAsistenciaXDia.ListaDiaFestivo = TraerListaDiaFestivo(auxFecha); 
                    auxFecha = auxFecha.AddDays(1);
                    
                    auxDetalleReporteAsistenciaXTrabajador.detallesAsistenciasXDia.Add(auxDetalleAsistenciaXDia);
                }
                auxReporte.detallesReporteAsistenciaXTrabajador.Add(auxDetalleReporteAsistenciaXTrabajador);
            }
            return auxReporte;
        }

        public List<Horario> TraerHorarioSemana(DayOfWeek DiaSemana, Trabajador miTrabajador, DateTime Fecha)
        {
            HorarioSemana auxHorarioSemana = new HorarioSemana();
            List<Horario> auxListaHorarios = new List<Horario>();
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PeriodoTrabajador> consultaPeriodo = from d in bd.PeriodoTrabajadorSet.Include("HorarioSemana").Include("HorarioSemana.Dia").Include("HorarioSemana.Dia.HorarioDia").Include("HorarioSemana.Dia.HorarioDia.Horario")
                                                                where Fecha >= d.Inicio &&
                                                                Fecha <= d.Fin && 
                                                                d.Trabajador.Id == miTrabajador.Id
                                                                select d;
                if (consultaPeriodo.Count() > 0)
                {
                    PeriodoTrabajador auxPeriodo = consultaPeriodo.FirstOrDefault();

                    auxHorarioSemana = auxPeriodo.HorarioSemana;

                    foreach (Dia auxDia in auxHorarioSemana.Dia)
                    {
                        if (auxDia.HorarioDia != null)
                        {
                            switch (DiaSemana)
                            {
                                case DayOfWeek.Sunday:
                                    if (auxDia.NombreDiaSemana == "Domingo")
                                    {
                                        auxListaHorarios = auxDia.HorarioDia.Horario.ToList();
                                    }
                                    break;
                                case DayOfWeek.Monday:
                                    if (auxDia.NombreDiaSemana == "Lunes")
                                    {
                                        auxListaHorarios = auxDia.HorarioDia.Horario.ToList();
                                    }
                                    break;
                                case DayOfWeek.Tuesday:
                                    if (auxDia.NombreDiaSemana == "Martes")
                                    {
                                        auxListaHorarios = auxDia.HorarioDia.Horario.ToList();
                                    }
                                    break;
                                case DayOfWeek.Wednesday:
                                    if (auxDia.NombreDiaSemana == "Miercoles")
                                    {
                                        auxListaHorarios = auxDia.HorarioDia.Horario.ToList();
                                    }
                                    break;
                                case DayOfWeek.Thursday:
                                    if (auxDia.NombreDiaSemana == "Jueves")
                                    {
                                        auxListaHorarios = auxDia.HorarioDia.Horario.ToList();
                                    }
                                    break;
                                case DayOfWeek.Friday:
                                    if (auxDia.NombreDiaSemana == "Viernes")
                                    {
                                        auxListaHorarios = auxDia.HorarioDia.Horario.ToList();
                                    }
                                    break;
                                case DayOfWeek.Saturday:
                                    if (auxDia.NombreDiaSemana == "Sabado")
                                    {
                                        auxListaHorarios = auxDia.HorarioDia.Horario.ToList();
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                       
                    }
                    return auxListaHorarios;
                }
                   else
                {
                    return null;
                }
            }
        }

       public List<PermisosDias>  TraerListaPermisosDias (DateTime auxFecha, Trabajador auxTrabajador )
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PeriodoTrabajador> consultaPeriodo = from d in bd.PeriodoTrabajadorSet
                                                                where auxFecha >= d.Inicio &&
                                                                auxFecha <= d.Fin &&
                                                                d.Trabajador.Id == auxTrabajador.Id
                                                                select d;
                if (consultaPeriodo.Count() > 0)
                {
                    IQueryable<PermisosDias> consultaPermisos = from d in bd.PermisosDiasSet
                                                                where (consultaPeriodo.FirstOrDefault().Trabajador.Id == auxTrabajador.Id
                                                                && auxFecha > d.Inicio
                                                                && auxFecha < d.Fin)
                                                                select d;
                    return consultaPermisos.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        public List<DiaFestivo> TraerListaDiaFestivo (DateTime auxFecha)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<DiaFestivo> consultaDiaFestivo = from d in bd.DiaFestivoSet
                                                            where
                                                            auxFecha.Year == d.Dia.Year &&
                                                            auxFecha.Month == d.Dia.Month &&
                                                            auxFecha.Day == d.Dia.Day
                                                            select d;
                if (consultaDiaFestivo.Count() > 0)
                {
                    return consultaDiaFestivo.ToList();
                }

                else
                {
                    return null;
                }
            }
        } 

        public List<PermisosHoras> TraerListaPermisosHoras (DateTime auxFecha, Trabajador auxTrabajador)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PeriodoTrabajador> consultaPeriodo = from d in bd.PeriodoTrabajadorSet
                                                                where auxFecha >= d.Inicio &&
                                                                auxFecha <= d.Fin &&
                                                                d.Trabajador.Id == auxTrabajador.Id
                                                                select d;
                if (consultaPeriodo.Count() > 0)
                {
                    IQueryable<PermisosHoras> consultaPermisos = from d in bd.PermisosHorasSet
                                                                 where (consultaPeriodo.FirstOrDefault().Trabajador.Id == auxTrabajador.Id
                                                                 && auxFecha > d.Fin
                                                                 && auxFecha < d.Fin)
                                                                 select d;
                    return consultaPermisos.ToList();
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// Trae y llena el detalleHorario comparando el horario y la lista de asistencia del trabajador por dia  
        /// </summary>
        /// <param name="oHorario">Objeto Horario</param>
        /// <param name="ListaAsistenciaDelDia">Lista de Asistencia del Trabajador por dia</param>
        /// <returns></returns>
        //public cDetalleHorario TraerAuxDetalleHorario(Horario oHorario, List<Asistencia> ListaAsistenciaDelDia )
        //{
        //    cDetalleHorario auxDetalleHorario = new cDetalleHorario();

        //    foreach (Asistencia auxAsistencia in ListaAsistenciaDelDia)
        //    {
        //        //Verifica que esta dentro del rango de Inicio y Fin de Picado del horario de Entrada
        //        if (auxAsistencia.PicadoReloj.TimeOfDay >= oHorario.InicioPicadoEntrada.TimeOfDay && 
        //            auxAsistencia.PicadoReloj.TimeOfDay <= oHorario.FinPicadoEntrada.TimeOfDay )
        //        {
        //            auxDetalleHorario.miPicadoEntrada = auxAsistencia;
        //            if (auxAsistencia.PicadoReloj.TimeOfDay > oHorario.InicioPicadoEntrada.TimeOfDay)
        //            {
        //                TimeSpan auxIntervalo = oHorario.Entrada - auxAsistencia.PicadoReloj;
        //                TimeSpan tiempoTolerancia = oHorario.Tolerancia - oHorario.Entrada;
        //                if (auxIntervalo < tiempoTolerancia )
        //                {
        //                    auxDetalleHorario.minutosTarde = auxIntervalo.Minutes;
        //                }
        //                else
        //                {
        //                    auxDetalleHorario.minutosTarde = 0;
        //                    auxDetalleHorario.falta = 1;
        //                }

        //            }
        //        }
        //        //Verifica que esta dentro del rango de Inicio y Fin de Picado del horario de Salida
        //        if (auxAsistencia.PicadoReloj.TimeOfDay >= oHorario.InicioPicadoSalida.TimeOfDay &&
        //            auxAsistencia.PicadoReloj.TimeOfDay <= oHorario.InicioPicadoSalida.TimeOfDay)
        //        {
        //            auxDetalleHorario.miPicadoSalida = auxAsistencia;
        //            if (true)
        //            {

        //            }
        //        }
        //    }

        //    return auxDetalleHorario; 
        //}

        public List<Asistencia> LlenarAsistencia(Trabajador miTrabajador, DateTime miFecha)
        {
            DateTime x = miFecha.AddDays(-1);
            DateTime xx = miFecha.AddDays(1);
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Asistencia> consultaAsistencia = from d in bd.AsistenciaSet
                                                            where d.Trabajador.Id == miTrabajador.Id
                                                            && d.PicadoReloj > x
                                                            && d.PicadoReloj < xx
                                                            select d;
                return consultaAsistencia.ToList();
            }
        }
   }
}
