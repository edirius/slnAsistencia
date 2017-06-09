using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CapaEntities;
using CapaDeDatos;
using miExcel = Microsoft.Office.Interop.Excel;

namespace CapaDeNegocios.cblReportes
{
    public class blAcuTardanzasMeses
    {
        private Microsoft.Office.Interop.Excel.Application oExcel;
        private object oMissing;
        private Microsoft.Office.Interop.Excel.Workbook oLibro;
        private Microsoft.Office.Interop.Excel.Worksheet oHoja;
        public string rutaarchivo = AppDomain.CurrentDomain.BaseDirectory + "R_AcuTardanzasMeses.xltx";

        TimeSpan mAcu = new TimeSpan(00, 00, 00); int mTot;

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

        public void Asistencia_Meses(List<Trabajador> miListaTrabajadores, int miAño, int miMes)
        {
            Iniciar();
            int contador = 0;
            int nro_filas = 0;
            int celda_inicio = 10;
            foreach (Trabajador item in miListaTrabajadores)
            {
                PeriodoTrabajador miPeriodoTrabajador = CargarPeriodoTrabajador(item);
                DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;

                nro_filas += 1;
                oHoja.Range["A7"].Formula = "INFORME DE TARDANZAS DEL MES DE " + formatoFecha.GetMonthName(miMes).ToUpper() + " DE " + miAño;
                oHoja.Range["A" + (celda_inicio + contador).ToString()].Formula = nro_filas;
                oHoja.Range["B" + (celda_inicio + contador).ToString()].Formula = item.DNI.ToString();//DNI
                oHoja.Range["C" + (celda_inicio + contador).ToString()].Formula = item.ApellidoPaterno.ToString() + " " + item.ApellidoMaterno.ToString() + ", " + item.Nombre.ToString();//APELLIDSO Y NOMBRES

                DateTime miFechaInicio = Convert.ToDateTime("01/" + miMes + "/" + miAño);
                mAcu = new TimeSpan(00, 00, 00);
                mTot = 0;
                for (int dia = 0; dia < DateTime.DaysInMonth(miAño, miMes); dia++)
                {
                    DateTime auxiliar = miFechaInicio.AddDays(dia);

                    Horario miHorario = CargarHorario(miPeriodoTrabajador, auxiliar);
                    List<Asistencia> miAsistenciaTrabajador = LlenarAsistencia(item, auxiliar);
                    List<PermisosDias> miPermisoDiasTrabajador = LlenarPermisos(item, auxiliar);
                    mAcu += CONTROL_TARDANZA(miHorario, miAsistenciaTrabajador, miPermisoDiasTrabajador);
                }
                oHoja.Range["G" + (celda_inicio + contador).ToString()].Formula = mAcu.Minutes.ToString();
                oHoja.Range["H" + (celda_inicio + contador).ToString()].Formula = 30;
                if (mAcu.Minutes >= 30) { mTot = mAcu.Minutes - 30; }
                else { mTot = 0; }
                oHoja.Range["I" + (celda_inicio + contador).ToString()].Formula = mTot;

                if (nro_filas < miListaTrabajadores.Count)
                {
                    contador += 1;
                    oHoja.Range[(celda_inicio + contador).ToString() + ":" + (celda_inicio + contador).ToString()].Insert();
                }
            }
        }

        public TimeSpan CONTROL_TARDANZA(Horario miHorario, List<Asistencia> miAsistenciaTrabajador, List<PermisosDias> miPermisoDiasTrabajador)
        {
            DateTime miH_Entrada = DateTime.Today;
            TimeSpan Tardanza = new TimeSpan(00, 00, 00);

            foreach (Asistencia item in miAsistenciaTrabajador)
            {
                if (item.PicadoReloj.TimeOfDay >= miHorario.InicioPicadoEntrada.TimeOfDay && item.PicadoReloj.TimeOfDay <= miHorario.FinPicadoEntrada.TimeOfDay)
                {
                    if (miH_Entrada.TimeOfDay.ToString() == "00:00:00")
                    {
                        miH_Entrada = item.PicadoReloj;
                    }
                    else if (miH_Entrada.TimeOfDay >= item.PicadoReloj.TimeOfDay)
                    {
                        miH_Entrada = item.PicadoReloj;
                    }
                }
            }

            if (miHorario.Id != 0)
            {
                if (miH_Entrada.TimeOfDay.ToString() != "00:00:00")
                {
                    if (miH_Entrada.TimeOfDay >= miHorario.Entrada.TimeOfDay && miH_Entrada.TimeOfDay <= miHorario.Tolerancia.TimeOfDay)
                    {
                        Tardanza = miH_Entrada.TimeOfDay - miHorario.Entrada.TimeOfDay;
                    }
                }
            }
            return Tardanza;
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

        public List<Asistencia> LlenarAsistencia(Trabajador miTrabajador, DateTime miFecha)
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

        public List<PermisosDias> LlenarPermisos(Trabajador miTrabajador, DateTime miFecha)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PermisosDias> consultaPermisos = from d in bd.PermisosDiasSet
                                                            where d.PeriodoTrabajador.Trabajador.Id == miTrabajador.Id
                                                            && miFecha >= d.Inicio
                                                            && miFecha <= d.Fin
                                                            select d;
                return consultaPermisos.ToList();
            }
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
    }
}
