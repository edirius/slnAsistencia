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

namespace CapaPresentacion.caCronogramaVacaciones
{
    /// <summary>
    /// Lógica de interacción para wListaCronogramaVacaciones.xaml
    /// </summary>
    public partial class wListaCronogramaVacaciones : Window
    {
        public CronogramaVacaciones miCronogramaVacaciones = new CronogramaVacaciones();
        CapaDeNegocios.blCronogramaVacaciones.blCronogramaVacaciones oblCronogramaVacaciones = new CapaDeNegocios.blCronogramaVacaciones.blCronogramaVacaciones();
        CapaDeNegocios.blCronogramaVacaciones.blDetalleCronogramaVacaciones oblDetalleCronogramaVacaciones = new CapaDeNegocios.blCronogramaVacaciones.blDetalleCronogramaVacaciones();

        public wListaCronogramaVacaciones()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarCronogramaVacaciones();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                caCronogramaVacaciones.wCronogramaVacaciones fCronogramaVacaciones = new wCronogramaVacaciones();
                fCronogramaVacaciones.Owner = this.Owner;
                fCronogramaVacaciones.miCronogramaVacaciones = new CronogramaVacaciones();
                fCronogramaVacaciones.miCronogramaVacaciones.Anio = Convert.ToInt16(DateTime.Today.Year);
                if (fCronogramaVacaciones.ShowDialog() == true)
                {
                    miCronogramaVacaciones = fCronogramaVacaciones.miCronogramaVacaciones;
                    oblCronogramaVacaciones.AgregarCronogramaVacaciones(miCronogramaVacaciones);
                }
                CargarCronogramaVacaciones();
            }
            catch
            { }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miCronogramaVacaciones.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN CRONOGRAMA VACACIONAL.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                caCronogramaVacaciones.wCronogramaVacaciones fCronogramaVacaciones = new wCronogramaVacaciones();
                fCronogramaVacaciones.Owner = this.Owner;
                fCronogramaVacaciones.miCronogramaVacaciones = miCronogramaVacaciones;
                if (fCronogramaVacaciones.ShowDialog() == true)
                {
                    miCronogramaVacaciones = fCronogramaVacaciones.miCronogramaVacaciones;
                    oblCronogramaVacaciones.ModificarCronogramaVacaciones(miCronogramaVacaciones);
                }
                CargarCronogramaVacaciones();
            }
            catch
            { }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miCronogramaVacaciones.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN CRONOGRAMA VACACIONAL.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                oblCronogramaVacaciones.EliminarCronogramaVacaciones(miCronogramaVacaciones);
                CargarCronogramaVacaciones();
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
                if (miCronogramaVacaciones.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN CRONOGRAMA VACACIONAL.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                caTrabajadores.wBuscarTrabajadores fTrabajadores = new caTrabajadores.wBuscarTrabajadores();
                if (fTrabajadores.ShowDialog() == true)
                {
                    caCronogramaVacaciones.wDetalleCronogramaVacaciones fDetalleCronogramaVacaciones = new wDetalleCronogramaVacaciones();
                    fDetalleCronogramaVacaciones.Owner = this.Owner;
                    fDetalleCronogramaVacaciones.miDetalleCronogramaVacaciones = new DetalleCronogramaVacaciones();
                    fDetalleCronogramaVacaciones.miDetalleCronogramaVacaciones.Inicio = DateTime.Today;
                    fDetalleCronogramaVacaciones.miDetalleCronogramaVacaciones.Fin = DateTime.Today;
                    if (fDetalleCronogramaVacaciones.ShowDialog() == true)
                    {
                        fDetalleCronogramaVacaciones.miDetalleCronogramaVacaciones.Trabajador = fTrabajadores.miTrabajador;
                        fDetalleCronogramaVacaciones.miDetalleCronogramaVacaciones.CronogramaVacaciones = miCronogramaVacaciones;
                        oblDetalleCronogramaVacaciones.AgregarDetalleCronogramaVacaciones(fDetalleCronogramaVacaciones.miDetalleCronogramaVacaciones);
                    }
                }
                CargarDetalleCronogramaVacaciones();
            }
            catch
            { }
        }

        private void cboAño_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboAño.DisplayMemberPath != "")
            {
                miCronogramaVacaciones = (CronogramaVacaciones)cboAño.SelectedItem;
                CargarDetalleCronogramaVacaciones();
            }
        }

        private void dgCronogramaVacaciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //miCronogramaVacaciones = (CronogramaVacaciones)dgCronogramaVacaciones.SelectedItem;
        }

        private void CargarCronogramaVacaciones()
        {
            cboAño.ItemsSource = oblCronogramaVacaciones.ListarCronogramaVacaciones();
            cboAño.DisplayMemberPath = "Anio";
            cboAño.SelectedValuePath = "Id";
            cboAño.SelectedIndex = -1;
        }

        private void CargarDetalleCronogramaVacaciones()
        {
            ICollection<DetalleCronogramaVacaciones> ListaDetalleCronogramaVacaciones = oblDetalleCronogramaVacaciones.ListarDetalleCronogramaVacaciones(miCronogramaVacaciones);
            dgCronogramaVacaciones.ItemsSource = ListaDetalleCronogramaVacaciones;
        }
    }
}
