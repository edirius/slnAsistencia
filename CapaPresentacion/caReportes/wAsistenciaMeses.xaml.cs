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
    /// Lógica de interacción para wAsistenciaMeses.xaml
    /// </summary>
    public partial class wAsistenciaMeses : Window
    {
        int sAño;
        int sMes;

        public wAsistenciaMeses()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarAños();
            CargarMeses();
            cboMes.Text = DateTime.Today.ToString("MMMM").ToUpper();
            CargarTrabajadores();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Trabajador> miListaTrabajadores = new List<Trabajador>();
                for (int i = 0; i < dgTrabajadores.Items.Count; i++)
                {
                    //bool estado = false;
                    //string miFechaPicado = Convert.ToString((dgTrabajadores.Items[i] as System.Data.DataRowView).Row.ItemArray[9]); ;
                    //var estado = (dgTrabajadores.Items[i] as System.Data.DataRowView).Row.ItemArray[0];

                    miListaTrabajadores.Add((Trabajador)dgTrabajadores.Items[i]);
                }
                CapaDeNegocios.cblReportes.blReporteAsistenciaMeses miReporteAsistenciaMeses = new CapaDeNegocios.cblReportes.blReporteAsistenciaMeses();
                miReporteAsistenciaMeses.Asistencia_Meses(miListaTrabajadores, sAño, sMes);
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

        private void cboMes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboMes.DisplayMemberPath != "")
            {
                sMes = Convert.ToInt32(cboMes.SelectedValue);
            }
        }

        private void CargarAños()
        {
            for (int i = DateTime.Now.Year; i >= 2000; i--)
            {
                cboAño.Items.Add(i);
            }
            cboAño.Text = Convert.ToString(DateTime.Now.Year);
        }

        private void CargarMeses()
        {
            List<ComboData> dsMeses = new List<ComboData>();
            dsMeses.Add(new ComboData { Id = 1, Nombre = "ENERO" });
            dsMeses.Add(new ComboData { Id = 2, Nombre = "FEBRERO" });
            dsMeses.Add(new ComboData { Id = 3, Nombre = "MARZO" });
            dsMeses.Add(new ComboData { Id = 4, Nombre = "ABRIL" });
            dsMeses.Add(new ComboData { Id = 5, Nombre = "MAYO" });
            dsMeses.Add(new ComboData { Id = 6, Nombre = "JUNIO" });
            dsMeses.Add(new ComboData { Id = 7, Nombre = "JULIO" });
            dsMeses.Add(new ComboData { Id = 8, Nombre = "AGOSTO" });
            dsMeses.Add(new ComboData { Id = 9, Nombre = "SETIEMBRE" });
            dsMeses.Add(new ComboData { Id = 10, Nombre = "OCTUBRE" });
            dsMeses.Add(new ComboData { Id = 11, Nombre = "NOVIEMBRE" });
            dsMeses.Add(new ComboData { Id = 12, Nombre = "DICIEMBRE" });

            cboMes.ItemsSource = dsMeses;
            cboMes.DisplayMemberPath = "Nombre";
            cboMes.SelectedValuePath = "Id";
            cboMes.SelectedIndex = -1;
        }

        public class ComboData
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
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
