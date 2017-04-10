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

namespace CapaPresentacion.caHorarioDia
{
    /// <summary>
    /// Interaction logic for wListaHorarioDia.xaml
    /// </summary>
    public partial class wListaHorarioDia : Window
    {
        CapaDeNegocios.blHorarioDia.blHorarioDia oblHorarioDia = new CapaDeNegocios.blHorarioDia.blHorarioDia();
        CapaDeNegocios.blHorario.blHorario oblHorario = new CapaDeNegocios.blHorario.blHorario();

        HorarioDia miHorarioDia;

        public wListaHorarioDia()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            lstHorariosDia.ItemsSource = oblHorarioDia.ListarHorarioDias();
            lstHorarios.ItemsSource = oblHorario.ListarHorarios();
            //lstHorarios.DisplayMemberPath = "Nombre";
        }

        private void CheckBox_OnCheck(object sender, RoutedEventArgs e)
        {

        }
    }
}
