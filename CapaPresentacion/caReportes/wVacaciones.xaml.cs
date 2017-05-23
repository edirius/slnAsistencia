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
using System.Data;
using CapaDeNegocios;
using CapaEntities;

namespace CapaPresentacion.caReportes
{
    /// <summary>
    /// Lógica de interacción para wVacaciones.xaml
    /// </summary>
    public partial class wVacaciones : Window
    {
        int sAño;

        public wVacaciones()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarAños();
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
                CapaDeNegocios.cblReportes.blReporteVacaciones miReporteVacaciones = new CapaDeNegocios.cblReportes.blReporteVacaciones();
                miReporteVacaciones.Control_Vacaciones(miListaTrabajadores, sAño);
            }
            catch (Exception m)
            { }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cboAño_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cboAño.DisplayMemberPath != "")
            //{
            sAño = Convert.ToInt32(cboAño.Text);
            //}
        }

        private void CargarAños()
        {
            for (int i = DateTime.Now.Year; i >= 2000; i--)
            {
                cboAño.Items.Add(i);
            }
            cboAño.Text = Convert.ToString(DateTime.Now.Year);
        }

        private void CargarTrabajadores()
        {
            CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();
            ICollection<Trabajador> ListaTrabajadores = oblTrabajador.ListaTrabajadores();
            dgTrabajadores.ItemsSource = ListaTrabajadores;
            dgTrabajadores.Columns[5].Visibility = Visibility.Collapsed;
            dgTrabajadores.Columns[6].Visibility = Visibility.Collapsed;
            dgTrabajadores.Columns[7].Visibility = Visibility.Collapsed;
            dgTrabajadores.Columns[8].Visibility = Visibility.Collapsed;
            //AgregarColumnasDataGrig();
            if (dgTrabajadores.Items.Count > 0)
            {
                object item = dgTrabajadores.Items[dgTrabajadores.Items.Count - 1];
                dgTrabajadores.SelectedItem = item;
            }
        }

        private void AgregarColumnasDataGrig()
        {
            DataGridCheckBoxColumn Check = new DataGridCheckBoxColumn();//creamos un objeto check
            {
                //Check. = "☑";//le damos un nombre de cabecera
                dgTrabajadores.Columns.Add(Check);//agregamos los check a cada items
            }
            dgTrabajadores.Columns[9].Width = 30;
            dgTrabajadores.IsReadOnly = false;
        }
    }
}
