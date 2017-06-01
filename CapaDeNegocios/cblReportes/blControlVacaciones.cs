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
    public class blControlVacaciones
    {
        private Microsoft.Office.Interop.Excel.Application oExcel;
        private object oMissing;
        private Microsoft.Office.Interop.Excel.Workbook oLibro;
        private Microsoft.Office.Interop.Excel.Worksheet oHoja;
        public string rutaarchivo = AppDomain.CurrentDomain.BaseDirectory + "R_ControlVacaciones.xltx";

        int mEne; int mFeb; int mMar; int mAbr; int mMay; int mJun; int mJul; int mAgo; int mSet; int mOct; int mNov; int mDic;
        int mAcumulado;

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

        public void Control_Vacaciones(List<Trabajador> miListaTrabajadores, int miAño)
        {
            Iniciar();
            int contador = 0;
            int nro_filas = 0;
            int celda_inicio = 10;
            foreach (Trabajador item in miListaTrabajadores)
            {
                nro_filas += 1;
                List<PermisosDias> miPermisoDiasTrabajador = LlenarPermisos(item, miAño);
                CONTROL_ASISTENCIA(miPermisoDiasTrabajador, miAño);

                oHoja.Range["A7"].Formula = "CONTROL DE VACACIONES DEL AÑO " + miAño;
                oHoja.Range["A" + (celda_inicio + contador).ToString()].Formula = nro_filas;
                oHoja.Range["B" + (celda_inicio + contador).ToString()].Formula = item.DNI.ToString();//DNI
                oHoja.Range["C" + (celda_inicio + contador).ToString()].Formula = item.ApellidoPaterno.ToString() + " " + item.ApellidoMaterno.ToString() + ", " + item.Nombre.ToString();//APELLIDSO Y NOMBRES
                oHoja.Range["G" + (celda_inicio + contador).ToString()].Formula = mEne;
                oHoja.Range["H" + (celda_inicio + contador).ToString()].Formula = mFeb;
                oHoja.Range["I" + (celda_inicio + contador).ToString()].Formula = mMar;
                oHoja.Range["J" + (celda_inicio + contador).ToString()].Formula = mAbr;
                oHoja.Range["K" + (celda_inicio + contador).ToString()].Formula = mMay;
                oHoja.Range["L" + (celda_inicio + contador).ToString()].Formula = mJun;
                oHoja.Range["M" + (celda_inicio + contador).ToString()].Formula = mJul;
                oHoja.Range["N" + (celda_inicio + contador).ToString()].Formula = mAgo;
                oHoja.Range["O" + (celda_inicio + contador).ToString()].Formula = mSet;
                oHoja.Range["P" + (celda_inicio + contador).ToString()].Formula = mOct;
                oHoja.Range["Q" + (celda_inicio + contador).ToString()].Formula = mNov;
                oHoja.Range["R" + (celda_inicio + contador).ToString()].Formula = mDic;
                oHoja.Range["S" + (celda_inicio + contador).ToString()].Formula = mAcumulado;
                oHoja.Range["T" + (celda_inicio + contador).ToString()].Formula = mEne + mFeb + mMar + mAbr + mMay + mJun + mJul + mAgo + mSet + mOct + mNov + mDic + mAcumulado;

                if (nro_filas < miListaTrabajadores.Count)
                {
                    contador += 1;
                    oHoja.Range[(celda_inicio + contador).ToString() + ":" + (celda_inicio + contador).ToString()].Insert();
                }
            }
        }

        public void CONTROL_ASISTENCIA(List<PermisosDias> miPermisoDiasTrabajador, int miAño)
        {
            mEne = 0; mFeb = 0; mMar = 0; mAbr = 0; mMay = 0; mJun = 0; mJul = 0; mAgo = 0; mSet = 0; mOct = 0; mNov = 0; mDic = 0;
            mAcumulado = 0;
            foreach (PermisosDias item in miPermisoDiasTrabajador)
            {
                for (int i = 0; i <= (item.Fin - item.Inicio).Days; i++)
                {
                    DateTime auxiliar = item.Inicio.AddDays(i);
                    if (auxiliar.Month == 1 && auxiliar.Year == miAño)
                    {
                        mEne += 1;
                    }
                    else if (auxiliar.Month == 2 && auxiliar.Year == miAño)
                    {
                        mFeb += 1;
                    }
                    else if (auxiliar.Month == 3 && auxiliar.Year == miAño)
                    {
                        mMar += 1;
                    }
                    else if (auxiliar.Month == 4 && auxiliar.Year == miAño)
                    {
                        mAbr += 1;
                    }
                    else if (auxiliar.Month == 5 && auxiliar.Year == miAño)
                    {
                        mMay += 1;
                    }
                    else if (auxiliar.Month == 6 && auxiliar.Year == miAño)
                    {
                        mJun += 1;
                    }
                    else if (auxiliar.Month == 7 && auxiliar.Year == miAño)
                    {
                        mJul += 1;
                    }
                    else if (auxiliar.Month == 8 && auxiliar.Year == miAño)
                    {
                        mAgo += 1;
                    }
                    else if (auxiliar.Month == 9 && auxiliar.Year == miAño)
                    {
                        mSet += 1;
                    }
                    else if (auxiliar.Month == 10 && auxiliar.Year == miAño)
                    {
                        mOct += 1;
                    }
                    else if (auxiliar.Month == 11 && auxiliar.Year == miAño)
                    {
                        mNov += 1;
                    }
                    else if (auxiliar.Month == 12 && auxiliar.Year == miAño)
                    {
                        mDic += 1;
                    }
                }
            }

            bool final = false;
            int acu = 0;
            int t_acu = 0;
            for (int i = 0; i < 12; i++)
            {
                switch (i + 1)
                {
                    case 1:
                        acu += mEne;
                        if (mFeb == 0) { final = true; }
                        break;
                    case 2:
                        acu += mFeb;
                        if (mMar == 0) { final = true; }
                        break;
                    case 3:
                        acu += mMar;
                        if (mAbr == 0) { final = true; }
                        break;
                    case 4:
                        acu += mAbr;
                        if (mMay == 0) { final = true; }
                        break;
                    case 5:
                        acu += mMay;
                        if (mJun == 0) { final = true; }
                        break;
                    case 6:
                        acu += mJun;
                        if (mJul == 0) { final = true; }
                        break;
                    case 7:
                        acu += mJul;
                        if (mAgo == 0) { final = true; }
                        break;
                    case 8:
                        acu += mAgo;
                        if (mSet == 0) { final = true; }
                        break;
                    case 9:
                        acu += mSet;
                        if (mOct == 0) { final = true; }
                        break;
                    case 10:
                        acu += mOct;
                        if (mNov == 0) { final = true; }
                        break;
                    case 11:
                        acu += mNov;
                        if (mDic == 0) { final = true; }
                        break;
                    case 12:
                        acu += mDic;
                        final = true;
                        break;
                }


                if (final == true)
                {
                    t_acu += (acu / 5) * 2;
                    acu = 0;
                    final = false;
                }
            }
            mAcumulado = t_acu;
        }

        public List<PermisosDias> LlenarPermisos(Trabajador miTrabajador, int miAño)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<PermisosDias> consultaPermisos = from d in bd.PermisosDiasSet.Include("TipoPermisos")
                                                            where d.PeriodoTrabajador.Trabajador.Id == miTrabajador.Id
                                                            && d.Inicio.Year == miAño
                                                            select d;
                return consultaPermisos.ToList();
            }
        }
    }
}
