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
    /// Lógica de interacción para wPermisos.xaml
    /// </summary>
    public partial class wPermisosDias : Window
    {
        public PermisosDias miPermiso;
        public CapaEntities.TipoPermisos miTipoPermisos = new TipoPermisos();
        CapaDeNegocios.blTipoPermisos.blTipoPermisos oblTipoPermisos = new CapaDeNegocios.blTipoPermisos.blTipoPermisos();

        public wPermisosDias()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarTipoPermisos();
            Iniciar();
        }

        private void Iniciar()
        {
            dtpInicio.SelectedDate = miPermiso.Inicio;
            dtpFin.SelectedDate = miPermiso.Fin;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            miPermiso.Inicio = Convert.ToDateTime(dtpInicio.Text);
            miPermiso.Fin = Convert.ToDateTime(dtpFin.Text);
            this.DialogResult = true;
        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cboTipoPermiso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTipoPermiso.DisplayMemberPath != "")
            {
                miTipoPermisos.Id = Convert.ToInt32(cboTipoPermiso.SelectedValue);
            }
        }

        private void CargarTipoPermisos()
        {
            cboTipoPermiso.ItemsSource = oblTipoPermisos.ListarTipoPermisos();
            cboTipoPermiso.DisplayMemberPath = "Nombre";
            cboTipoPermiso.SelectedValuePath = "Id";
            cboTipoPermiso.SelectedIndex = -1;
        }
    }
}
