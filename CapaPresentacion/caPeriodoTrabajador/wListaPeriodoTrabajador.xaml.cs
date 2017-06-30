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

namespace CapaPresentacion.caPeriodoTrabajador
{
    /// <summary>
    /// Lógica de interacción para wListaPeriodoTrabajador.xaml
    /// </summary>
    public partial class wListaPeriodoTrabajador : Window
    {
        public Trabajador miTrabajador = new Trabajador();
        public PeriodoTrabajador miPeriodoTrabajador = new PeriodoTrabajador();
        CapaDeNegocios.blPeriodoTrabajador.blPeriodoTrabajador oblPeriodoTrabajador = new CapaDeNegocios.blPeriodoTrabajador.blPeriodoTrabajador();

        public wListaPeriodoTrabajador()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void dgHorarioSemana_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            miPeriodoTrabajador = (PeriodoTrabajador)dgPeriodoTrabajador.SelectedItem;
        }

        private void btnTrabajador_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                caTrabajadores.wBuscarTrabajadores fTrabajadores = new caTrabajadores.wBuscarTrabajadores();
                if (fTrabajadores.ShowDialog() == true)
                {
                    txtTrabajador.Text = fTrabajadores.miTrabajador.Nombre + " " + fTrabajadores.miTrabajador.ApellidoPaterno + " " + fTrabajadores.miTrabajador.ApellidoMaterno;
                    miTrabajador = fTrabajadores.miTrabajador;
                    CargarPeriodoTrabajador();
                }
            }
            catch
            { }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                caPeriodoTrabajador.wPeriodoTrabajador fPeriodoTrabajador = new caPeriodoTrabajador.wPeriodoTrabajador();
                fPeriodoTrabajador.miPeriodoTrabajador = new PeriodoTrabajador();
                fPeriodoTrabajador.miPeriodoTrabajador.Inicio = DateTime.Today;
                fPeriodoTrabajador.miPeriodoTrabajador.Fin = DateTime.Today;
                if (fPeriodoTrabajador.ShowDialog() == true)
                {
                    fPeriodoTrabajador.miPeriodoTrabajador.Trabajador = miTrabajador;
                    fPeriodoTrabajador.miPeriodoTrabajador.Oficina = fPeriodoTrabajador.miOficina;
                    fPeriodoTrabajador.miPeriodoTrabajador.HorarioSemana = fPeriodoTrabajador.miHorarioSemana;
                    oblPeriodoTrabajador.AgregarPeriodoTrabajador(fPeriodoTrabajador.miPeriodoTrabajador);
                }
                CargarPeriodoTrabajador();
            }
            catch (Exception m)
            { }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miPeriodoTrabajador.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN HORARIO DE SEMANA.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                caPeriodoTrabajador.wPeriodoTrabajador fPeriodoTrabajador = new caPeriodoTrabajador.wPeriodoTrabajador();
                fPeriodoTrabajador.miPeriodoTrabajador = miPeriodoTrabajador;
                if (fPeriodoTrabajador.ShowDialog() == true)
                {
                    oblPeriodoTrabajador.ModificarPeriodoTrabajador(fPeriodoTrabajador.miPeriodoTrabajador);
                }
                CargarPeriodoTrabajador();
            }
            catch
            { }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miPeriodoTrabajador.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN HORARIO DE SEMANA.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                oblPeriodoTrabajador.EliminarPeriodoTrabajador(miPeriodoTrabajador);
                CargarPeriodoTrabajador();
            }
            catch
            { }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CargarPeriodoTrabajador()
        {
            ICollection<PeriodoTrabajador> ListaPeriodoTrabajador = oblPeriodoTrabajador.ListarPeriodoTrabajador(miTrabajador);
            dgPeriodoTrabajador.ItemsSource = ListaPeriodoTrabajador;
            if (dgPeriodoTrabajador.Items.Count > 0)
            {
                object item = dgPeriodoTrabajador.Items[dgPeriodoTrabajador.Items.Count - 1];
                dgPeriodoTrabajador.SelectedItem = item;
                //dgTrabajadores.ScrollIntoView(item);
                //row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                //break;
            }
        }
    }
}
