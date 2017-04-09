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
using CapaEntities;

namespace CapaPresentacion.wImportarAsistencia
{
    /// <summary>
    /// Interaction logic for wAsistencia.xaml
    /// </summary>
    public partial class wAsistencia : Window
    {
        public CapaEntities.Asistencia miAsistencia;

        public wAsistencia()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            dpFecha.SelectedDate = miAsistencia.PicadoReloj.Date;
            txtHora.Text = miAsistencia.PicadoReloj.Hour.ToString();
            txtMinuto.Text = miAsistencia.PicadoReloj.Minute.ToString();
            txtSegundo.Text = miAsistencia.PicadoReloj.Second.ToString();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            
            miAsistencia.PicadoReloj = new DateTime(dpFecha.SelectedDate.Value.Year, dpFecha.SelectedDate.Value.Month, dpFecha.SelectedDate.Value.Day, Convert.ToInt16(txtHora.Text), Convert.ToInt16(txtMinuto.Text), Convert.ToInt16(txtSegundo.Text));
            DialogResult = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
