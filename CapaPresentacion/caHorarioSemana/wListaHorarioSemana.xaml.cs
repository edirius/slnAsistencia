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
    /// Lógica de interacción para wListaHorarioSemana.xaml
    /// </summary>
    public partial class wListaHorarioSemana : Window
    {
        public HorarioSemana miHorarioSemana = new HorarioSemana();
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
            miHorarioSemana = (CapaEntities.HorarioSemana)dgHorarioSemana.SelectedItem;
            
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                caHorarioSemana.wHorarioSemana fHorarioSemana = new caHorarioSemana.wHorarioSemana();
                fHorarioSemana.miHorarioSemana = new CapaEntities.HorarioSemana();
                string[] DiasSemana = { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" };

                for (int i = 0; i < 7; i++)
                {
                    Dia auxDia = new Dia();
                    auxDia.HorarioSemana = fHorarioSemana.miHorarioSemana;
                    auxDia.NombreDiaSemana = DiasSemana[i];
                    fHorarioSemana.miHorarioSemana.Dia.Add(auxDia);
                } 
                

                if (fHorarioSemana.ShowDialog() == true)
                {
                    oblHorarioSemana.AgregarHorarioSemana(fHorarioSemana.miHorarioSemana);
                }
                CargarHorarioSemana();
            }
            catch
            { }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miHorarioSemana.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN HORARIO DE SEMANA.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                caHorarioSemana.wHorarioSemana fHorarioSemana = new caHorarioSemana.wHorarioSemana();
                fHorarioSemana.miHorarioSemana = miHorarioSemana;
                if (fHorarioSemana.ShowDialog() == true)
                {
                    oblHorarioSemana.ModificarHorarioSemana(fHorarioSemana.miHorarioSemana);
                }
                CargarHorarioSemana();
            }
            catch
            { }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (miHorarioSemana.Id == 0)
                {
                    MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN HORARIO DE SEMANA.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                oblHorarioSemana.EliminarHorarioSemana(miHorarioSemana);
                CargarHorarioSemana();
            }
            catch
            { }
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
