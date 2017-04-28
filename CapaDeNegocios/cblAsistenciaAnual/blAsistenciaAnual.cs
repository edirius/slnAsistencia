using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.cblAsistenciaAnual
{
    public class blAsistenciaAnual
    {
        public cAsistenciaPeriodoLaborado CalcularAsistenciaPeriodoTrabajador(Trabajador miTrabajador, cAsistenciaPeriodoLaborado micAsistenciaPeriodoLaborado)
        {
            if (micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Inicio.AddDays(364).Date >= DateTime.Today.Date)
            { micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Fin = DateTime.Today.Date; }
            else
            { micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Fin = micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Inicio.AddDays(364).Date; }
            micAsistenciaPeriodoLaborado = CrearMeses(micAsistenciaPeriodoLaborado);
            micAsistenciaPeriodoLaborado = LlenarAsistencia(miTrabajador, micAsistenciaPeriodoLaborado);
            micAsistenciaPeriodoLaborado = LlenarPermisos(miTrabajador, micAsistenciaPeriodoLaborado);
            //while (miPeriodoTrabajador.AsistenciaPeriodoLaborado[miPeriodoTrabajador.AsistenciaPeriodoLaborado.Count - 1].fechaFin.Date < DateTime.Today.Date)
            //{
                //cAsistenciaPeriodoLaborado auxiliar = new cAsistenciaPeriodoLaborado();
                //auxiliar.Inicio = miPeriodoTrabajador.miListaAsistenciaPeriodoLaborado[miPeriodoTrabajador.miListaAsistenciaPeriodoLaborado.Count - 1].fechaFin.AddDays(1).Date;
                //auxiliar.Fin = auxiliar.Inicio.AddDays(364).Date;
                ////auxiliar = CrearMeses(auxiliar);
                ////auxiliar = LlenarAsistencia(miTrabajador, auxiliar);
                ////auxiliar = LlenarPermisos(miTrabajador, auxiliar);
                //miPeriodoTrabajador.miListaAsistenciaPeriodoLaborado.Add(auxiliar);
            //}
            return micAsistenciaPeriodoLaborado;
        }

        public cAsistenciaPeriodoLaborado CrearMeses(cAsistenciaPeriodoLaborado micAsistenciaPeriodoLaborado)
        {
            DateTime fechaInicio = micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Inicio;
            DateTime fechaFin = micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Fin;
            int nroAños = fechaFin.Year - fechaInicio.Year;
            int año = 0;
            int mesinicio = 0;
            int mesfin = 0;
            for (int i = 0; i <= nroAños; i++)
            {
                año = fechaInicio.Year + i;
                if (nroAños == 0)
                {
                    mesinicio = fechaInicio.Month;
                    mesfin = fechaFin.Month;
                }
                else
                {
                    if (año == fechaInicio.Year)
                    {
                        mesinicio = fechaInicio.Month;
                        mesfin = 12;
                    }
                    else if (año > fechaInicio.Year && año < fechaFin.Year)
                    {
                        mesinicio = 1;
                        mesfin = 12;
                    }
                    else if (año == fechaFin.Year)
                    {
                        mesinicio = 1;
                        mesfin = fechaFin.Month;
                    }
                }
                for (int j = mesinicio; j <= mesfin; j++)
                {
                    if (j == 1)
                    {
                        cAsistenciaMeses Enero = new cblAsistenciaAnual.cAsistenciaMeses();
                        Enero.nombreAño = año;
                        Enero.nombreMes = "Enero";
                        Enero.miAsistenciaPeriodoLaborado = micAsistenciaPeriodoLaborado;
                        micAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Enero);
                    }
                    else if (j == 2)
                    {
                        cAsistenciaMeses Febrero = new cblAsistenciaAnual.cAsistenciaMeses();
                        Febrero.nombreAño = año;
                        Febrero.nombreMes = "Febrero";
                        Febrero.miAsistenciaPeriodoLaborado = micAsistenciaPeriodoLaborado;
                        micAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Febrero);
                    }
                    else if (j == 3)
                    {
                        cAsistenciaMeses Marzo = new cblAsistenciaAnual.cAsistenciaMeses();
                        Marzo.nombreAño = año;
                        Marzo.nombreMes = "Marzo";
                        Marzo.miAsistenciaPeriodoLaborado = micAsistenciaPeriodoLaborado;
                        micAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Marzo);
                    }
                    else if (j == 4)
                    {
                        cAsistenciaMeses Abril = new cblAsistenciaAnual.cAsistenciaMeses();
                        Abril.nombreAño = año;
                        Abril.nombreMes = "Abril";
                        Abril.miAsistenciaPeriodoLaborado = micAsistenciaPeriodoLaborado;
                        micAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Abril);
                    }
                    else if (j == 5)
                    {
                        cAsistenciaMeses Mayo = new cblAsistenciaAnual.cAsistenciaMeses();
                        Mayo.nombreAño = año;
                        Mayo.nombreMes = "Mayo";
                        Mayo.miAsistenciaPeriodoLaborado = micAsistenciaPeriodoLaborado;
                        micAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Mayo);
                    }
                    else if (j == 6)
                    {
                        cAsistenciaMeses Junio = new cblAsistenciaAnual.cAsistenciaMeses();
                        Junio.nombreAño = año;
                        Junio.nombreMes = "Junio";
                        Junio.miAsistenciaPeriodoLaborado = micAsistenciaPeriodoLaborado;
                        micAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Junio);
                    }
                    else if (j == 7)
                    {
                        cAsistenciaMeses Julio = new cblAsistenciaAnual.cAsistenciaMeses();
                        Julio.nombreAño = año;
                        Julio.nombreMes = "Julio";
                        Julio.miAsistenciaPeriodoLaborado = micAsistenciaPeriodoLaborado;
                        micAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Julio);
                    }
                    else if (j == 8)
                    {
                        cAsistenciaMeses Agosto = new cblAsistenciaAnual.cAsistenciaMeses();
                        Agosto.nombreAño = año;
                        Agosto.nombreMes = "Agosto";
                        Agosto.miAsistenciaPeriodoLaborado = micAsistenciaPeriodoLaborado;
                        micAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Agosto);
                    }
                    else if (j == 9)
                    {
                        cAsistenciaMeses Setiembre = new cblAsistenciaAnual.cAsistenciaMeses();
                        Setiembre.nombreAño = año;
                        Setiembre.nombreMes = "Setiembre";
                        Setiembre.miAsistenciaPeriodoLaborado = micAsistenciaPeriodoLaborado;
                        micAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Setiembre);
                    }
                    else if (j == 10)
                    {
                        cAsistenciaMeses Octubre = new cblAsistenciaAnual.cAsistenciaMeses();
                        Octubre.nombreAño = año;
                        Octubre.nombreMes = "Octubre";
                        Octubre.miAsistenciaPeriodoLaborado = micAsistenciaPeriodoLaborado;
                        micAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Octubre);
                    }
                    else if (j == 11)
                    {
                        cAsistenciaMeses Noviembre = new cblAsistenciaAnual.cAsistenciaMeses();
                        Noviembre.nombreAño = año;
                        Noviembre.nombreMes = "Noviembre";
                        Noviembre.miAsistenciaPeriodoLaborado = micAsistenciaPeriodoLaborado;
                        micAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Noviembre);
                    }
                    else if (j == 12)
                    {
                        cAsistenciaMeses Diciembre = new cblAsistenciaAnual.cAsistenciaMeses();
                        Diciembre.nombreAño = año;
                        Diciembre.nombreMes = "Diciembre";
                        Diciembre.miAsistenciaPeriodoLaborado = micAsistenciaPeriodoLaborado;
                        micAsistenciaPeriodoLaborado.miListaAsistenciaMeses.Add(Diciembre);
                    }
                }
            }
            return micAsistenciaPeriodoLaborado;
        }

        public cAsistenciaPeriodoLaborado LlenarAsistencia(Trabajador miTrabajador, cAsistenciaPeriodoLaborado micAsistenciaPeriodoLaborado)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Asistencia> consultaAsistencia = from d in bd.AsistenciaSet
                                                            where d.Trabajador.Id == miTrabajador.Id
                                                            && d.PicadoReloj >= micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Inicio
                                                            && d.PicadoReloj <= micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Fin
                                                            select d;

                int nroMeses = Math.Abs((micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Fin.Month - micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Inicio.Month) + 12 * (micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Fin.Year - micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Inicio.Year));
                foreach (Asistencia item in consultaAsistencia)
                {
                    for (int j = 0; j <= nroMeses; j++)
                    {
                        if (item.PicadoReloj.Year == micAsistenciaPeriodoLaborado.miListaAsistenciaMeses[j].nombreAño && item.PicadoReloj.ToString("MMMM") == micAsistenciaPeriodoLaborado.miListaAsistenciaMeses[j].nombreMes)
                        {
                            cAsistenciaDia auxiliardia = new cAsistenciaDia();
                            auxiliardia.fecha = item.PicadoReloj;
                            auxiliardia.asistencia = true;
                            //auxiliardia.miPermiso = Permisos(miTrabajador, auxiliardia);
                            micAsistenciaPeriodoLaborado.miListaAsistenciaMeses[j].miListaAsistenciaDias.Add(auxiliardia);
                        }
                    }
                }
                return micAsistenciaPeriodoLaborado;
            }
        }

        public cAsistenciaPeriodoLaborado LlenarPermisos(Trabajador miTrabajador, cAsistenciaPeriodoLaborado micAsistenciaPeriodoLaborado)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PermisosDias> consultaPermisos = from d in bd.PermisosDiasSet.Include("TipoPermisos")
                                                            where d.PeriodoTrabajador.Trabajador.Id == miTrabajador.Id
                                                            select d;

                int nroMeses = Math.Abs((micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Fin.Month - micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Inicio.Month) + 12 * (micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Fin.Year - micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Inicio.Year));
                foreach (PermisosDias item in consultaPermisos)
                {
                    int nroDiasPermiso = (item.Fin - item.Inicio).Days;
                    for (int i = 0; i <= nroDiasPermiso; i++)
                    {
                        DateTime fechaauxiliar = item.Inicio.AddDays(i);
                        for (int j = 0; j <= nroMeses; j++)
                        {
                            if (fechaauxiliar.Year == micAsistenciaPeriodoLaborado.miListaAsistenciaMeses[j].nombreAño && fechaauxiliar.ToString("MMMM") == micAsistenciaPeriodoLaborado.miListaAsistenciaMeses[j].nombreMes)
                            {
                                cAsistenciaDia auxiliardia = new cAsistenciaDia();
                                auxiliardia.fecha = fechaauxiliar;
                                auxiliardia.asistencia = false;
                                auxiliardia.miPermiso = item;
                                micAsistenciaPeriodoLaborado.miListaAsistenciaMeses[j].miListaAsistenciaDias.Add(auxiliardia);
                            }
                        }
                    }
                }
                return micAsistenciaPeriodoLaborado;
            }
        }

        //public CapaDeNegocios.cblAsistenciaAnual.cAsistenciaMeses CalculoDiasAsistenciaMeses(int Año, int Mes, cblAsistenciaAnual.cAsistenciaPeriodoTrabajador miAsistenciaPeriodoTrabajador)
        //{
        //    cblAsistenciaAnual.cAsistenciaPeriodoLaborado miAsistenciaAnual = miAsistenciaPeriodoTrabajador.miListaAsistenciaPeriodoLaborado[miAsistenciaPeriodoTrabajador.miListaAsistenciaPeriodoLaborado.Count - 1];
        //    cblAsistenciaAnual.cAsistenciaMeses miAsistenciaMeses = new cblAsistenciaAnual.cAsistenciaMeses();

        //    int nroMeses = Math.Abs((miAsistenciaAnual.fechaFin.Month - miAsistenciaAnual.fechaInicio.Month) + 12 * (miAsistenciaAnual.fechaFin.Year - miAsistenciaAnual.fechaInicio.Year));
        //    for (int i = 0; i <= nroMeses; i++)
        //    {
        //        if (Año == miAsistenciaAnual.miListaAsistenciaMeses[i].nombreAño && Convert.ToDateTime("01/" + Mes + "/" + Año).ToString("MMMM") == miAsistenciaAnual.miListaAsistenciaMeses[i].nombreMes)
        //        {
        //            foreach (cblAsistenciaAnual.cAsistenciaDia item in miAsistenciaAnual.miListaAsistenciaMeses[i].miListaAsistenciaDias)
        //            {
        //                if (item.asistencia == true)
        //                {
        //                    miAsistenciaAnual.miListaAsistenciaMeses[i].diasLaborados += 1;
        //                }
        //                if (item.miPermiso != null)
        //                {
        //                    if (item.miPermiso.TipoPermisos.Computable == false)
        //                    {
        //                        miAsistenciaAnual.miListaAsistenciaMeses[i].diasPermisos += 1;
        //                    }
        //                    else
        //                    {
        //                        miAsistenciaAnual.miListaAsistenciaMeses[i].diasFaltas += 1;
        //                    }
        //                }
        //            }
        //            miAsistenciaAnual.miListaAsistenciaMeses[i].diasTotal = miAsistenciaAnual.miListaAsistenciaMeses[i].diasLaborados + miAsistenciaAnual.miListaAsistenciaMeses[i].diasPermisos;
        //            miAsistenciaMeses = miAsistenciaAnual.miListaAsistenciaMeses[i];
        //        }
        //    }
        //    return miAsistenciaMeses;
        //}

        public cAsistenciaPeriodoLaborado CalculoDiasAsistenciaAnual(cAsistenciaPeriodoLaborado micAsistenciaPeriodoTrabajador)
        {
            int nroMeses = Math.Abs((micAsistenciaPeriodoTrabajador.miAsistenciaPeriodoLaborado.Fin.Month - micAsistenciaPeriodoTrabajador.miAsistenciaPeriodoLaborado.Inicio.Month) + 12 * (micAsistenciaPeriodoTrabajador.miAsistenciaPeriodoLaborado.Fin.Year - micAsistenciaPeriodoTrabajador.miAsistenciaPeriodoLaborado.Inicio.Year));
            for (int i = 0; i <= nroMeses; i++)
            {
                foreach (cblAsistenciaAnual.cAsistenciaDia item in micAsistenciaPeriodoTrabajador.miListaAsistenciaMeses[i].miListaAsistenciaDias)
                {
                    if (item.asistencia == true)
                    {
                        micAsistenciaPeriodoTrabajador.miAsistenciaPeriodoLaborado.DiasLaborados += 1;
                    }
                    if (item.miPermiso != null)
                    {
                        if (item.miPermiso.TipoPermisos.Computable == true)
                        {
                            micAsistenciaPeriodoTrabajador.miAsistenciaPeriodoLaborado.DiasPermisosComputables += 1;
                        }
                        else
                        {
                            micAsistenciaPeriodoTrabajador.miAsistenciaPeriodoLaborado.DiasPermisosNoComputables += 1;
                        }
                    }
                }
            }
            return micAsistenciaPeriodoTrabajador;
        }

        //public bool AsignarVacaciones(CapaDeNegocios.cblAsistenciaAnual.cAsistenciaPeriodoTrabajador miAsistenciaPeriodo, CapaDeNegocios.cblAsistenciaAnual.cAsistenciaPeriodoLaborado miAsistenciaAnual, CapaDeNegocios.cblVacaciones.cVacaciones miVacaciones)
        //{
        //    if (miAsistenciaPeriodo.miListaAsistenciaPeriodoLaborado.Count >= 3)
        //    {
        //        if (miAsistenciaPeriodo.miListaAsistenciaPeriodoLaborado[miAsistenciaPeriodo.miListaAsistenciaPeriodoLaborado.Count - 2].miVacaciones.vacacionesEfectuadas == false && miAsistenciaPeriodo.miListaAsistenciaPeriodoLaborado[miAsistenciaPeriodo.miListaAsistenciaPeriodoLaborado.Count - 3].miVacaciones.vacacionesEfectuadas == false && miVacaciones.vacacionesEfectuadas == false)
        //        {
        //            throw new cReglaNegociosException("No se puede postergar xq ya existen 2 periodos acumulados.");
        //        }
        //        else
        //        {
        //            miAsistenciaAnual.miVacaciones = miVacaciones;
        //        }
        //    }
        //    else
        //    {
        //        miAsistenciaAnual.miVacaciones = miVacaciones;
        //    }
        //    return true;
        //}
    }
}
