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
    public class blAsistenciaGeneral
    {
        private Microsoft.Office.Interop.Excel.Application oExcel;
        private object oMissing;
        private Microsoft.Office.Interop.Excel.Workbook oLibro;
        private Microsoft.Office.Interop.Excel.Worksheet oHoja;
        public string rutaarchivo = AppDomain.CurrentDomain.BaseDirectory + "R_AsistenciaGeneral.xltx";

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
        }

        public void ReporteAsistencia(List<Trabajador> miListaTrabajadores, DateTime miFechaInicio, DateTime miFechaFin)
        {
            Iniciar();
            int contador = 0;
            int nro_filas = 0;
            int celda_inicio = 10;
            foreach (Trabajador item in miListaTrabajadores)
            {
                PeriodoTrabajador miPeriodoTrabajador = CargarPeriodoTrabajador(item);

                nro_filas += 1;
                oHoja.Range["A6"].Formula = "INFORME DE ASISTENCIA GENERAL DE " + miFechaInicio.ToString("dd/MM/yyyy").Substring(0, 10) + " HASTA " + miFechaFin.ToString("dd/MM/yyyy").Substring(0, 10);

                int nro_fechas = 0;
                for (int i = 0; i < (miFechaFin - miFechaInicio).Days + 1; i++)
                {
                    nro_fechas += 1;
                    DateTime auxiliar = miFechaInicio.AddDays(i);
                    List<Asistencia> miAsistenciaTrabajador = CargarAsistencia(item, auxiliar);
                    Horario miHorario = CargarHorario(miPeriodoTrabajador, auxiliar);
                    PermisosDias miPermisoDiasTrabajador = CargarPermisos(item, auxiliar);
                    Vacaciones miVacaciones = CargarVacaciones(miPeriodoTrabajador, auxiliar);
                    DiaFestivo miDiaFestivo = CargarDiaFestivo(auxiliar);
                    int Vacaciones = 0; int DiaFestivo = 0; int Permiso = 0; 

                    string Dia = (QuitarAcento(auxiliar.ToString("dddd"))).ToUpper();
                    string HEntrada = ENTRADA(miHorario, miAsistenciaTrabajador);
                    string HSalida = SALIDA(miHorario, miAsistenciaTrabajador);
                    string RInicio = R_INICIO(miHorario, miAsistenciaTrabajador);
                    string RFin = R_FIN(miHorario, miAsistenciaTrabajador);
                    if (miVacaciones.Id != 0) { Vacaciones = 1; }
                    if (miDiaFestivo.Id != 0) { DiaFestivo = 1; }
                    if (miPermisoDiasTrabajador.Id != 0) { Permiso = 1; }
                    int Falta = FALTA(miHorario, HEntrada, Vacaciones, DiaFestivo, Permiso);
                    TimeSpan Tardanza = TARDANZA(miHorario, HEntrada);

                    oHoja.Range["A" + (celda_inicio + contador).ToString()].Formula = item.DNI.ToString();//DNI
                    oHoja.Range["B" + (celda_inicio + contador).ToString()].Formula = item.ApellidoPaterno.ToString() + " " + item.ApellidoMaterno.ToString() + ", " + item.Nombre.ToString();//APELLIDSO Y NOMBRES
                    oHoja.Range["C" + (celda_inicio + contador).ToString()].Formula = auxiliar.ToString("dd/MM/yyyy").Substring(0, 10);//FECHA
                    oHoja.Range["D" + (celda_inicio + contador).ToString()].Formula = Dia.Substring(0, 3);//DIA
                    oHoja.Range["E" + (celda_inicio + contador).ToString()].Formula = miHorario.Nombre;

                    if (Vacaciones >= 1)
                    {
                        oHoja.Range["F" + (celda_inicio + contador).ToString()].Formula = "VACACIONES";
                        oHoja.Range["F" + (celda_inicio + contador).ToString()].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        oHoja.Range["F" + (celda_inicio + contador).ToString()].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbYellow;
                    }
                    else if (miHorario.Id == 0)
                    {
                        oHoja.Range["F" + (celda_inicio + contador).ToString()].Formula = "DESCANSO";
                        oHoja.Range["F" + (celda_inicio + contador).ToString()].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    }
                    else if (DiaFestivo >= 1)
                    {
                        oHoja.Range["F" + (celda_inicio + contador).ToString()].Formula = "FERIADO " + miDiaFestivo.Nombre;
                        oHoja.Range["F" + (celda_inicio + contador).ToString()].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    }
                    else if (Permiso >= 1)
                    {
                        oHoja.Range["F" + (celda_inicio + contador).ToString()].Formula = "PERMISO POR " + miPermisoDiasTrabajador.TipoPermisos.Nombre;
                        oHoja.Range["F" + (celda_inicio + contador).ToString()].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        oHoja.Range["F" + (celda_inicio + contador).ToString()].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbYellow;
                    }
                    else if (Falta >= 1)
                    {
                        oHoja.Range["F" + (celda_inicio + contador).ToString()].Formula = "FALTA";
                        oHoja.Range["F" + (celda_inicio + contador).ToString()].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    }
                    else
                    {
                        //oHoja.Range["I" + (celda_inicio + contador).ToString()].Formula = miHorario.Entrada.TimeOfDay.ToString();
                        oHoja.Range["F" + (celda_inicio + contador).ToString()].Formula = HEntrada;
                        //oHoja.Range["K" + (celda_inicio + contador).ToString()].Formula = miHorario.InicioPicadoRefrigerio.TimeOfDay.ToString();
                        oHoja.Range["G" + (celda_inicio + contador).ToString()].Formula = RInicio;
                        //oHoja.Range["M" + (celda_inicio + contador).ToString()].Formula = miHorario.FinPicadoRefrigerio.TimeOfDay.ToString();
                        oHoja.Range["H" + (celda_inicio + contador).ToString()].Formula = RFin;
                        //oHoja.Range["O" + (celda_inicio + contador).ToString()].Formula = miHorario.Salida.TimeOfDay.ToString();
                        oHoja.Range["I" + (celda_inicio + contador).ToString()].Formula = HSalida;
                    }

                    oHoja.Range["J" + (celda_inicio + contador).ToString()].Formula = Tardanza.Minutes.ToString();
                    oHoja.Range["K" + (celda_inicio + contador).ToString()].Formula = Permiso.ToString();
                    oHoja.Range["L" + (celda_inicio + contador).ToString()].Formula = Falta.ToString();
                    if (Tardanza.ToString() != "00:00:00")
                    {
                        oHoja.Range["J" + (celda_inicio + contador).ToString()].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbOrange;
                    }
                    if (Permiso >= 1)
                    {
                        oHoja.Range["k" + (celda_inicio + contador).ToString()].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbGreen;
                    }
                    if (Falta >= 1)
                    {
                        oHoja.Range["L" + (celda_inicio + contador).ToString()].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbRed;
                    }

                    if (nro_fechas <= (miFechaFin - miFechaInicio).Days)
                    {
                        contador += 1;
                        oHoja.Range[(celda_inicio + contador).ToString() + ":" + (celda_inicio + contador).ToString()].Insert();
                        oHoja.Range["A" + (celda_inicio + contador).ToString(), "L" + (celda_inicio + contador).ToString()].Interior.ColorIndex = 0;
                        oHoja.Range["C" + (celda_inicio + contador).ToString(), "L" + (celda_inicio + contador).ToString()].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    }
                }

                if (nro_filas < miListaTrabajadores.Count)
                {
                    contador += 1;
                    oHoja.Range[(celda_inicio + contador).ToString() + ":" + (celda_inicio + contador).ToString()].Insert();
                    //oHoja.Range["F" + (celda_inicio + contador).ToString(), "S" + (celda_inicio + contador).ToString()].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    oHoja.Range["A" + (celda_inicio + contador).ToString(), "L" + (celda_inicio + contador).ToString()].Interior.ColorIndex = 0;
                    oHoja.Range["C" + (celda_inicio + contador).ToString(), "L" + (celda_inicio + contador).ToString()].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                }
            }
        }

        public string ENTRADA(Horario miHorario, List<Asistencia> miAsistenciaTrabajador)
        {
            DateTime Hora = DateTime.Today;
            foreach (Asistencia item in miAsistenciaTrabajador)
            {
                if (item.PicadoReloj.TimeOfDay >= miHorario.InicioPicadoEntrada.TimeOfDay && item.PicadoReloj.TimeOfDay <= miHorario.FinPicadoEntrada.TimeOfDay)
                {
                    if (Hora.TimeOfDay.ToString() == "00:00:00")
                    {
                        Hora = item.PicadoReloj;
                    }
                    else if (Hora.TimeOfDay >= item.PicadoReloj.TimeOfDay)
                    {
                        Hora = item.PicadoReloj;
                    }
                }
            }
            return Hora.TimeOfDay.ToString();
        }

        public string SALIDA(Horario miHorario, List<Asistencia> miAsistenciaTrabajador)
        {
            DateTime Hora = DateTime.Today;
            foreach (Asistencia item in miAsistenciaTrabajador)
            {
                if (item.PicadoReloj.TimeOfDay >= miHorario.InicioPicadoSalida.TimeOfDay && item.PicadoReloj.TimeOfDay <= miHorario.FinPicadoSalida.TimeOfDay)
                {
                    if (Hora.TimeOfDay.ToString() == "00:00:00")
                    {
                        Hora = item.PicadoReloj;
                    }
                    else if (Hora.TimeOfDay >= item.PicadoReloj.TimeOfDay)
                    {
                        Hora = item.PicadoReloj;
                    }
                }
            }
            return Hora.TimeOfDay.ToString();
        }

        public string R_INICIO(Horario miHorario, List<Asistencia> miAsistenciaTrabajador)
        {
            DateTime Hora = DateTime.Today;
            DateTime Inicio_Picado = DateTime.Today;
            DateTime Fin_Picado = DateTime.Today;
            if (miHorario.Id != 0)
            {
                Inicio_Picado = miHorario.InicioPicadoRefrigerio.AddMinutes(-30);
                Fin_Picado = miHorario.InicioPicadoRefrigerio.AddMinutes(30);
            }

            foreach (Asistencia item in miAsistenciaTrabajador)
            {
                if (item.PicadoReloj.TimeOfDay >= Inicio_Picado.TimeOfDay && item.PicadoReloj.TimeOfDay <= Fin_Picado.TimeOfDay)
                {
                    if (Hora.TimeOfDay.ToString() == "00:00:00")
                    {
                        Hora = item.PicadoReloj;
                    }
                    else if (Hora.TimeOfDay >= item.PicadoReloj.TimeOfDay)
                    {
                        Hora = item.PicadoReloj;
                    }
                }
            }
            return Hora.TimeOfDay.ToString();
        }

        public string R_FIN(Horario miHorario, List<Asistencia> miAsistenciaTrabajador)
        {
            DateTime Hora = DateTime.Today;
            DateTime Inicio_Picado = DateTime.Today;
            DateTime Fin_Picado = DateTime.Today;
            if (miHorario.Id != 0)
            {
                Inicio_Picado = miHorario.FinPicadoRefrigerio.AddMinutes(-30);
                Fin_Picado = miHorario.FinPicadoRefrigerio.AddMinutes(30);
            }

            foreach (Asistencia item in miAsistenciaTrabajador)
            {
                if (item.PicadoReloj.TimeOfDay >= Inicio_Picado.TimeOfDay && item.PicadoReloj.TimeOfDay <= Fin_Picado.TimeOfDay)
                {
                    if (Hora.TimeOfDay.ToString() == "00:00:00")
                    {
                        Hora = item.PicadoReloj;
                    }
                    else if (Hora.TimeOfDay >= item.PicadoReloj.TimeOfDay)
                    {
                        Hora = item.PicadoReloj;
                    }
                }
            }
            return Hora.TimeOfDay.ToString();
        }

        public int FALTA(Horario miHorario, string miH_Entrada, int miVacaciones, int miDiaFestivo, int miPermiso)
        {
            int Hora = 0;
            DateTime H_Entrada = Convert.ToDateTime(miH_Entrada);
            if (miHorario.Id != 0)
            {
                if ((H_Entrada.TimeOfDay.ToString() == "00:00:00" || H_Entrada.TimeOfDay > miHorario.Tolerancia.TimeOfDay) && (miVacaciones == 0) && (miDiaFestivo == 0) && (miPermiso == 0))
                {
                    Hora = 1;
                }
            }
            return Hora;
        }

        public TimeSpan TARDANZA(Horario miHorario, string miH_Entrada)
        {
            TimeSpan Hora = new TimeSpan(00, 00, 00);
            DateTime H_Entrada = Convert.ToDateTime(miH_Entrada);
            if (H_Entrada.TimeOfDay >= miHorario.Entrada.TimeOfDay && H_Entrada.TimeOfDay <= miHorario.Tolerancia.TimeOfDay)
            {
                Hora = H_Entrada.TimeOfDay - miHorario.Entrada.TimeOfDay;
            }
            return Hora;
        }

        public string QuitarAcento(string miTexto)
        {
            string normalizedString = miTexto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            for (int i = 0; i < normalizedString.Length; i++)
            {
                var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(normalizedString[i]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public DiaFestivo CargarDiaFestivo(DateTime miFecha)
        {
            DiaFestivo miDiaFestivo = new DiaFestivo();
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<DiaFestivo> consultaDiaFestivo = from d in bd.DiaFestivoSet
                                                            where d.Dia.Year == miFecha.Year
                                                            && d.Dia.Month == miFecha.Month
                                                            && d.Dia.Day == miFecha.Day
                                                            select d;
                foreach (DiaFestivo item in consultaDiaFestivo.OrderBy(x => x.Id))
                {
                    miDiaFestivo = item;
                }
                return miDiaFestivo;
            }
        }

        public Vacaciones CargarVacaciones(PeriodoTrabajador miPeriodoTrabajador, DateTime miFecha)
        {
            Vacaciones miVacaciones = new Vacaciones();
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Vacaciones> consultaVacaciones = from d in bd.VacacionesSet
                                                            where d.AsistenciaPeriodoLaborado.PeriodoTrabajador.Id == miPeriodoTrabajador.Id
                                                            && miFecha >= d.Inicio
                                                            && miFecha <= d.Fin
                                                            select d;
                foreach (Vacaciones item in consultaVacaciones)
                {
                    miVacaciones = item;
                }
                return miVacaciones;
            }
        }

        public PermisosDias CargarPermisos(Trabajador miTrabajador, DateTime miFecha)
        {
            PermisosDias miPermisosDias = new PermisosDias();
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PermisosDias> consultaPermisos = from d in bd.PermisosDiasSet.Include("TipoPermisos")
                                                            where d.PeriodoTrabajador.Trabajador.Id == miTrabajador.Id
                                                            && miFecha >= d.Inicio
                                                            && miFecha <= d.Fin
                                                            select d;
                foreach (PermisosDias item in consultaPermisos)
                {
                    miPermisosDias = item;
                }
                return miPermisosDias;
            }
        }

        public Horario CargarHorario(PeriodoTrabajador miPeriodoTrabajador, DateTime miFecha)
        {
            Horario miHorario = new Horario();
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Dia> consultaHorarioDia = from d in bd.DiaSet.Include("Horario")
                                                     where d.HorarioSemana.Id == miPeriodoTrabajador.HorarioSemana.Id
                                                     select d;
                foreach (Dia item in consultaHorarioDia)
                {
                    if ((QuitarAcento(miFecha.ToString("dddd"))).ToUpper() == item.NombreDiaSemana.ToUpper() && item.Horario != null)
                    {
                        miHorario = item.Horario;
                    }
                }
                return miHorario;
            }
        }

        public List<Asistencia> CargarAsistencia(Trabajador miTrabajador, DateTime miFecha)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Asistencia> consultaAsistencia = from d in bd.AsistenciaSet
                                                            where d.Trabajador.Id == miTrabajador.Id
                                                            && d.PicadoReloj.Year == miFecha.Year
                                                            && d.PicadoReloj.Month == miFecha.Month
                                                            && d.PicadoReloj.Day == miFecha.Day
                                                            select d;
                return consultaAsistencia.ToList();
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
