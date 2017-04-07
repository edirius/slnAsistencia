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

namespace CapaPresentacion.HorarioSemana
{
    /// <summary>
    /// Lógica de interacción para wHorarioSemana.xaml
    /// </summary>
    public partial class wHorarioSemana : Window
    {
        public Trabajador miHorarioSemana;

        public wHorarioSemana()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            //txtNombres.Text = miTrabajador.Nombre;
            //txtAPaterno.Text = miTrabajador.ApellidoPaterno;
            //txtAMaterno.Text = miTrabajador.ApellidoPaterno;
            //txtDNI.Text = miTrabajador.DNI;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //miTrabajador.Nombre = txtNombres.Text;
            //miTrabajador.ApellidoPaterno = txtAPaterno.Text;
            //miTrabajador.ApellidoMaterno = txtAMaterno.Text;
            //miTrabajador.DNI = txtDNI.Text;
            //this.DialogResult = true;
        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
