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
using System.Data;
using CapaDeNegocios;
using CapaEntities;

namespace CapaPresentacion.caPermisos
{
    /// <summary>
    /// Lógica de interacción para wListaPermisos.xaml
    /// </summary>
    public partial class wListaPermisosHoras : Window
    {
        public PermisosHoras miPermisosHoras = new PermisosHoras();
        public PeriodoTrabajador miPeriodoTrabajador = new PeriodoTrabajador();
        CapaDeNegocios.blPermisosHoras.blPermisosHoras oblPermisosHoras = new CapaDeNegocios.blPermisosHoras.blPermisosHoras();
        CapaDeNegocios.blPeriodoTrabajador.blPeriodoTrabajador oblPeriodoTrabajador = new CapaDeNegocios.blPeriodoTrabajador.blPeriodoTrabajador();

        public wListaPermisosHoras()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (miPeriodoTrabajador.Id == 0)
                //{
                //    MessageBox.Show("EL TRABAJADOR TIENE QUE TENER UN PERIODO ACTIVO", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                //    return;
                //}
                caPermisos.wPermisosHoras fPermisosHoras = new wPermisosHoras();
                fPermisosHoras.miPermisoHoras = new PermisosHoras();
                fPermisosHoras.miPermisoHoras.Fecha = DateTime.Today;
                fPermisosHoras.Owner = this.Owner;
                if (fPermisosHoras.ShowDialog() == true)
                {
                    fPermisosHoras.miPermisoHoras.TipoPermisos = fPermisosHoras.miTipoPermisos;
                    fPermisosHoras.miPermisoHoras.PeriodoTrabajador = miPeriodoTrabajador;
                    oblPermisosHoras.AgregarPermisosHoras(fPermisosHoras.miPermisoHoras);
                }
                CargarPermisosHoras();
            }
            catch
            { }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miPermisosHoras.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN PERMISO DEL TRABAJADOR.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                caPermisos.wPermisosHoras fPermisosHoras = new caPermisos.wPermisosHoras();
                fPermisosHoras.miPermisoHoras = miPermisosHoras;
                fPermisosHoras.Owner = this.Owner;
                if (fPermisosHoras.ShowDialog() == true)
                {
                    oblPermisosHoras.ModificarPermisosHoras(fPermisosHoras.miPermisoHoras);
                }
                CargarPermisosHoras();
            }
            catch
            { }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miPermisosHoras.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN PERMISO DEL TRABAJADOR.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                oblPermisosHoras.EliminarPermisosHoras(miPermisosHoras);
                CargarPermisosHoras();
            }
            catch
            { }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnTrabajador_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                caTrabajadores.wBuscarTrabajadores fTrabajadores = new caTrabajadores.wBuscarTrabajadores();
                if (fTrabajadores.ShowDialog() == true)
                {
                    txtTrabajador.Text = fTrabajadores.miTrabajador.Nombre + " " + fTrabajadores.miTrabajador.ApellidoPaterno + " " + fTrabajadores.miTrabajador.ApellidoMaterno;
                    CargarPeriodoTrabajador(fTrabajadores.miTrabajador);
                }
                CargarPermisosHoras();
            }
            catch
            { }
        }

        private void dgPermisos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            miPermisosHoras = (PermisosHoras)dgPermisos.SelectedItem;
        }

        private void CargarPeriodoTrabajador(Trabajador miTrabajador)
        {
            ICollection<PeriodoTrabajador> ListaPeriodoTrabajador = oblPeriodoTrabajador.ListarPeriodoTrabajador(miTrabajador);
            foreach (PeriodoTrabajador item in ListaPeriodoTrabajador)
            {
                miPeriodoTrabajador = item;
            }
        }

        private void CargarPermisosHoras()
        {
            ICollection<PermisosHoras> ListaPermisosHoras = oblPermisosHoras.ListarPermisosHoras();
            dgPermisos.ItemsSource = ListaPermisosHoras;
        }
    }
}
