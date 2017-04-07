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

namespace CapaPresentacion.Trabajadores
{
    /// <summary>
    /// Lógica de interacción para wListaTrabajadores.xaml
    /// </summary>
    public partial class wListaTrabajadores : Window
    {
        public CapaEntities.Local miLocal = new Local();
        public CapaEntities.Oficina miOficina = new CapaEntities.Oficina();
        CapaDeNegocios.blLocal.blLocal oblLocal = new CapaDeNegocios.blLocal.blLocal();
        CapaDeNegocios.blOficina.blOficina oblOficina = new CapaDeNegocios.blOficina.blOficina();
        CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();

        public wListaTrabajadores()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CargarLocales();
        }

        private void cbolocales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboLocales.DisplayMemberPath != "")
            {
                miLocal.Id = Convert.ToInt32(cboLocales.SelectedValue);
                CargarOficinas();
            }
        }

        private void cbooficinas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboOficinas.DisplayMemberPath != "")
            {
                miOficina.Id = Convert.ToInt32(cboOficinas.SelectedValue);
                CargarTrabajadores();
            }
        }

        private void dgTrabajadores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Trabajadores.wTrabajadores fTrabajadores = new Trabajadores.wTrabajadores();
            fTrabajadores.miTrabajador = new Trabajador();
            fTrabajadores.miTrabajador.Oficina = miOficina;
            if (fTrabajadores.ShowDialog() == true)
            {
                oblTrabajador.AgregarTrabajador(fTrabajadores.miTrabajador);
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Trabajadores.wTrabajadores fTrabajadores = new Trabajadores.wTrabajadores();
            //fAgenciaAgraria.miLocal = (Local)dtgAgenciasAgrarias.SelectedItem;
            if (fTrabajadores.ShowDialog() == true)
            {
                oblTrabajador.AgregarTrabajador(fTrabajadores.miTrabajador);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CargarLocales()
        {
            cboLocales.ItemsSource = oblLocal.ListarLocales();
            cboLocales.DisplayMemberPath = "Nombre";
            cboLocales.SelectedValuePath = "Id";
            cboLocales.SelectedIndex = -1;
        }

        private void CargarOficinas()
        {
            cboOficinas.ItemsSource = oblOficina.ListarOficinas(miLocal);
            cboOficinas.DisplayMemberPath = "Nombre";
            cboOficinas.SelectedValuePath = "Id";
            cboOficinas.SelectedIndex = -1;
        }

        private void CargarTrabajadores()
        {
            ICollection<Trabajador> ListaTrabajadores = oblTrabajador.ListaTrabajadores(miOficina);
            dgTrabajadores.ItemsSource = ListaTrabajadores;
        }
    }
}
