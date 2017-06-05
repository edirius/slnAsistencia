using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using miExcel = Microsoft.Office.Interop.Excel;
using CapaEntities;

namespace CapaDeNegocios.cblReportesAsistencia
{
    public class blExportarExcelReporteAsistencia
    {
        private Microsoft.Office.Interop.Excel.Application oExcel;
        private object oMissing;
        private Microsoft.Office.Interop.Excel.Workbook oLibro;
        private Microsoft.Office.Interop.Excel.Worksheet oHoja;
        public string rutaarchivo;

        public blExportarExcelReporteAsistencia(string mirutaarchivo)
        {
            oExcel = new Microsoft.Office.Interop.Excel.Application(); ;
            oMissing = System.Reflection.Missing.Value;
            oLibro = oExcel.Workbooks.Add(oMissing);
            oHoja = (Microsoft.Office.Interop.Excel.Worksheet)oExcel.ActiveSheet;
            rutaarchivo = mirutaarchivo;
            oExcel.Visible = true;
        }

        public void ImprimirReporteAsistencia (cReporteAsistencia miReporteAsistencia)
        {
            string[] letras = { "A", "B", "C", "D", "E", "F", "G","H", "I", "J", "K", "L", "M", "N", "O", "P" };
            int fila= 1;
            int columna= 0;

            oHoja.Range[letras[columna] + fila.ToString()].Value = miReporteAsistencia.titulo;
            fila += 1;

            oHoja.Range[letras[columna] + fila.ToString()].Value = "Reporte del " + miReporteAsistencia.FechaInicio.Date.ToString("dd/MM/yyyy") + " al " + miReporteAsistencia.FechaFin.Date.ToString("dd/MM/yyyy");
            fila += 1;
            foreach (cDetalleReporteAsistenciaXTrabajador item in miReporteAsistencia.detallesReporteAsistenciaXTrabajador)
            {

                oHoja.Range[letras[columna] + fila.ToString()].Value = item.miTrabajador.Nombre + " " + item.miTrabajador.ApellidoPaterno + " " + item.miTrabajador.ApellidoMaterno;
                columna += 1;
                oHoja.Range[letras[columna] + fila.ToString()].Value = item.miTrabajador.DNI;

                foreach (cDetalleAsistenciaXDia auxDetalleAsistenciaXDia in item.detallesAsistenciasXDia)
                {
                    fila += 1;
                    columna = 2;
                    oHoja.Range[letras[columna] + fila.ToString()].Value = auxDetalleAsistenciaXDia.Dia.Date;
                    columna += 1;
                    //Verificando que tiene horario 
                    if (auxDetalleAsistenciaXDia.ListaHorario == null)
                    {
                        //No tiene Horario
                        oHoja.Range[letras[columna] + fila.ToString()].Value = "Dia Libre";
                        
                    }
                    else
                    {
                        //Verificando que tiene asistencias
                        
                        if (auxDetalleAsistenciaXDia.ListaAsistencia == null)
                        {
                            //Tiene horario pero no tiene permisos
                            if (auxDetalleAsistenciaXDia.ListaPermisos == null)
                            {
                                //No tiene Permisos 
                                //Verificando si es dia Festivo
                                if (auxDetalleAsistenciaXDia.ListaDiaFestivo == null)
                                {
                                    //No tiene Dias Festivos
                                    //El trabajador se faltó
                                    oHoja.Range[letras[columna] + fila.ToString()].Value = "Falta";
                                    columna += 1;
                                    oHoja.Range[letras[columna] + fila.ToString()].Value = "Falta";
                                    columna += 1;

                                    oHoja.Range[letras[columna] + fila.ToString()].Value = "Falta";
                                    columna += 1;
                                    oHoja.Range[letras[columna] + fila.ToString()].Value = "Falta";
                                    columna += 1;
                                }
                                else
                                {
                                    //Si existe un dia Festivo
                                    oHoja.Range[letras[columna] + fila.ToString()].Value = "Dia festivo";
                                    columna += 1;
                                }
                            }
                            else
                            {
                                //Tiene Permisos
                                oHoja.Range[letras[columna] + fila.ToString()].Value = "Permiso";
                                columna += 1;
                            }
                            
                        }
                        else
                        {
                            //Tiene Asistencias
                            foreach (Asistencia auxAsistencia in auxDetalleAsistenciaXDia.ListaAsistencia)
                            {
                                foreach (Horario auxHorario in auxDetalleAsistenciaXDia.ListaHorario)
                                {

                                    //Verificando que esta dentro del rango del inicio y fin del picado
                                    if (auxAsistencia.PicadoReloj.TimeOfDay  >= auxHorario.InicioPicadoEntrada.TimeOfDay &&
                                        auxAsistencia.PicadoReloj.TimeOfDay  <= auxHorario.FinPicadoEntrada.TimeOfDay )
                                    {
                                        oHoja.Range[letras[columna] + fila.ToString()].Value = auxHorario.Entrada;
                                        columna += 1;
                                        oHoja.Range[letras[columna] + fila.ToString()].Value = auxAsistencia.PicadoReloj;
                                        columna += 1;
                                    }
                                    else
                                    {
                                        if (auxAsistencia.PicadoReloj.TimeOfDay >= auxHorario.InicioPicadoEntrada.TimeOfDay  &&
                                        auxAsistencia.PicadoReloj.TimeOfDay  <= auxHorario.FinPicadoEntrada.TimeOfDay )
                                        {
                                            oHoja.Range[letras[columna] + fila.ToString()].Value = auxHorario.Salida;
                                            columna += 1;
                                            oHoja.Range[letras[columna] + fila.ToString()].Value = auxAsistencia.PicadoReloj;
                                            columna += 1;
                                        }
                                    }

                                }
                            }

                        }
                    }    

                        
                       
                    }

                   
                    
                 
                    
            }
        }
    }
}
