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

namespace CapaPresentacion.caPeriodoTrabajador
{
    /// <summary>
    /// Lógica de interacción para wHorarioSemana.xaml
    /// </summary>
    public partial class wPeriodoTrabajador : Window
    {
        public PeriodoTrabajador miPeriodoTrabajador;
        public CapaEntities.Oficina miOficina = new CapaEntities.Oficina();
        public CapaEntities.HorarioSemana miHorarioSemana = new HorarioSemana();
        CapaDeNegocios.blOficina.blOficina oblOficina = new CapaDeNegocios.blOficina.blOficina();
        CapaDeNegocios.blHorarioSemana.blHorarioSemana oblHorarioSemana = new CapaDeNegocios.blHorarioSemana.blHorarioSemana();

        public wPeriodoTrabajador()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarOficinas();
            CargarHorarioSemana();
            Iniciar();
        }

        private void Iniciar()
        {
            dtpInicio.SelectedDate = miPeriodoTrabajador.Inicio;
            dtpFin.SelectedDate = miPeriodoTrabajador.Fin;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            miPeriodoTrabajador.Inicio = dtpInicio.DisplayDate;
            miPeriodoTrabajador.Fin = dtpFin.DisplayDate;
            this.DialogResult = true;
        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cboOficinas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboOficinas.DisplayMemberPath != "")
            {
                miOficina.Id = Convert.ToInt32(cboOficinas.SelectedValue);
            }
        }

        private void cboHorarioSemana_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboHorarioSemana.DisplayMemberPath != "")
            {
                miHorarioSemana.Id = Convert.ToInt32(cboHorarioSemana.SelectedValue);
            }
        }

        private void CargarHorarioSemana()
        {
            cboHorarioSemana.ItemsSource = oblHorarioSemana.ListarHorarioSemanas();
            cboHorarioSemana.DisplayMemberPath = "Nombre";
            cboHorarioSemana.SelectedValuePath = "Id";
            cboHorarioSemana.SelectedIndex = -1;
        }

        private void CargarOficinas()
        {
            cboOficinas.ItemsSource = oblOficina.ListarOficinas();
            cboOficinas.DisplayMemberPath = "Nombre";
            cboOficinas.SelectedValuePath = "Id";
            cboOficinas.SelectedIndex = -1;
        }
    }
}
