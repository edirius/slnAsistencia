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

namespace CapaPresentacion.caTipoPermisos
{
    /// <summary>
    /// Lógica de interacción para wTipoPermisos.xaml
    /// </summary>
    public partial class wTipoPermisos : Window
    {
        public TipoPermisos miTipoPermisos;

        public wTipoPermisos()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            txtDescripcion.Text = miTipoPermisos.Nombre;
            chkComputable.IsChecked = miTipoPermisos.Computable;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            miTipoPermisos.Nombre = txtDescripcion.Text;
            miTipoPermisos.Computable = chkComputable.IsChecked.Value;
            this.DialogResult = true;
        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
