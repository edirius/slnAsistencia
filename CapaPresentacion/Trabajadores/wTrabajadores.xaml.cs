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

namespace CapaPresentacion.Trabajadores
{
    /// <summary>
    /// Lógica de interacción para wTrabajadores.xaml
    /// </summary>
    public partial class wTrabajadores : Window
    {
        public Trabajador miTrabajador;

        public wTrabajadores()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            txtNombres.Text = miTrabajador.Nombre;
            txtAPaterno.Text = miTrabajador.ApellidoPaterno;
            txtAMaterno.Text = miTrabajador.ApellidoPaterno;
            txtDNI.Text = miTrabajador.DNI;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            miTrabajador.Nombre = txtNombres.Text;
            miTrabajador.ApellidoPaterno = txtAPaterno.Text;
            miTrabajador.ApellidoPaterno = txtAMaterno.Text;
            miTrabajador.DNI = txtDNI.Text;
            this.DialogResult = true;
        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
