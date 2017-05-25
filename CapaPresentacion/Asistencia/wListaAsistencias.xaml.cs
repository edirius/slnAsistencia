using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Windows.Forms;
using miExcel = Microsoft.Office.Interop.Excel;
using CapaEntities;
using CapaDeNegocios;

namespace CapaPresentacion.Asistencia
{
    /// <summary>
    /// Lógica de interacción para wListaAsistencias.xaml
    /// </summary>
    public partial class wCargarAsistencias : Window
    {
        ICollection<Trabajador> ListaTrabajadores;
        CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();

        public wCargarAsistencias()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListaTrabajadores = oblTrabajador.ListaTrabajadores();
        }

        private void btnImportar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xlsx";
            openfile.Filter = "(.xlsx)|*.xlsx";
            //openfile.ShowDialog();

            var browsefile = openfile.ShowDialog();
            if (browsefile == System.Windows.Forms.DialogResult.OK)
            {
                txtExcel.Text = openfile.FileName;

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                //Static File From Base Path...........
                //Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "TestExcel.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                //Dynamic File Using Uploader...........
                Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(txtExcel.Text.ToString(), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1); ;
                Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

                string strCellData = "";
                double douCellData;
                int rowCnt = 0;
                int colCnt = 0;

                DataTable dt = new DataTable();
                for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                {
                    string strColumn = "";
                    strColumn = Convert.ToString((excelRange.Cells[1, colCnt] as Microsoft.Office.Interop.Excel.Range).Value);
                    dt.Columns.Add(strColumn, typeof(string));
                }

                for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
                {
                    string strData = "";
                    for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                    {
                        try
                        {
                            strCellData = Convert.ToString((excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value);
                            strData += strCellData + "|";
                        }
                        catch (Exception ex)
                        {
                            douCellData = (excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            strData += douCellData.ToString() + "|";
                        }
                    }
                    strData = strData.Remove(strData.Length - 1, 1);
                    dt.Rows.Add(strData.Split('|'));
                }
                dgExcel.ItemsSource = dt.DefaultView;
                excelBook.Close(true, null, null);
                excelApp.Quit();
            }
        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            CapaEntities.Asistencia miAsistencia = new CapaEntities.Asistencia();
            CapaDeNegocios.blAsistencia.blAsistencia oblAsistencia = new CapaDeNegocios.blAsistencia.blAsistencia();
            for (int i = 0; i < dgExcel.Items.Count; i++)
            {
                int miDNI = 0;
                string miFechaPicado = "";
                miDNI = Convert.ToInt32((dgExcel.Items[i] as System.Data.DataRowView).Row.ItemArray[0]);
                miFechaPicado = Convert.ToString((dgExcel.Items[i] as System.Data.DataRowView).Row.ItemArray[1]);

                string xx = miFechaPicado.Substring(0, 19) + " " + miFechaPicado.Substring(19, 2);
                miAsistencia.Trabajador = Seleccionar_Trabajador(miDNI);
                miAsistencia.PicadoReloj = Convert.ToDateTime(xx);
                //miAsistencia.PicadoReloj = Seleccionar_Fecha(miFechaPicado);
                oblAsistencia.AgregarAsistencia(miAsistencia);
            }
        }

        private Trabajador Seleccionar_Trabajador(int miDNI)
        {
            Trabajador miTrabajador = new Trabajador();
            foreach (Trabajador name in ListaTrabajadores.ToList())
            {
                if (miDNI == Convert.ToInt32(name.DNI))
                {
                    miTrabajador = name;
                }
            }
            return miTrabajador;
        }

        private DateTime Seleccionar_Fecha(string Fecha)
        {
            DateTime miF = new DateTime();
            TimeSpan miH = new TimeSpan();
            DateTime miFecha = new DateTime();
            miF = Convert.ToDateTime(Fecha.Substring(0, 10));
            miH = Hora_24H(Fecha.Substring(11, 10));
            //miH = DateTime.ParseExact("01:45 PM", "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
            miFecha = miF.Add(miH);
            return miFecha;
        }

        private TimeSpan Hora_24H(string Hora)
        {
            int H = 0;
            int M = 0;
            int S = 0;
            TimeSpan miH = new TimeSpan();
            if (Hora.Substring(6, 4) == "a.m.")
            {
                H = Convert.ToInt32(Hora.Substring(0, 2));
            }
            else if (Hora.Substring(6, 4) == "p.m.")
            {
                switch (Convert.ToInt32(Hora.Substring(0, 2)))
                {
                    case 1:
                        H = 13;
                        break;
                    case 2:
                        H = 14;
                        break;
                    case 3:
                        H = 15;
                        break;
                    case 4:
                        H = 16;
                        break;
                    case 5:
                        H = 17;
                        break;
                    case 6:
                        H = 18;
                        break;
                    case 7:
                        H = 19;
                        break;
                    case 8:
                        H = 20;
                        break;
                    case 9:
                        H = 21;
                        break;
                    case 10:
                        H = 22;
                        break;
                    case 11:
                        H = 23;
                        break;
                }
            }
            M = Convert.ToInt32(Hora.Substring(3, 2));
            S = 0;
            miH = new TimeSpan(H, M, S);
            return miH;
        }
    }
}
