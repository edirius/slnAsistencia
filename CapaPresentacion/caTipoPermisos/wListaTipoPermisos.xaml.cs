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

namespace CapaPresentacion.caTipoPermisos
{
    /// <summary>
    /// Lógica de interacción para wListaTipoPermisos.xaml
    /// </summary>
    public partial class wListaTipoPermisos : Window
    {
        public CapaEntities.TipoPermisos miTipoPermisos = new TipoPermisos();
        CapaDeNegocios.blTipoPermisos.blTipoPermisos oblTipoPermisos = new CapaDeNegocios.blTipoPermisos.blTipoPermisos();

        public wListaTipoPermisos()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarTipoPermisos();
        }

        private void dgTipoPermisos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            miTipoPermisos = (TipoPermisos)dgTipoPermisos.SelectedItem;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (miTipoPermisos.Id == 0)
                //{
                //    MessageBox.Show("TIENE QUE ESTAR SELECCIONADA UN A OFICINA.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                //    return;
                //}
                caTipoPermisos.wTipoPermisos fTipoPermisos = new caTipoPermisos.wTipoPermisos();
                fTipoPermisos.miTipoPermisos = new TipoPermisos();
                if (fTipoPermisos.ShowDialog() == true)
                {
                    oblTipoPermisos.AgregarTipoPermisos(fTipoPermisos.miTipoPermisos);
                }
                CargarTipoPermisos();
            }
            catch (Exception m)
            { }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miTipoPermisos.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN TIPO DE PERMISO.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                caTipoPermisos.wTipoPermisos fTipoPermisos = new caTipoPermisos.wTipoPermisos();
                fTipoPermisos.miTipoPermisos = miTipoPermisos;
                if (fTipoPermisos.ShowDialog() == true)
                {
                    oblTipoPermisos.ModificarTipoPermisos(fTipoPermisos.miTipoPermisos);
                }
                CargarTipoPermisos();
            }
            catch
            { }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miTipoPermisos.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN TIPO DE PERMISO.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                oblTipoPermisos.EliminarTipoPermisos(miTipoPermisos);
                CargarTipoPermisos();
            }
            catch
            { }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CargarTipoPermisos()
        {
            ICollection<TipoPermisos> ListaTipoPermisos = oblTipoPermisos.ListarTipoPermisos();
            dgTipoPermisos.ItemsSource = ListaTipoPermisos;
            if (dgTipoPermisos.Items.Count > 0)
            {
                object item = dgTipoPermisos.Items[dgTipoPermisos.Items.Count - 1];
                dgTipoPermisos.SelectedItem = item;
                //dgTrabajadores.ScrollIntoView(item);
                //row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                //break;
            }
        }
    }
}
