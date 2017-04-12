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
    /// Lógica de interacción para wListaTrabajadores.xaml
    /// </summary>
    public partial class wListaTrabajadores : Window
    {
        public Local miLocal = new Local();
        public CapaEntities.Oficina miOficina = new CapaEntities.Oficina();
        public Trabajador miTrabajador = new Trabajador();
        CapaDeNegocios.blLocal.blLocal oblLocal = new CapaDeNegocios.blLocal.blLocal();
        CapaDeNegocios.blOficina.blOficina oblOficina = new CapaDeNegocios.blOficina.blOficina();
        CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();

        public wListaTrabajadores()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CargarLocales();
        }

        private void cbolocales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboLocales.DisplayMemberPath != "")
            {
                miLocal.Id = Convert.ToInt32(cboLocales.SelectedValue);
                CargarOficinas();
            }
        }

        private void cbooficinas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboOficinas.DisplayMemberPath != "")
            {
                miOficina.Id = Convert.ToInt32(cboOficinas.SelectedValue);
                CargarTrabajadores();
            }
        }

        private void dgTrabajadores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            miTrabajador = (Trabajador)dgTrabajadores.SelectedItem;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miOficina.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADA UN A OFICINA.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                caTrabajadores.wTrabajadores fTrabajadores = new caTrabajadores.wTrabajadores();
                fTrabajadores.miTrabajador = new Trabajador();
                fTrabajadores.miTrabajador.OficinaActual = miOficina;
                if (fTrabajadores.ShowDialog() == true)
                {
                    oblTrabajador.AgregarTrabajador(fTrabajadores.miTrabajador);
                }
                CargarTrabajadores();
            }
            catch
            { }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miTrabajador.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN TRABAJADOR.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                caTrabajadores.wTrabajadores fTrabajadores = new caTrabajadores.wTrabajadores();
                fTrabajadores.miTrabajador = miTrabajador;
                if (fTrabajadores.ShowDialog() == true)
                {
                    oblTrabajador.ModificarTrabajador(fTrabajadores.miTrabajador);
                }
                CargarTrabajadores();
            }
            catch
            { }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miTrabajador.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN TRABAJADOR.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                oblTrabajador.EliminarTrabajador(miTrabajador);
                CargarTrabajadores();
            }
            catch
            { }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnPeriodo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miTrabajador.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN TRABAJADOR.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                caPeriodoTrabajador.wListaPeriodoTrabajador fPeriodoTrabajador = new caPeriodoTrabajador.wListaPeriodoTrabajador();
                fPeriodoTrabajador.txtTrabajador.Text = miTrabajador.Nombre + ' ' + miTrabajador.ApellidoPaterno + ' ' + miTrabajador.ApellidoMaterno;
                fPeriodoTrabajador.miTrabajador = miTrabajador;
                if (fPeriodoTrabajador.ShowDialog() == true)
                {
                    //oblTrabajador.ModificarTrabajador(fPeriodoTrabajador.miTrabajador);
                }
                CargarTrabajadores();
            }
            catch
            { }
        }

        private void CargarLocales()
        {
            cboLocales.ItemsSource = oblLocal.ListarLocales();
            cboLocales.DisplayMemberPath = "Nombre";
            cboLocales.SelectedValuePath = "Id";
            cboLocales.SelectedIndex = -1;
        }

        private void CargarOficinas()
        {
            cboOficinas.ItemsSource = oblOficina.ListarOficinas(miLocal);
            cboOficinas.DisplayMemberPath = "Nombre";
            cboOficinas.SelectedValuePath = "Id";
            cboOficinas.SelectedIndex = -1;
        }

        private void CargarTrabajadores()
        {
            ICollection<Trabajador> ListaTrabajadores = oblTrabajador.ListaTrabajadores(miOficina);
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
