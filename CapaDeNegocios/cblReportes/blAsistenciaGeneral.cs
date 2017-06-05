﻿using System;
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
            foreach (Trabajador item in miListaTrabajadores)
            {
                PeriodoTrabajador miPeridodTrabajador = CargarPeriodoTrabajador(item);

                nro_filas += 1;
                oHoja.Range["A" + (6 + contador).ToString()].Formula = nro_filas;
                oHoja.Range["B" + (6 + contador).ToString()].Formula = item.ApellidoPaterno.ToString() + " " + item.ApellidoMaterno.ToString() + ", " + item.Nombre.ToString();//APELLIDSO Y NOMBRES
                oHoja.Range["E" + (6 + contador).ToString()].Formula = item.DNI.ToString();//DNI

                int nro_fechas = 0;
                for (int i = 0; i < (miFechaFin - miFechaInicio).Days; i++)
                {
                    nro_fechas += 1;
                    DateTime auxiliar = miFechaInicio.AddDays(i);
                    oHoja.Range["G" + (6 + contador).ToString()].Formula = auxiliar.Date.ToString();//SEXO

                    List<Horario> miListaHorario = CargarListaHorario(miPeridodTrabajador, auxiliar);
                    List<Asistencia> miAsistenciaTrabajador = LlenarAsistencia(item, auxiliar);
                    List<PermisosDias> miPermisoDiasTrabajador = LlenarPermisos(item, auxiliar);
                    int nro_horario = 0;
                    foreach (Horario item2 in miListaHorario.OrderBy(x => x.Id))
                    {
                        nro_horario += 1;
                        string H_Entrada = ENTRADA(item2, miAsistenciaTrabajador);
                        string H_Salida = SALIDA(item2, miAsistenciaTrabajador);
                        TimeSpan Tardanza = TARDANZA(item2, H_Entrada);
                        int Permiso = miPermisoDiasTrabajador.Count;
                        int Falta = FALTA(item2, H_Entrada, Permiso);
                        oHoja.Range["H" + (6 + contador).ToString()].Formula = item2.Nombre;
                        oHoja.Range["I" + (6 + contador).ToString()].Formula = item2.Entrada.TimeOfDay.ToString();
                        oHoja.Range["J" + (6 + contador).ToString()].Formula = H_Entrada;
                        oHoja.Range["K" + (6 + contador).ToString()].Formula = item2.Salida.TimeOfDay.ToString();
                        oHoja.Range["L" + (6 + contador).ToString()].Formula = H_Salida;
                        oHoja.Range["M" + (6 + contador).ToString()].Formula = Tardanza.ToString();
                        oHoja.Range["N" + (6 + contador).ToString()].Formula = Permiso.ToString();
                        oHoja.Range["O" + (6 + contador).ToString()].Formula = Falta.ToString();
                        //if (Falta == 1)
                        //{
                        //    oHoja.Range["O" + (6 + contador).ToString()].Interior.ColorIndex = 3;
                        //}

                        if (nro_horario < miListaHorario.Count)
                        {
                            contador += 1;
                            oHoja.Range[(6 + contador).ToString() + ":" + (6 + contador).ToString()].Insert();
                        }
                    }

                    if (nro_fechas <= (miFechaFin - miFechaInicio).Days)
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
                    if ((QuitarAcento(miFecha.ToString("dddd"))).ToUpper() == item.NombreDiaSemana.ToUpper() && item.HorarioDia != null)
                    {
                        miHorario = item.HorarioDia.Horario.ToList();
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
                                                            && d.Inicio >= miFecha
                                                            && d.Inicio <= miFecha
                                                            select d;
                return consultaPermisos.ToList();
            }
        }
    }
}