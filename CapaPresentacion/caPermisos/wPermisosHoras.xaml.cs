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
    public partial class wPermisosHoras : Window
    {
        public PermisosHoras miPermisoHoras;
        public TipoPermisos miTipoPermisos = new TipoPermisos();
        CapaDeNegocios.blTipoPermisos.blTipoPermisos oblTipoPermisos = new CapaDeNegocios.blTipoPermisos.blTipoPermisos();

        public wPermisosHoras()
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
            dtpFecha.SelectedDate = miPermisoHoras.Fecha;
            cboRHoraInicio.Text = miPermisoHoras.Inicio.Hour.ToString("D2");
            cboRMinutosInicio.Text = miPermisoHoras.Inicio.Minute.ToString("D2");
            cboRSegundosInicio.Text = miPermisoHoras.Inicio.Second.ToString("D2");
            cboRHoraFin.Text = miPermisoHoras.Fin.Hour.ToString("D2");
            cboRMinutosFin.Text = miPermisoHoras.Fin.Minute.ToString("D2");
            cboRSegundosFin.Text = miPermisoHoras.Fin.Second.ToString("D2");
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            miPermisoHoras.Fecha = Convert.ToDateTime(dtpFecha.Text);
            miPermisoHoras.Inicio = new DateTime(1, 1, 1, Convert.ToInt16(cboRHoraInicio.Text), Convert.ToInt16(cboRMinutosInicio.Text), Convert.ToInt16(cboRSegundosInicio.Text));
            miPermisoHoras.Fin = new DateTime(1, 1, 1, Convert.ToInt16(cboRHoraFin.Text), Convert.ToInt16(cboRMinutosFin.Text), Convert.ToInt16(cboRSegundosFin.Text));
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
