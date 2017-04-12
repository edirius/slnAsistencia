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

namespace CapaPresentacion.caPermisos
{
    /// <summary>
    /// Lógica de interacción para wListaPermisos.xaml
    /// </summary>
    public partial class wListaPermisos : Window
    {
        public PermisosDias miPermisos = new PermisosDias();
        public PeriodoTrabajador miPeriodoTrabajador = new PeriodoTrabajador();
        CapaDeNegocios.blPermisosDias.blPermisosDias oblPermisos = new CapaDeNegocios.blPermisosDias.blPermisosDias();
        CapaDeNegocios.blPeriodoTrabajador.blPeriodoTrabajador oblPeriodoTrabajador = new CapaDeNegocios.blPeriodoTrabajador.blPeriodoTrabajador();

        public wListaPermisos()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (miPeriodoTrabajador.Id == 0)
                //{
                //    MessageBox.Show("EL TRABAJADOR TIENE QUE TENER UN PERIODO ACTIVO", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                //    return;
                //}
                caPermisos.wPermisos fPermisos = new wPermisos();
                fPermisos.miPermiso = new PermisosDias();
                if (fPermisos.ShowDialog() == true)
                {
                    oblPermisos.AgregarPermisosDias(fPermisos.miPermiso);
                }
                CargarPermisos();
            }
            catch
            { }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (miTrabajador.Id == 0)
            //    {
            //        MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN TRABAJADOR.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
            //        return;
            //    }
            //    caTrabajadores.wTrabajadores fTrabajadores = new caTrabajadores.wTrabajadores();
            //    fTrabajadores.miTrabajador = miTrabajador;
            //    if (fTrabajadores.ShowDialog() == true)
            //    {
            //        oblTrabajador.ModificarTrabajador(fTrabajadores.miTrabajador);
            //    }
            //    CargarTrabajadores();
            //}
            //catch
            //{ }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (miTrabajador.Id == 0)
            //    {
            //        MessageBox.Show("TIENE QUE ESTAR SELECCIONADO ALGUN TRABAJADOR.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //    oblTrabajador.EliminarTrabajador(miTrabajador);
            //    CargarTrabajadores();
            //}
            //catch
            //{ }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnTrabajador_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                caPermisos.wBuscarTrabajadores fTrabajadores = new wBuscarTrabajadores();
                if (fTrabajadores.ShowDialog() == true)
                {
                    txtTrabajador.Text = fTrabajadores.miTrabajador.Nombre + " " + fTrabajadores.miTrabajador.ApellidoPaterno + " " + fTrabajadores.miTrabajador.ApellidoMaterno;
                    CargarPeriodoTrabajador(fTrabajadores.miTrabajador);
                }
                CargarPermisos();
            }
            catch
            { }
        }

        private void CargarPeriodoTrabajador(Trabajador miTrabajador)
        {
            ICollection<PeriodoTrabajador> ListaPeriodoTrabajador = oblPeriodoTrabajador.ListarPeriodoTrabajador(miTrabajador);
            //cboOficinas.DisplayMemberPath = "Nombre";
            //cboOficinas.SelectedValuePath = "Id";
            //cboOficinas.SelectedIndex = -1;
        }

        private void CargarPermisos()
        {

        }
    }
}
