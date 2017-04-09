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

namespace CapaPresentacion.wImportarAsistencia
{
    /// <summary>
    /// Interaction logic for wImportarAsistencia.xaml
    /// </summary>
    public partial class wImportarAsistencia : Window
    {
        CapaEntities.Trabajador miTrabajador; 
        CapaDeNegocios.blAsistencia.blAsistencia oblAsistencia = new CapaDeNegocios.blAsistencia.blAsistencia();
        CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();
         
        public wImportarAsistencia()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            cboTrabajador.ItemsSource = oblTrabajador.ListaTrabajadores();
            //cboTrabajador.DisplayMemberPath = "Nombre" + "ApellidoPaterno";
        }

        private void btnAgregarAsistencia_Click(object sender, RoutedEventArgs e)
        {
            CapaPresentacion.wImportarAsistencia.wAsistencia fAsistencia = new wAsistencia();
            fAsistencia.miAsistencia = new CapaEntities.Asistencia();
            fAsistencia.miAsistencia.Trabajador = miTrabajador;
            if (fAsistencia.ShowDialog() == true)
            {
                oblAsistencia.AgregarAsistencia(fAsistencia.miAsistencia);
            }
        }

        private void cboTrabajador_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTrabajador.SelectedItem != null)
            {
                dtgListaAsistencia.ItemsSource = oblAsistencia.ListarAsistencias((CapaEntities.Trabajador ) cboTrabajador.SelectedItem);
                miTrabajador = (Trabajador)cboTrabajador.SelectedItem;
            }
        }
    }
}
