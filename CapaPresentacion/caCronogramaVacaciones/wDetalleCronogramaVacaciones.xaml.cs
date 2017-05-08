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
using CapaDeNegocios;
using CapaEntities;

namespace CapaPresentacion.caCronogramaVacaciones
{
    /// <summary>
    /// Lógica de interacción para wDetalleCronogramaVacaciones.xaml
    /// </summary>
    public partial class wDetalleCronogramaVacaciones : Window
    {
        public DetalleCronogramaVacaciones miDetalleCronogramaVacaciones;

        public wDetalleCronogramaVacaciones()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            dtpInicio.SelectedDate = miDetalleCronogramaVacaciones.Inicio;
            dtpFin.SelectedDate = miDetalleCronogramaVacaciones.Fin;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            miDetalleCronogramaVacaciones.Inicio = Convert.ToDateTime(dtpInicio.Text);
            miDetalleCronogramaVacaciones.Fin = Convert.ToDateTime(dtpFin.Text);
            this.DialogResult = true;
        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void dtpInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dtpFin.DisplayDateStart = dtpInicio.DisplayDate.AddDays(1);
            dtpFin.DisplayDateEnd = dtpInicio.DisplayDate.AddDays(30 - 1);
            dtpFin.SelectedDate = dtpInicio.DisplayDate.AddDays(30 - 1);
        }

        private void dtpFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
