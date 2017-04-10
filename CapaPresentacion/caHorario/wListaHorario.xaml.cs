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

namespace CapaPresentacion.caHorario
{
    /// <summary>
    /// Interaction logic for wListaHorario.xaml
    /// </summary>
    public partial class wListaHorario : Window
    {
        public Horario miHorario = new Horario();
        CapaDeNegocios.blHorario.blHorario  oblHorario = new CapaDeNegocios.blHorario.blHorario();
        public wListaHorario()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void wListaHorarios_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            lstListaHorarios.ItemsSource = oblHorario.ListarHorarios();
            lstListaHorarios.DisplayMemberPath = "Nombre";
        }

        private void btnAgregarHorario_Click(object sender, RoutedEventArgs e)
        {
            DateTime auxDatetimeEntrada = new DateTime(1, 1, 1, Convert.ToInt16(cboHoraEntrada.Text), Convert.ToInt16(cboMinutosEntrada.Text), Convert.ToInt16(cboSegundosEntrada.Text));
            DateTime auxDatetimeSalida = new DateTime(1, 1, 1, Convert.ToInt16(cboHoraSalida.Text), Convert.ToInt16(cboMinutosSalida.Text), Convert.ToInt16(cboSegundosSalida.Text));
            DateTime auxDatetimeTardanza = new DateTime(1, 1, 1, 0, Convert.ToInt16(cboMinutosTardanza.Text), 0);
            miHorario = new Horario();
            miHorario.Entrada = auxDatetimeEntrada;
            miHorario.Salida = auxDatetimeSalida;
            miHorario.TiempoTardanza = auxDatetimeTardanza;
            miHorario.Nombre = txtNombre.Text;
            if (MessageBox.Show("Desea agregar el horario: " + txtNombre.Text , "Agregar", MessageBoxButton.YesNo ) == MessageBoxResult.Yes)
            {
                oblHorario.AgregarHorario(miHorario);
                Iniciar();
            }
        }

        private void lstListaHorarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstListaHorarios.SelectedItem != null)
            {
                miHorario = (Horario ) lstListaHorarios.SelectedItem;
                txtNombre.Text = miHorario.Nombre;
                cboHoraEntrada.Text = miHorario.Entrada.Hour.ToString("D2");
                cboMinutosEntrada.Text = miHorario.Entrada.Minute.ToString("D2");
                cboSegundosEntrada.Text = miHorario.Entrada.Second.ToString("D2");
                cboHoraSalida.Text = miHorario.Salida.Hour.ToString("D2");
                cboMinutosSalida.Text = miHorario.Salida.Minute.ToString("D2");
                cboSegundosSalida.Text = miHorario.Salida.Second.ToString("D2");
                cboMinutosTardanza.Text = miHorario.TiempoTardanza.Minute.ToString("D2");
                
            }
        }
    }
}
