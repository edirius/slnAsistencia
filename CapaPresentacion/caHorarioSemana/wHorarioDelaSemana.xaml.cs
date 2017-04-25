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

namespace CapaPresentacion.caHorarioSemana
{
    /// <summary>
    /// Interaction logic for wHorarioDelaSemana.xaml
    /// </summary>
    public partial class wHorarioDelaSemana : Window
    {
        public CapaEntities.HorarioSemana miHorarioSemana;
        CapaDeNegocios.blHorarioDia.blHorarioDia oblHorarioDia = new CapaDeNegocios.blHorarioDia.blHorarioDia();
        public IEnumerable<HorarioDia> miListaHorarioDia;

        public wHorarioDelaSemana()
        {
            InitializeComponent();
        }

        public void Iniciar()
        {
            txtNombres.Text = miHorarioSemana.Nombre;
            //dtgListaDias.ItemsSource = miHorarioSemana.Dia;
            miListaHorarioDia = oblHorarioDia.ListarHorarioDias();
            Dia midia;
            //midia.
            //cboHorariosDias.ItemsSource = miListaHorarioDia;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
