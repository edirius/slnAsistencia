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
        public wReporteAsistencia()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();
            ICollection<Trabajador> ListaTrabajadores = oblTrabajador.ListaTrabajadores();
            dtgListaTrabajadores.ItemsSource = ListaTrabajadores;
            dtpFechaInicio.SelectedDate = DateTime.Today;
            
        }

        private void btnExportarExcel_Click(object sender, RoutedEventArgs e)
        {
            CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();
            ICollection<Trabajador> ListaTrabajadores = oblTrabajador.ListaTrabajadores();

            
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

        private void dtpFechaInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dtpFechaFin.DisplayDateStart = dtpFechaInicio.SelectedDate;

        }
    }
}
