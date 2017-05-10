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
    /// Lógica de interacción para wTardanzas.xaml
    /// </summary>
    public partial class wTardanzas : Window
    {
        public wTardanzas()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                miReporteAsistencia.ReporteAsistencia(miListaTrabajadores, Convert.ToDateTime(dpInicio.Text), Convert.ToDateTime(dpFin.Text));
            }
            catch
            { }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
