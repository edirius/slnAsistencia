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
        CapaEntities.Asistencia miAsistencia;
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
            
        }
    }
}
