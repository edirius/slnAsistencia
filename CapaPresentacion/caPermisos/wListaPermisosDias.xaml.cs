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
    public partial class wListaPermisosDias : Window
    {
        public PermisosDias miPermisos = new PermisosDias();
        public PeriodoTrabajador miPeriodoTrabajador = new PeriodoTrabajador();
        CapaDeNegocios.blPermisosDias.blPermisosDias oblPermisos = new CapaDeNegocios.blPermisosDias.blPermisosDias();
        CapaDeNegocios.blPeriodoTrabajador.blPeriodoTrabajador oblPeriodoTrabajador = new CapaDeNegocios.blPeriodoTrabajador.blPeriodoTrabajador();

        public wListaPermisosDias()
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
                caPermisos.wPermisosDias fPermisosDias = new wPermisosDias();
                fPermisosDias.miPermiso = new PermisosDias();
                fPermisosDias.miPermiso.Inicio = DateTime.Today;
                fPermisosDias.miPermiso.Fin = DateTime.Today;
                fPermisosDias.Owner = this.Owner;
                if (fPermisosDias.ShowDialog() == true)
                {
                    fPermisosDias.miPermiso.TipoPermisos = fPermisosDias.miTipoPermisos;
                    fPermisosDias.miPermiso.PeriodoTrabajador = miPeriodoTrabajador;
                    oblPermisos.AgregarPermisosDias(fPermisosDias.miPermiso);
                }
                CargarPermisos();
            }
            catch
            { }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miPermisos.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN PERMISO DEL TRABAJADOR.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                caPermisos.wPermisosDias fPermisosDias = new caPermisos.wPermisosDias();
                fPermisosDias.miPermiso = miPermisos;
                fPermisosDias.Owner = this.Owner;
                if (fPermisosDias.ShowDialog() == true)
                {
                    oblPermisos.ModificarPermisosDias(fPermisosDias.miPermiso);
                }
                CargarPermisos();
            }
            catch
            { }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miPermisos.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN PERMISO DEL TRABAJADOR.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                oblPermisos.EliminarPermisosDias(miPermisos);
                CargarPermisos();
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
                CargarPermisos();
            }
            catch
            { }
        }

        private void dgPermisos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            miPermisos = (PermisosDias)dgPermisos.SelectedItem;
        }

        private void CargarPeriodoTrabajador(Trabajador miTrabajador)
        {
            ICollection<PeriodoTrabajador> ListaPeriodoTrabajador = oblPeriodoTrabajador.ListarPeriodoTrabajador(miTrabajador);
            foreach (PeriodoTrabajador item in ListaPeriodoTrabajador)
            {
                miPeriodoTrabajador = item;
            }
        }

        private void CargarPermisos()
        {
            ICollection<PermisosDias> ListaPermisoDias = oblPermisos.ListarPermisosDias();
            dgPermisos.ItemsSource = ListaPermisoDias;
        }
    }
}
