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

namespace CapaPresentacion.caHorarioSemana
{
    /// <summary>
    /// Lógica de interacción para wHorarioSemana.xaml
    /// </summary>
    public partial class wHorarioSemana : Window
    {
        public CapaEntities.HorarioSemana miHorarioSemana;

        public wHorarioSemana()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            txtNombres.Text = miHorarioSemana.Nombre;
            chkLunes.IsChecked = miHorarioSemana.Lunes;
            chkMartes.IsChecked = miHorarioSemana.Martes;
            chkMiercoles.IsChecked = miHorarioSemana.Miercoles;
            chkJueves.IsChecked = miHorarioSemana.Jueves;
            chkViernes.IsChecked = miHorarioSemana.Viernes;
            chkSabado.IsChecked = miHorarioSemana.Sabado;
            chkDomingo.IsChecked = miHorarioSemana.Domingo;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            miHorarioSemana.Nombre = txtNombres.Text;
            miHorarioSemana.Lunes = chkLunes.IsChecked.Value;
            miHorarioSemana.Martes = chkMartes.IsChecked.Value;
            miHorarioSemana.Miercoles = chkMiercoles.IsChecked.Value;
            miHorarioSemana.Jueves = chkJueves.IsChecked.Value;
            miHorarioSemana.Viernes = chkViernes.IsChecked.Value;
            miHorarioSemana.Sabado = chkSabado.IsChecked.Value;
            miHorarioSemana.Domingo = chkDomingo.IsChecked.Value;
            this.DialogResult = true;
        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
