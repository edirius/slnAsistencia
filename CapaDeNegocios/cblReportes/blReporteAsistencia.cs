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
            foreach (Trabajador item in miListaTrabajadores)
            {
                //List<Asistencia> miAsistenciaTrabajador = LlenarAsistencia(item, miFechaInicio, miFechaFin);
                //List<PermisosDias> miPermisoDiasTrabajador = LlenarPermisos(item, miFechaInicio, miFechaFin);
                //PeriodoTrabajador miPeridodTrabajador = CargarPeriodoTrabajador(item);
                //List<Dia> miHorarioDia = CargarHorario(miPeridodTrabajador);

                contador += 1;
                oHoja.Range["A" + (5 + contador).ToString()].Formula = contador;
                oHoja.Range["B" + (5 + contador).ToString()].Formula = item.ApellidoPaterno.ToString() + " " + item.ApellidoMaterno.ToString() + ", " + item.Nombre.ToString();//APELLIDSO Y NOMBRES
                oHoja.Range["E" + (5 + contador).ToString()].Formula = item.DNI.ToString();//DNI
                //oHoja.Range["G" + (5 + contador).ToString()].Formula = item.Sexo.ToString();//SEXO

                //for (int i = 0; i <= 40; i++)
                //{
                //    DateTime auxiliar = miFechaInicio.AddDays(i);

                //}

                if (contador < miListaTrabajadores.Count)
                {
                    oHoja.Range[(6 + contador).ToString() + ":" + (6 + contador).ToString()].Insert();
                }
            }
        }

        public List<Asistencia> LlenarAsistencia(Trabajador miTrabajador, DateTime miFechaInicio, DateTime miFechaFin)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Asistencia> consultaAsistencia = from d in bd.AsistenciaSet
                                                            where d.Trabajador.Id == miTrabajador.Id
                                                            && d.PicadoReloj >= miFechaInicio
                                                            && d.PicadoReloj <= miFechaFin
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

        public List<Dia> CargarHorario(PeriodoTrabajador miPeriodoTrabajador)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Dia> consultaHorarioDia = from d in bd.DiaSet.Include("HorarioDia.Horario")
                                                     where d.HorarioSemana.Id == miPeriodoTrabajador.HorarioSemana.Id
                                                     select d;
                return consultaHorarioDia.ToList();
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
