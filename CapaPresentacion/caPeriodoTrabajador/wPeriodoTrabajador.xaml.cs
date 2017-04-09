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

namespace CapaPresentacion.caPeriodoTrabajador
{
    /// <summary>
    /// Lógica de interacción para wHorarioSemana.xaml
    /// </summary>
    public partial class wPeriodoTrabajador : Window
    {
        public PeriodoTrabajador miPeriodoTrabajador;

        public wPeriodoTrabajador()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            //datepi.IsChecked = miPeriodoTrabajador.Lunes;
            //chkMartes.IsChecked = miHorarioSemana.Martes;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //miHorarioSemana.Lunes = chkLunes.IsChecked.Value;
            //miHorarioSemana.Martes = chkMartes.IsChecked.Value;
            this.DialogResult = true;
        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
