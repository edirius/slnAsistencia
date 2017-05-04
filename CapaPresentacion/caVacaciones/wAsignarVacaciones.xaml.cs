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

namespace CapaPresentacion.caVacaciones
{
    /// <summary>
    /// Lógica de interacción para wAsignarVacaciones.xaml
    /// </summary>
    public partial class wAsignarVacaciones : Window
    {
        public Vacaciones miVacaciones = new Vacaciones();

        public wAsignarVacaciones()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpInicio.DisplayDateStart = miVacaciones.Inicio;
            dtpInicio.SelectedDate = miVacaciones.Inicio;
            //dtpFin.SelectedDate = miVacaciones.Fin;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            miVacaciones.Inicio = Convert.ToDateTime(dtpInicio.Text);
            miVacaciones.Fin = Convert.ToDateTime(dtpFin.Text);
            this.DialogResult = true;
        }

        private void dtpInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dtpFin.DisplayDateStart = dtpInicio.DisplayDate.AddDays(1);
            dtpFin.DisplayDateEnd = dtpInicio.DisplayDate.AddDays(miVacaciones.DiasVacacionesDisponibles - 1);
            dtpFin.SelectedDate = dtpInicio.DisplayDate.AddDays(miVacaciones.DiasVacacionesDisponibles - 1);
        }

        private void dtpFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
