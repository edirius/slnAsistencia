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
    public class blAcuFaltasMeses
    {
        private Microsoft.Office.Interop.Excel.Application oExcel;
        private object oMissing;
        private Microsoft.Office.Interop.Excel.Workbook oLibro;
        private Microsoft.Office.Interop.Excel.Worksheet oHoja;
        public string rutaarchivo = AppDomain.CurrentDomain.BaseDirectory + "R_AcuFaltasMeses.xltx";

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

        public void Imprimir_Reporte(System.Data.DataTable oDataTrabajadores, int miAño, int miMes)
        {
            Iniciar();
            int contador = 0;
            int nro_filas = 0;
            int celda_inicio = 10;

            DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
            oHoja.Range["A7"].Formula = "FALTAS DEL MES DE " + formatoFecha.GetMonthName(miMes).ToUpper() + " DE " + miAño;
            foreach (System.Data.DataRow item in oDataTrabajadores.Select("", "FALTAS DESC, TRABAJADOR ASC"))
            {
                nro_filas += 1;
                oHoja.Range["A" + (celda_inicio + contador).ToString()].Formula = nro_filas;
                oHoja.Range["B" + (celda_inicio + contador).ToString()].Formula = item[0];//DNI
                oHoja.Range["C" + (celda_inicio + contador).ToString()].Formula = item[1];//APELLIDSO Y NOMBRES
                oHoja.Range["D" + (celda_inicio + contador).ToString()].Formula = item[2];

                if (nro_filas < oDataTrabajadores.Rows.Count)
                {
                    contador += 1;
                    oHoja.Range[(celda_inicio + contador).ToString() + ":" + (celda_inicio + contador).ToString()].Insert();
                }
            }
        }

        public void Asistencia_Meses(List<Trabajador> miListaTrabajadores, int miAño, int miMes)
        {
            int mTot;
            System.Data.DataTable oDataTrabajadores = new System.Data.DataTable();
            oDataTrabajadores = new System.Data.DataTable();
            oDataTrabajadores.Columns.Add("DNI");
            oDataTrabajadores.Columns.Add("TRABAJADOR");
            oDataTrabajadores.Columns.Add("FALTAS", typeof(int));

            foreach (Trabajador item in miListaTrabajadores)
            {
                PeriodoTrabajador miPeriodoTrabajador = CargarPeriodoTrabajador(item);
                DateTime miFechaInicio = Convert.ToDateTime("01/" + miMes + "/" + miAño);
                mTot = 0;
                for (int dia = 0; dia < DateTime.DaysInMonth(miAño, miMes); dia++)
                {
                    int Vacaciones = 0; int DiaFestivo = 0; int Permiso = 0;

                    DateTime auxiliar = miFechaInicio.AddDays(dia);
                    List<Asistencia> miAsistenciaTrabajador = CargarAsistencia(item, auxiliar);
                    Horario miHorario = CargarHorario(miPeriodoTrabajador, auxiliar);
                    PermisosDias miPermisoDiasTrabajador = CargarPermisos(item, auxiliar);
                    Vacaciones miVacaciones = CargarVacaciones(miPeriodoTrabajador, auxiliar);
                    DiaFestivo miDiaFestivo = CargarDiaFestivo(auxiliar);
                    if (miVacaciones.Id != 0) { Vacaciones = 1; }
                    if (miDiaFestivo.Id != 0) { DiaFestivo = 1; }
                    if (miPermisoDiasTrabajador.Id != 0) { Permiso = 1; }
                    mTot += CONTROL_FALTAS(miHorario, miAsistenciaTrabajador, Vacaciones, DiaFestivo, Permiso);
                }

                var row = oDataTrabajadores.NewRow();
                row["DNI"] = item.DNI.ToString();//DNI;
                row["TRABAJADOR"] = item.ApellidoPaterno.ToString() + " " + item.ApellidoMaterno.ToString() + ", " + item.Nombre.ToString();//APELLIDSO Y NOMBRES
                row["FALTAS"] = mTot;
                oDataTrabajadores.Rows.Add(row);
            }
            Imprimir_Reporte(oDataTrabajadores, miAño, miMes);
        }

        public int CONTROL_FALTAS(Horario miHorario, List<Asistencia> miAsistenciaTrabajador, int miVacaciones, int miDiaFestivo, int miPermiso)
        {
            DateTime miH_Entrada = DateTime.Today;
            int Faltas = 0;

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
                if ((miH_Entrada.TimeOfDay.ToString() == "00:00:00" || miH_Entrada.TimeOfDay > miHorario.Tolerancia.TimeOfDay) && (miVacaciones == 0) && (miDiaFestivo == 0) && (miPermiso == 0))
                {
                    Faltas = 1;
                }
            }
            return Faltas;
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
