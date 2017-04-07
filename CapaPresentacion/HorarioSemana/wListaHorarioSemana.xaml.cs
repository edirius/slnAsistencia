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
    /// Lógica de interacción para wListaHorarioSemana.xaml
    /// </summary>
    public partial class wListaHorarioSemana : Window
    {
        public CapaEntities.HorarioSemana miTrabajador = new CapaEntities.HorarioSemana();
        CapaDeNegocios.blHorarioSemana.blHorarioSemana oblHorarioSemana = new CapaDeNegocios.blHorarioSemana.blHorarioSemana();

        public wListaHorarioSemana()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarHorarioSemana();
        }

        private void dgHorarioSemana_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            miTrabajador = (CapaEntities.HorarioSemana)dgHorarioSemana.SelectedItem;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (miOficina.Id == 0)
            //    {
            //        MessageBox.Show("TIENE QUE ESTAR SELECCIONADA UN A OFICINA.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
            //        return;
            //    }
            //    Trabajadores.wTrabajadores fTrabajadores = new Trabajadores.wTrabajadores();
            //    fTrabajadores.miTrabajador = new Trabajador();
            //    fTrabajadores.miTrabajador.OficinaActual = miOficina;
            //    if (fTrabajadores.ShowDialog() == true)
            //    {
            //        oblTrabajador.AgregarTrabajador(fTrabajadores.miTrabajador);
            //    }
            //    CargarTrabajadores();
            //}
            //catch
            //{ }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (miTrabajador.Id == 0)
            //    {
            //        MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN TRABAJADOR.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
            //        return;
            //    }
            //    Trabajadores.wTrabajadores fTrabajadores = new Trabajadores.wTrabajadores();
            //    fTrabajadores.miTrabajador = miTrabajador;
            //    if (fTrabajadores.ShowDialog() == true)
            //    {
            //        oblTrabajador.ModificarTrabajador(fTrabajadores.miTrabajador);
            //    }
            //    CargarTrabajadores();
            //}
            //catch
            //{ }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (miTrabajador.Id == 0)
            //    {
            //        MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN TRABAJADOR.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //    oblTrabajador.EliminarTrabajador(miTrabajador);
            //    CargarTrabajadores();
            //}
            //catch
            //{ }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CargarHorarioSemana()
        {
            ICollection<CapaEntities.HorarioSemana> ListaHorarioSemana = oblHorarioSemana.ListarHorarioSemanas();
            dgHorarioSemana.ItemsSource = ListaHorarioSemana;
            //if (dgHorarioSemana.Items.Count > 0)
            //{
            //    object item = dgTrabajadores.Items[dgTrabajadores.Items.Count - 1];
            //    dgTrabajadores.SelectedItem = item;
            //    //dgTrabajadores.ScrollIntoView(item);
            //    //row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            //    //break;
            //}
        }
    }
}
