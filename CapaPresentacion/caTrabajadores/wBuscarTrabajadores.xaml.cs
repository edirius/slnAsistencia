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

namespace CapaPresentacion.caTrabajadores
{
    /// <summary>
    /// Lógica de interacción para wBuscarTrabajadores.xaml
    /// </summary>
    public partial class wBuscarTrabajadores : Window
    {
        public Trabajador miTrabajador = new Trabajador();
        CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();

        public wBuscarTrabajadores()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarTrabajadores();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void dgTrabajadores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            miTrabajador = (Trabajador)dgTrabajadores.SelectedItem;
        }

        private void dgTrabajadores_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
        }

        private void CargarTrabajadores()
        {
            ICollection<Trabajador> ListaTrabajadores = oblTrabajador.ListaTrabajadores();
            dgTrabajadores.ItemsSource = ListaTrabajadores;
            if (dgTrabajadores.Items.Count > 0)
            {
                object item = dgTrabajadores.Items[dgTrabajadores.Items.Count - 1];
                dgTrabajadores.SelectedItem = item;
                //dgTrabajadores.ScrollIntoView(item);
                //row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                //break;
            }
        }
    }
}
