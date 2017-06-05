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

        HorarioDia miHorarioDia = new HorarioDia();

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
            lstHorariosDia.DisplayMemberPath = "Nombre";
            lstHorarios.ItemsSource = oblHorario.ListarHorarios();
        }

        private ListViewItem GetNodetItem(DependencyObject obj)
        {
            if (obj != null)
            {
                DependencyObject parent = VisualTreeHelper.GetParent((DependencyObject)obj);

                ListViewItem project = parent as ListViewItem;

                return (project != null) ? project : GetNodetItem(parent);
            }
            return null;
        }

        private void CheckBox_OnCheck(object sender, RoutedEventArgs e)
        {
            Horario auxHorario;
            ListViewItem item = GetNodetItem((DependencyObject)sender);
            auxHorario = (Horario)item.DataContext;
            miHorarioDia.Horario.Add(auxHorario);
            MessageBox.Show(auxHorario.Nombre );
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea Agregar el el Horario: " + txtNombre.Text, "Agregar", MessageBoxButton.YesNo) == MessageBoxResult.Yes ) 
            {
                miHorarioDia.Nombre = txtNombre.Text;
                oblHorarioDia.AgregarHorarioDia(miHorarioDia);
            }
        }

        private void lstHorariosDia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HorarioDia auxHorarioDia = new HorarioDia();
            if (lstHorariosDia.SelectedItem != null)
            {
                auxHorarioDia = (HorarioDia)lstHorariosDia.SelectedItem;
                txtNombre.Text = auxHorarioDia.Nombre;
            }
        }
    }
}
