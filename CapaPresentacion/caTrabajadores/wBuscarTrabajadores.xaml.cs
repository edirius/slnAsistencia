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

namespace CapaPresentacion.caTrabajadores
{
    /// <summary>
    /// Lógica de interacción para wBuscarTrabajadores.xaml
    /// </summary>
    public partial class wBuscarTrabajadores : Window
    {
        System.Data.DataTable oDataTrabajadores = new System.Data.DataTable();
        public Local miLocal = new Local();
        public Trabajador miTrabajador = new Trabajador();
        CapaDeNegocios.blLocal.blLocal oblLocal = new CapaDeNegocios.blLocal.blLocal();
        CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();

        public wBuscarTrabajadores()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarLocales();
        }

        private void cbolocales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboLocales.DisplayMemberPath != "")
            {
                miLocal.Id = Convert.ToInt32(cboLocales.SelectedValue);
                CargarTrabajadores();
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void dgTrabajadores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //miTrabajador = (Trabajador)dgTrabajadores.SelectedItem;
            try
            {
                System.Data.DataRowView item = dgTrabajadores.CurrentCell.Item as System.Data.DataRowView;
                Trabajador auxTrabajador = new Trabajador();
                auxTrabajador.Id = Convert.ToInt32(item.Row.ItemArray[0]);
                auxTrabajador.Nombre = Convert.ToString(item.Row.ItemArray[1]);
                auxTrabajador.ApellidoPaterno = Convert.ToString(item.Row.ItemArray[2]);
                auxTrabajador.ApellidoMaterno = Convert.ToString(item.Row.ItemArray[3]);
                auxTrabajador.DNI = Convert.ToString(item.Row.ItemArray[4]);
                miTrabajador = auxTrabajador;
            }
            catch (Exception m)
            { }
        }

        private void dgTrabajadores_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
        }

        private void CargarLocales()
        {
            cboLocales.ItemsSource = oblLocal.ListarLocales();
            cboLocales.DisplayMemberPath = "Nombre";
            cboLocales.SelectedValuePath = "Id";
            cboLocales.SelectedIndex = -1;
        }

        private void CargarTrabajadores()
        {
            ICollection<Trabajador> ListaTrabajadores = oblTrabajador.ListaTrabajadores(miLocal);

            oDataTrabajadores = new System.Data.DataTable();
            oDataTrabajadores.Columns.Add("ID", typeof(int));
            oDataTrabajadores.Columns.Add("NOMBRE");
            oDataTrabajadores.Columns.Add("APATERNO");
            oDataTrabajadores.Columns.Add("AMATERNO");
            oDataTrabajadores.Columns.Add("DNI");
            foreach (Trabajador item in ListaTrabajadores)
            {
                var row = oDataTrabajadores.NewRow();
                row["ID"] = item.Id;
                row["NOMBRE"] = item.Nombre;
                row["APATERNO"] = item.ApellidoPaterno;
                row["AMATERNO"] = item.ApellidoMaterno;
                row["DNI"] = item.DNI;
                oDataTrabajadores.Rows.Add(row);
            }
            dgTrabajadores.ItemsSource = oDataTrabajadores.DefaultView;

            if (dgTrabajadores.Items.Count > 0)
            {
                object item = dgTrabajadores.Items[dgTrabajadores.Items.Count - 1];
                dgTrabajadores.SelectedItem = item;
            }
        }
    }
}
