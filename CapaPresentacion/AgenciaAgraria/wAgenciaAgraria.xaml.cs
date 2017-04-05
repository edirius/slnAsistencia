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

namespace CapaPresentacion.AgenciaAgraria
{
    /// <summary>
    /// Interaction logic for wAgenciaAgraria.xaml
    /// </summary>
    public partial class wAgenciaAgraria : Window
    {
        public Local miLocal;

        public wAgenciaAgraria()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            txtNombre.Text = miLocal.Nombre;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            miLocal.Nombre = txtNombre.Text;
            this.DialogResult = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
