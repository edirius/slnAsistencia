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
    public class blAsistenciaMeses
    {
        private Microsoft.Office.Interop.Excel.Application oExcel;
        private object oMissing;
        private Microsoft.Office.Interop.Excel.Workbook oLibro;
        private Microsoft.Office.Interop.Excel.Worksheet oHoja;
        public string rutaarchivo = AppDomain.CurrentDomain.BaseDirectory + "R_AsistenciaMeses.xltx";

        int mAsi; int mPer; int mTar; int mFal;

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
            Dias_Mes(miAño, miMes);
            int contador = 0;
            int nro_filas = 0;
            int celda_inicio = 10;
            foreach (Trabajador item in miListaTrabajadores)
            {
                PeriodoTrabajador miPeriodoTrabajador = CargarPeriodoTrabajador(item);
                DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;

                nro_filas += 1;
                oHoja.Range["A7"].Formula = "INFORME DE ASISTENCIA DEL MES DE " + formatoFecha.GetMonthName(miMes).ToUpper() + " DE " + miAño;
                oHoja.Range["A" + (celda_inicio + contador).ToString()].Formula = nro_filas;
                oHoja.Range["B" + (celda_inicio + contador).ToString()].Formula = item.DNI.ToString();//DNI
                oHoja.Range["C" + (celda_inicio + contador).ToString()].Formula = item.ApellidoPaterno.ToString() + " " + item.ApellidoMaterno.ToString() + ", " + item.Nombre.ToString();//APELLIDSO Y NOMBRES

                string dia_celda = "";
                DateTime miFechaInicio = Convert.ToDateTime("01/" + miMes + "/" + miAño);
                mAsi = 0; mPer = 0; mTar = 0; mFal = 0;
                for (int dia = 0; dia < DateTime.DaysInMonth(miAño, miMes); dia++)
                {
                    DateTime auxiliar = miFechaInicio.AddDays(dia);

                    Horario miHorario = CargarHorario(miPeriodoTrabajador, auxiliar);
                    List<Asistencia> miAsistenciaTrabajador = LlenarAsistencia(item, auxiliar);
                    List<PermisosDias> miPermisoDiasTrabajador = LlenarPermisos(item, auxiliar);

                    string Control_Asistencia = CONTROL_ASISTENCIA(miHorario, miAsistenciaTrabajador, miPermisoDiasTrabajador);
                    if (dia + 1 == 1) { dia_celda = "G"; }
                    if (dia + 1 == 2) { dia_celda = "H"; }
                    if (dia + 1 == 3) { dia_celda = "I"; }
                    if (dia + 1 == 4) { dia_celda = "J"; }
                    if (dia + 1 == 5) { dia_celda = "K"; }
                    if (dia + 1 == 6) { dia_celda = "L"; }
                    if (dia + 1 == 7) { dia_celda = "M"; }
                    if (dia + 1 == 8) { dia_celda = "N"; }
                    if (dia + 1 == 9) { dia_celda = "O"; }
                    if (dia + 1 == 10) { dia_celda = "P"; }
                    if (dia + 1 == 11) { dia_celda = "Q"; }
                    if (dia + 1 == 12) { dia_celda = "R"; }
                    if (dia + 1 == 13) { dia_celda = "S"; }
                    if (dia + 1 == 14) { dia_celda = "T"; }
                    if (dia + 1 == 15) { dia_celda = "U"; }
                    if (dia + 1 == 16) { dia_celda = "V"; }
                    if (dia + 1 == 17) { dia_celda = "W"; }
                    if (dia + 1 == 18) { dia_celda = "X"; }
                    if (dia + 1 == 19) { dia_celda = "Y"; }
                    if (dia + 1 == 20) { dia_celda = "Z"; }
                    if (dia + 1 == 21) { dia_celda = "AA"; }
                    if (dia + 1 == 22) { dia_celda = "AB"; }
                    if (dia + 1 == 23) { dia_celda = "AC"; }
                    if (dia + 1 == 24) { dia_celda = "AD"; }
                    if (dia + 1 == 25) { dia_celda = "AE"; }
                    if (dia + 1 == 26) { dia_celda = "AF"; }
                    if (dia + 1 == 27) { dia_celda = "AG"; }
                    if (dia + 1 == 28) { dia_celda = "AH"; }
                    if (dia + 1 == 29) { dia_celda = "AI"; }
                    if (dia + 1 == 30) { dia_celda = "AJ"; }
                    if (dia + 1 == 31) { dia_celda = "AK"; }
                    oHoja.Range[dia_celda + (celda_inicio + contador).ToString()].Formula = Control_Asistencia;
                }
                oHoja.Range["AL" + (celda_inicio + contador).ToString()].Formula = mAsi;
                oHoja.Range["AM" + (celda_inicio + contador).ToString()].Formula = mPer;
                oHoja.Range["AN" + (celda_inicio + contador).ToString()].Formula = mTar;
                oHoja.Range["AO" + (celda_inicio + contador).ToString()].Formula = mFal;

                if (nro_filas < miListaTrabajadores.Count)
                {
                    contador += 1;
                    oHoja.Range[(celda_inicio + contador).ToString() + ":" + (celda_inicio + contador).ToString()].Insert();
                }
            }
        }

        private void Dias_Mes(int año, int mes)
        {
            try
            {
                int celda_inicio = 8;
                string dia_celda = "";
                int nro_dias = DateTime.DaysInMonth(año, mes);

                for (int dia = 0; dia < nro_dias; dia++)
                {
                    if (dia + 1 == 1) { dia_celda = "G"; }
                    if (dia + 1 == 2) { dia_celda = "H"; }
                    if (dia + 1 == 3) { dia_celda = "I"; }
                    if (dia + 1 == 4) { dia_celda = "J"; }
                    if (dia + 1 == 5) { dia_celda = "K"; }
                    if (dia + 1 == 6) { dia_celda = "L"; }
                    if (dia + 1 == 7) { dia_celda = "M"; }
                    if (dia + 1 == 8) { dia_celda = "N"; }
                    if (dia + 1 == 9) { dia_celda = "O"; }
                    if (dia + 1 == 10) { dia_celda = "P"; }
                    if (dia + 1 == 11) { dia_celda = "Q"; }
                    if (dia + 1 == 12) { dia_celda = "R"; }
                    if (dia + 1 == 13) { dia_celda = "S"; }
                    if (dia + 1 == 14) { dia_celda = "T"; }
                    if (dia + 1 == 15) { dia_celda = "U"; }
                    if (dia + 1 == 16) { dia_celda = "V"; }
                    if (dia + 1 == 17) { dia_celda = "W"; }
                    if (dia + 1 == 18) { dia_celda = "X"; }
                    if (dia + 1 == 19) { dia_celda = "Y"; }
                    if (dia + 1 == 20) { dia_celda = "Z"; }
                    if (dia + 1 == 21) { dia_celda = "AA"; }
                    if (dia + 1 == 22) { dia_celda = "AB"; }
                    if (dia + 1 == 23) { dia_celda = "AC"; }
                    if (dia + 1 == 24) { dia_celda = "AD"; }
                    if (dia + 1 == 25) { dia_celda = "AE"; }
                    if (dia + 1 == 26) { dia_celda = "AF"; }
                    if (dia + 1 == 27) { dia_celda = "AG"; }
                    if (dia + 1 == 28) { dia_celda = "AH"; }
                    if (dia + 1 == 29) { dia_celda = "AI"; }
                    if (dia + 1 == 30) { dia_celda = "AJ"; }
                    if (dia + 1 == 31) { dia_celda = "AK"; }

                    oHoja.Range[dia_celda + (celda_inicio).ToString()].Formula = Convert.ToString(Convert.ToDateTime((dia + 1).ToString() + "/" + mes + "/" + año).ToString("dddd")).Substring(0, 1).ToUpper();
                    oHoja.Range[dia_celda + (celda_inicio + 1).ToString()].Formula = dia + 1;
                }
            }
            catch (Exception e)
            {

            }
        }

        public string CONTROL_ASISTENCIA(Horario miHorario, List<Asistencia> miAsistenciaTrabajador, List<PermisosDias> miPermisoDiasTrabajador)
        {
            DateTime miH_Entrada = DateTime.Today;
            string sAsistencia = "";

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
                    if (miH_Entrada.TimeOfDay <= miHorario.Entrada.TimeOfDay)
                    {
                        sAsistencia = "A";
                        mAsi += 1;
                    }
                    else if (miH_Entrada.TimeOfDay >= miHorario.Entrada.TimeOfDay && miH_Entrada.TimeOfDay <= miHorario.Tolerancia.TimeOfDay)
                    {
                        sAsistencia = "T";
                        mTar += 1;
                    }
                    else
                    {
                        sAsistencia = "F";
                        mFal += 1;
                    }
                }
                else
                {
                    if (miPermisoDiasTrabajador.Count != 0)
                    {
                        sAsistencia = "P";
                        mPer += 1;
                    }
                    else
                    {
                        sAsistencia = "F";
                        mFal += 1;
                    }
                }
            }
            else
            {
                sAsistencia = "--";
            }
            return sAsistencia;
        }

        public int FALTA(Horario miHorario, string miH_Entrada, int miPermiso)
        {
            int Hora = 0;
            DateTime H_Entrada = Convert.ToDateTime(miH_Entrada);
            if ((H_Entrada.TimeOfDay.ToString() == "00:00:00" || H_Entrada.TimeOfDay > miHorario.Tolerancia.TimeOfDay) && (miPermiso == 0))
            {
                Hora = 1;
            }
            return Hora;
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
    }
}
