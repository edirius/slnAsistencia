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


namespace CapaPresentacion.Entidad
{
    /// <summary>
    /// Interaction logic for wListaEntidad.xaml
    /// </summary>
    public partial class wListaEntidad : Window
    {
        public Empresa miEntidad;
        CapaDeNegocios.blLocal.blLocal oblLocal = new CapaDeNegocios.blLocal.blLocal();

        public wListaEntidad()
        {
            InitializeComponent();
            
        }

        public void Iniciar()
        {
            ICollection<Local> ListaLocales = oblLocal.ListarLocales();
            txtNombreEntidad.Text = miEntidad.Nombre;
            
            dtgAgenciasAgrarias.ItemsSource = ListaLocales ;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            AgenciaAgraria.wAgenciaAgraria fAgenciaAgraria = new AgenciaAgraria.wAgenciaAgraria();
            fAgenciaAgraria.miLocal = new Local();
            fAgenciaAgraria.miLocal.Empresa = miEntidad; 
            if (fAgenciaAgraria.ShowDialog() == true)
            {
                oblLocal.AgregarLocal(fAgenciaAgraria.miLocal); 
            }
            
        }

        private void btnModificarAGencia_Click(object sender, RoutedEventArgs e)
        {
            AgenciaAgraria.wAgenciaAgraria fAgenciaAgraria = new AgenciaAgraria.wAgenciaAgraria();
            fAgenciaAgraria.miLocal = (Local) dtgAgenciasAgrarias.SelectedItem ;
            if (fAgenciaAgraria.ShowDialog() == true)
            {
                oblLocal.ModificarLocal(fAgenciaAgraria.miLocal);
            }
        }
    }
}
