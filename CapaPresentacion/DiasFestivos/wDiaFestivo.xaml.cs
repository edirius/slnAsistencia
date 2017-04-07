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

namespace CapaPresentacion.DiasFestivos
{
    /// <summary>
    /// Interaction logic for wDiaFestivo.xaml
    /// </summary>
    public partial class wDiaFestivo : Window
    {
        public DiaFestivo miDiaFestivo;

        public wDiaFestivo()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            txtNombre.Text = miDiaFestivo.Nombre;
            calDiaFestivo.SelectedDate = miDiaFestivo.Dia;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            miDiaFestivo.Nombre = txtNombre.Text;
            miDiaFestivo.Dia = Convert.ToDateTime( calDiaFestivo.SelectedDate);
            DialogResult = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
