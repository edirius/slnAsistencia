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
    /// Interaction logic for wHorarioDelaSemana.xaml
    /// </summary>
    public partial class wHorarioDelaSemana : Window
    {
        public CapaEntities.HorarioSemana miHorarioSemana;
        CapaDeNegocios.blHorarioDia.blHorarioDia oblHorarioDia = new CapaDeNegocios.blHorarioDia.blHorarioDia();
        public ICollection<HorarioDia> HorarioDiaLunes;
        public ICollection<HorarioDia> HorarioDiaMartes;
        public ICollection<HorarioDia> HorarioDiaMiercoles;
        public ICollection<HorarioDia> HorarioDiaJueves;
        public ICollection<HorarioDia> HorarioDiaViernes;
        public ICollection<HorarioDia> HorarioDiaSabado;
        public ICollection<HorarioDia> HorarioDiaDomingo;



        public wHorarioDelaSemana()
        {
            InitializeComponent();
        }

        public void Iniciar()
        {
            txtNombres.Text = miHorarioSemana.Nombre;
            HorarioDia auxHorario = new HorarioDia();
            auxHorario.Nombre = "SIN HORARIO";
            
            HorarioDiaLunes = oblHorarioDia.ListarHorarioDias();
            HorarioDiaLunes.Add(auxHorario);
            HorarioDiaMartes = oblHorarioDia.ListarHorarioDias();
            HorarioDiaMartes.Add(auxHorario);
            HorarioDiaMiercoles = oblHorarioDia.ListarHorarioDias();
            HorarioDiaMiercoles.Add(auxHorario);
            HorarioDiaJueves = oblHorarioDia.ListarHorarioDias();
            HorarioDiaJueves.Add(auxHorario);
            HorarioDiaViernes = oblHorarioDia.ListarHorarioDias();
            HorarioDiaViernes.Add(auxHorario);
            HorarioDiaSabado = oblHorarioDia.ListarHorarioDias();
            HorarioDiaSabado.Add(auxHorario);
            HorarioDiaDomingo = oblHorarioDia.ListarHorarioDias();
            HorarioDiaDomingo.Add(auxHorario);
            cboLunes.ItemsSource = HorarioDiaLunes;
            cboLunes.DisplayMemberPath = "Nombre";
            cboMartes.ItemsSource = HorarioDiaMartes;
            cboMartes.DisplayMemberPath = "Nombre";
            cboMiercoles.ItemsSource = HorarioDiaMiercoles;
            cboMiercoles.DisplayMemberPath = "Nombre";
            cboJueves.ItemsSource = HorarioDiaMiercoles;
            cboJueves.DisplayMemberPath = "Nombre";
            cboViernes.ItemsSource = HorarioDiaJueves;
            cboViernes.DisplayMemberPath = "Nombre";
            cboSabado.ItemsSource = HorarioDiaViernes;
            cboSabado.DisplayMemberPath = "Nombre";
            cboDomingo.ItemsSource = HorarioDiaSabado;
            cboDomingo.DisplayMemberPath = "Nombre";
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
