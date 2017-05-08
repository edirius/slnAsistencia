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
    /// Lógica de interacción para wCronogramaVacaciones.xaml
    /// </summary>
    public partial class wCronogramaVacaciones : Window
    {
        public CronogramaVacaciones miCronogramaVacaciones;

        public wCronogramaVacaciones()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarAños();
            Iniciar();
        }

        private void Iniciar()
        {
            cboAño.Text = miCronogramaVacaciones.Anio.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            miCronogramaVacaciones.Anio = Convert.ToInt16(cboAño.Text);
            this.DialogResult = true;
        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void CargarAños()
        {
            for (int i = DateTime.Now.Year; i >= 2000; i--)
            {
                cboAño.Items.Add(i);
            }
        }
    }
}
