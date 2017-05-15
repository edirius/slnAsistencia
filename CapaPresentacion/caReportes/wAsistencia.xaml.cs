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
using CapaEntities;
using CapaDeNegocios;

namespace CapaPresentacion.caReportes
{
    /// <summary>
    /// Lógica de interacción para wAsistencia.xaml
    /// </summary>
    public partial class wAsistencia : Window
    {
        public wAsistencia()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dpInicio.SelectedDate = DateTime.Today;
            CargarTrabajadores();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Trabajador> miListaTrabajadores = new List<Trabajador>();
                for (int i = 0; i < dgTrabajadores.Items.Count; i++)
                {
                    miListaTrabajadores.Add((Trabajador)dgTrabajadores.Items[i]);
                }
                CapaDeNegocios.cblReportes.blReporteAsistencia miReporteAsistencia = new CapaDeNegocios.cblReportes.blReporteAsistencia();
                miReporteAsistencia.miListaTrabajadores = miListaTrabajadores;
                miReporteAsistencia.miFechaInicio = Convert.ToDateTime(dpInicio.Text);
                miReporteAsistencia.miFechaFin = Convert.ToDateTime(dpFin.Text);
                miReporteAsistencia.Iniciar();
            }
            catch (Exception m)
            { }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dpInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dpFin.DisplayDateStart = dpInicio.SelectedDate;
            dpFin.SelectedDate = dpInicio.SelectedDate;
        }

        private void dpFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CargarTrabajadores()
        {
            CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();
            ICollection<Trabajador> ListaTrabajadores = oblTrabajador.ListaTrabajadores();
            dgTrabajadores.ItemsSource = ListaTrabajadores;
            if (dgTrabajadores.Items.Count > 0)
            {
                object item = dgTrabajadores.Items[dgTrabajadores.Items.Count - 1];
                dgTrabajadores.SelectedItem = item;
            }
        }
    }
}
