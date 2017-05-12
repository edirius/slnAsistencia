using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;
using miExcel = Microsoft.Office.Interop.Excel;

namespace CapaDeNegocios.cblReportes
{
    public class blReporteAsistencia
    {
        private Microsoft.Office.Interop.Excel.Application oExcel;
        private object oMissing;
        private Microsoft.Office.Interop.Excel.Workbook oLibro;
        private Microsoft.Office.Interop.Excel.Worksheet oHoja;
        public string rutaarchivo = AppDomain.CurrentDomain.BaseDirectory + "Asistencia.xltx";

        public List<Trabajador> miListaTrabajadores;
        public DateTime miFechaInicio;
        public DateTime miFechaFin;

        public void Iniciar()
        {
            //if (File.Exists(@rutaarchivo))
            //{
                oExcel = new Microsoft.Office.Interop.Excel.Application(); ;
                oMissing = System.Reflection.Missing.Value;
                oLibro = oExcel.Workbooks.Add(@rutaarchivo);
                oHoja = (Microsoft.Office.Interop.Excel.Worksheet)oExcel.ActiveSheet;
                oExcel.Visible = true;
            //}
            //else
            //{
            //    throw new Exception("La plantilla Tareo.xltx no se encuentra en la ruta");
            //}
            ReporteAsistencia();
        }

        public void ReporteAsistencia()
        {
            int contador = 0;
            int nro_filas = 0;
            foreach (Trabajador item in miListaTrabajadores)
            {
                PeriodoTrabajador miPeridodTrabajador = CargarPeriodoTrabajador(item);
                //List<Asistencia> miAsistenciaTrabajador = LlenarAsistencia(item, miFechaInicio, miFechaFin);
                //List<PermisosDias> miPermisoDiasTrabajador = LlenarPermisos(item, miFechaInicio, miFechaFin);

                nro_filas += 1;
                oHoja.Range["A" + (6 + contador).ToString()].Formula = nro_filas;
                oHoja.Range["B" + (6 + contador).ToString()].Formula = item.ApellidoPaterno.ToString() + " " + item.ApellidoMaterno.ToString() + ", " + item.Nombre.ToString();//APELLIDSO Y NOMBRES
                oHoja.Range["E" + (6 + contador).ToString()].Formula = item.DNI.ToString();//DNI

                int nro_fechas = 0;
                for (int i = 0; i <= (miFechaFin - miFechaInicio).Days; i++)
                {
                    nro_fechas += 1;
                    DateTime auxiliar = miFechaInicio.AddDays(i);
                    oHoja.Range["G" + (6 + contador).ToString()].Formula = auxiliar.Date.ToString();//SEXO

                    List<Horario> miListaHorario = CargarListaHorario(miPeridodTrabajador, auxiliar);
                    List<Asistencia> miAsistenciaTrabajador = LlenarAsistencia(item, auxiliar);
                    int nro_horario = 0;
                    foreach (Horario item2 in miListaHorario.OrderBy(x => x.Id))
                    {
                        nro_horario += 1;
                        oHoja.Range["H" + (6 + contador).ToString()].Formula = item2.Nombre;
                        oHoja.Range["I" + (6 + contador).ToString()].Formula = item2.Entrada;
                        oHoja.Range["K" + (6 + contador).ToString()].Formula = item2.Salida;

                        foreach (Asistencia item3 in miAsistenciaTrabajador.OrderBy(x => x.Id))
                        {
                            oHoja.Range["J" + (6 + contador).ToString()].Formula = item3.PicadoReloj;
                        }

                        if (nro_horario < miListaHorario.Count)
                        {
                            contador += 1;
                            oHoja.Range[(6 + contador).ToString() + ":" + (6 + contador).ToString()].Insert();
                        }
                    }

                    if (nro_fechas < (miFechaFin - miFechaInicio).Days)
                    {
                        contador += 1;
                        oHoja.Range[(6 + contador).ToString() + ":" + (6 + contador).ToString()].Insert();
                    }
                }

                if (nro_filas < miListaTrabajadores.Count)
                {
                    contador += 1;
                    oHoja.Range[(6 + contador).ToString() + ":" + (6 + contador).ToString()].Insert();
                }
            }
        }

        public List<Asistencia> LlenarAsistencia(Trabajador miTrabajador, DateTime miFecha)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Asistencia> consultaAsistencia = from d in bd.AsistenciaSet
                                                            where d.Trabajador.Id == miTrabajador.Id
                                                            && d.PicadoReloj == miFecha
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

        public List<Horario> CargarListaHorario(PeriodoTrabajador miPeriodoTrabajador, DateTime miFecha)
        {
            List<Horario> miHorario = new List<Horario>();
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Dia> consultaHorarioDia = from d in bd.DiaSet.Include("HorarioDia.Horario")
                                                     where d.HorarioSemana.Id == miPeriodoTrabajador.HorarioSemana.Id
                                                     select d;
                foreach (Dia item in consultaHorarioDia)
                {
                    if ((miFecha.ToString("dddd")).ToUpper() == item.NombreDiaSemana.ToUpper() && item.HorarioDia != null)
                    {
                        miHorario = item.HorarioDia.Horario.ToList();
                    }
                }
                return miHorario;
            }
        }

        public PeriodoTrabajador CargarPeriodoTrabajador(Trabajador miTrabajador)
        {
            PeriodoTrabajador miPeriodoTrabajador = new PeriodoTrabajador();
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PeriodoTrabajador> consultaPeriodoTrabajador = from d in bd.PeriodoTrabajadorSet.Include("HorarioSemana")
                                                                          where d.Trabajador.Id == miTrabajador.Id
                                                                          select d;
                foreach (PeriodoTrabajador item in consultaPeriodoTrabajador.OrderBy(x => x.Id))
                {
                    miPeriodoTrabajador = item;
                }
                return miPeriodoTrabajador;
            }
        }
    }
}
