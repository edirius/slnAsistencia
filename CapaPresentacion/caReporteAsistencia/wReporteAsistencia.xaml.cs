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
using Microsoft.Win32;
using CapaEntities;
using CapaDeNegocios;

namespace CapaPresentacion.caReporteAsistencia
{
    /// <summary>
    /// Interaction logic for wReporteAsistencia.xaml
    /// </summary>
    public partial class wReporteAsistencia : Window
    {
        Local miLocal = new Local();
        System.Data.DataTable oDataTrabajadores = new System.Data.DataTable();

        public wReporteAsistencia()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpFechaInicio.SelectedDate = DateTime.Today;
            CargarLocales();
        }

        private void cbolocales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboLocales.DisplayMemberPath != "")
            {
                miLocal.Id = Convert.ToInt32(cboLocales.SelectedValue);
                CargarTrabajadores();
            }
        }

        private void dtpFechaInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dtpFechaFin.DisplayDateStart = dtpFechaInicio.SelectedDate;
        }

        private void btnExportarExcel_Click(object sender, RoutedEventArgs e)
        {
            List<Trabajador> ListaTrabajadores = new List<Trabajador>();
            foreach (System.Data.DataRowView item in dtgListaTrabajadores.Items)
            {
                bool Activo = false;
                Activo = Convert.ToBoolean(item.Row.ItemArray[5]);
                if (Activo == true)
                {
                    Trabajador auxTrabajador = new Trabajador();
                    auxTrabajador.Id = Convert.ToInt32(item.Row.ItemArray[0]);
                    auxTrabajador.Nombre = Convert.ToString(item.Row.ItemArray[1]);
                    auxTrabajador.ApellidoPaterno = Convert.ToString(item.Row.ItemArray[2]);
                    auxTrabajador.ApellidoMaterno = Convert.ToString(item.Row.ItemArray[3]);
                    auxTrabajador.DNI = Convert.ToString(item.Row.ItemArray[4]);
                    ListaTrabajadores.Add(auxTrabajador);
                }
            }

            CapaDeNegocios.cblReportesAsistencia.blReporteAsistencia oblReporteAsistencia = new CapaDeNegocios.cblReportesAsistencia.blReporteAsistencia();
            CapaDeNegocios.cblReportesAsistencia.cReporteAsistencia oReporteAsistencia = new CapaDeNegocios.cblReportesAsistencia.cReporteAsistencia();
            SaveFileDialog saveFileDialog  = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos Excel (*.xls)|*.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                CapaDeNegocios.cblReportesAsistencia.blExportarExcelReporteAsistencia oblExportarExcelReporteAsistencia = new CapaDeNegocios.cblReportesAsistencia.blExportarExcelReporteAsistencia(saveFileDialog.FileName);
                oReporteAsistencia = oblReporteAsistencia.LlenarReporteAsistencia(ListaTrabajadores, dtpFechaInicio.SelectedDate.Value, dtpFechaFin.SelectedDate.Value);
                oblExportarExcelReporteAsistencia.ImprimirReporteAsistencia(oReporteAsistencia);
            }
        }

        private void CargarLocales()
        {
            CapaDeNegocios.blLocal.blLocal oblLocal = new CapaDeNegocios.blLocal.blLocal();
            cboLocales.ItemsSource = oblLocal.ListarLocales();
            cboLocales.DisplayMemberPath = "Nombre";
            cboLocales.SelectedValuePath = "Id";
            cboLocales.SelectedIndex = -1;
        }

        private void CargarTrabajadores()
        {
            CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();
            ICollection<Trabajador> ListaTrabajadores = oblTrabajador.ListaTrabajadores(miLocal);

            oDataTrabajadores = new System.Data.DataTable();
            oDataTrabajadores.Columns.Add("ID", typeof(int));
            oDataTrabajadores.Columns.Add("NOMBRE");
            oDataTrabajadores.Columns.Add("APATERNO");
            oDataTrabajadores.Columns.Add("AMATERNO");
            oDataTrabajadores.Columns.Add("DNI");
            oDataTrabajadores.Columns.Add("CHK", typeof(bool));
            foreach (Trabajador item in ListaTrabajadores)
            {
                var row = oDataTrabajadores.NewRow();
                row["ID"] = item.Id;
                row["NOMBRE"] = item.Nombre;
                row["APATERNO"] = item.ApellidoPaterno;
                row["AMATERNO"] = item.ApellidoMaterno;
                row["DNI"] = item.DNI;
                row["CHK"] = false;
                oDataTrabajadores.Rows.Add(row);
            }
            dtgListaTrabajadores.ItemsSource = oDataTrabajadores.DefaultView;

            if (dtgListaTrabajadores.Items.Count > 0)
            {
                object item = dtgListaTrabajadores.Items[dtgListaTrabajadores.Items.Count - 1];
                dtgListaTrabajadores.SelectedItem = item;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (System.Data.DataRow dr in oDataTrabajadores.Rows)
            {
                dr["CHK"] = true;
            }
        }

        private void UnCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (System.Data.DataRow dr in oDataTrabajadores.Rows)
            {
                dr["CHK"] = false;
            }
        }

        private void Chk_Checked(object sender, RoutedEventArgs e)
        {
            int i = dtgListaTrabajadores.SelectedIndex;
            System.Data.DataRow dr = oDataTrabajadores.Rows[i];
            if (Convert.ToBoolean(dr["CHK"]) == false)
            {
                dr["CHK"] = true;
            }
            else
            {
                dr["CHK"] = false;
            }
        }
    }
}
