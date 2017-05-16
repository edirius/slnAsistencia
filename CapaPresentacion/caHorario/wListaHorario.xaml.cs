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
            DateTime auxDatetimeTolerancia = new DateTime(1, 1, 1, 0, Convert.ToInt16(cboMinutosTardanza.Text), 0);
            DateTime auxDatetimeIPEntrada = new DateTime(1, 1, 1, Convert.ToInt16(cboIPHoraEntrada.Text), Convert.ToInt16(cboIPMinutosEntrada.Text), Convert.ToInt16(cboIPSegundosEntrada.Text));
            DateTime auxDatetimeFPEntrada = new DateTime(1, 1, 1, Convert.ToInt16(cboFPHoraEntrada.Text), Convert.ToInt16(cboFPMinutosEntrada.Text), Convert.ToInt16(cboFPSegundosEntrada.Text));
            DateTime auxDatetimeIPSalida= new DateTime(1, 1, 1, Convert.ToInt16(cboIPHoraSalida.Text), Convert.ToInt16(cboIPMinutosSalida.Text), Convert.ToInt16(cboIPSegundosSalida.Text));
            DateTime auxDatetimeFPSalida = new DateTime(1, 1, 1, Convert.ToInt16(cboFPHoraSalida.Text), Convert.ToInt16(cboFPMinutosSalida.Text), Convert.ToInt16(cboFPSegundosSalida.Text));
            miHorario = new Horario();
            miHorario.Nombre = txtNombre.Text;
            miHorario.Entrada = auxDatetimeEntrada;
            miHorario.Salida = auxDatetimeSalida;
            miHorario.Tolerancia = auxDatetimeTolerancia;
            miHorario.InicioPicadoEntrada = auxDatetimeIPEntrada;
            miHorario.FinPicadoEntrada = auxDatetimeFPEntrada;
            miHorario.InicioPicadoSalida = auxDatetimeIPSalida;
            miHorario.FinPicadoSalida = auxDatetimeFPSalida;
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
                cboMinutosTardanza.Text = miHorario.Tolerancia.Minute.ToString("D2");
                cboIPHoraEntrada.Text = miHorario.InicioPicadoEntrada.Hour.ToString("D2");
                cboIPMinutosEntrada.Text = miHorario.InicioPicadoEntrada.Minute.ToString("D2");
                cboIPSegundosEntrada.Text = miHorario.InicioPicadoEntrada.Second.ToString("D2");
                cboFPHoraEntrada.Text = miHorario.FinPicadoEntrada.Hour.ToString("D2");
                cboFPMinutosEntrada.Text = miHorario.FinPicadoEntrada.Minute.ToString("D2");
                cboFPSegundosEntrada.Text = miHorario.FinPicadoEntrada.Second.ToString("D2");
                cboIPHoraSalida.Text = miHorario.InicioPicadoSalida.Hour.ToString("D2");
                cboIPMinutosSalida.Text = miHorario.InicioPicadoSalida.Minute.ToString("D2");
                cboIPSegundosSalida.Text = miHorario.InicioPicadoSalida.Second.ToString("D2");
                cboFPHoraSalida.Text = miHorario.FinPicadoSalida.Hour.ToString("D2");
                cboFPMinutosSalida.Text = miHorario.FinPicadoSalida.Minute.ToString("D2");
                cboFPSegundosSalida.Text = miHorario.FinPicadoSalida.Second.ToString("D2");
            }
        }
    }
}
