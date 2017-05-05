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

namespace CapaPresentacion.caVacaciones
{
    /// <summary>
    /// Lógica de interacción para wAPeriodoLaboradoAnteriores.xaml
    /// </summary>
    public partial class wAPeriodoLaboradoAnteriores : Window
    {
        public PeriodoTrabajador miPeriodoTrabajador;
        public AsistenciaPeriodoLaborado miAsistenciaPeriodoLaborado;

        public wAPeriodoLaboradoAnteriores()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarAPeriodoLaborado(miPeriodoTrabajador);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void dgAPeriodoLaborado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            miAsistenciaPeriodoLaborado = (AsistenciaPeriodoLaborado)dgAPeriodoLaborado.SelectedItem;
        }

        private void dgAPeriodoLaborado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CargarAPeriodoLaborado(PeriodoTrabajador miPeriodoTrabajador)
        {
            try
            {
                ICollection<AsistenciaPeriodoLaborado> ListaAsistenciaPeriodoLaborado = miPeriodoTrabajador.AsistenciaPeriodoLaborado.ToList();
                dgAPeriodoLaborado.ItemsSource = ListaAsistenciaPeriodoLaborado;
                if (dgAPeriodoLaborado.Items.Count > 0)
                {
                    object item = dgAPeriodoLaborado.Items[dgAPeriodoLaborado.Items.Count - 1];
                    dgAPeriodoLaborado.SelectedItem = item;
                    //dgTrabajadores.ScrollIntoView(item);
                    //row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    //break;
                }
            }
            catch (Exception m)
            {

            }
        }
    }
}
