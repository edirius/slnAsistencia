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
        CapaDeNegocios.blHorario.blHorario oblHorario = new CapaDeNegocios.blHorario.blHorario();
        public ICollection<Horario> miListaHorario;

        public wHorarioDelaSemana()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            foreach (Dia item in miHorarioSemana.Dia)
            {
                switch (item.NombreDiaSemana)
                {
                    case "Lunes":
                        item.Horario = (Horario)cboLunes.SelectedItem;
                        break;
                    case "Martes":
                        item.Horario = (Horario)cboMartes.SelectedItem;
                        break;
                    case "Miercoles":
                        item.Horario = (Horario)cboMiercoles.SelectedItem;
                        break;
                    case "Jueves":
                        item.Horario = (Horario)cboJueves.SelectedItem;
                        break;
                    case "Viernes":
                        item.Horario = (Horario)cboViernes.SelectedItem;
                        break;
                    case "Sabado":
                        item.Horario = (Horario)cboSabado.SelectedItem;
                        break;
                    case "Domingo":
                        item.Horario = (Horario)cboDomingo.SelectedItem;
                        break;
                    default:
                        break;
                }
                if (item.Horario.Nombre == "SIN HORARIO")
                {
                    item.Horario = null;
                }
            }
            miHorarioSemana.Nombre = txtNombres.Text;
            DialogResult = true;
        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public void Iniciar()
        {
            txtNombres.Text = miHorarioSemana.Nombre;
            Horario auxHorario = new Horario();
            auxHorario.Nombre = "SIN HORARIO";

            miListaHorario = oblHorario.ListarHorarios();
            miListaHorario.Add(auxHorario);

            cboLunes.ItemsSource = miListaHorario;
            cboLunes.DisplayMemberPath = "Nombre";
            cboMartes.ItemsSource = miListaHorario;
            cboMartes.DisplayMemberPath = "Nombre";
            cboMiercoles.ItemsSource = miListaHorario;
            cboMiercoles.DisplayMemberPath = "Nombre";
            cboJueves.ItemsSource = miListaHorario;
            cboJueves.DisplayMemberPath = "Nombre";
            cboViernes.ItemsSource = miListaHorario;
            cboViernes.DisplayMemberPath = "Nombre";
            cboSabado.ItemsSource = miListaHorario;
            cboSabado.DisplayMemberPath = "Nombre";
            cboDomingo.ItemsSource = miListaHorario;
            cboDomingo.DisplayMemberPath = "Nombre";

            foreach (Dia item in miHorarioSemana.Dia)
            {
                if (item.Horario != null)
                {
                    switch (item.NombreDiaSemana)
                    {
                        case "Lunes":
                            //cboLunes.SelectedItem = item.HorarioDia;
                            cboLunes.Text = item.Horario.Nombre;
                            break;
                        case "Martes":
                            //cboMartes.SelectedItem = item.HorarioDia;
                            cboMartes.Text = item.Horario.Nombre;
                            break;
                        case "Miercoles":
                            //cboMiercoles.SelectedItem =  item.HorarioDia;
                            cboMiercoles.Text = item.Horario.Nombre;
                            break;
                        case "Jueves":
                            //cboJueves.SelectedItem  =  item.HorarioDia;
                            cboJueves.Text = item.Horario.Nombre;
                            break;
                        case "Viernes":
                            //cboViernes.SelectedItem =  item.HorarioDia;
                            cboViernes.Text = item.Horario.Nombre;
                            break;
                        case "Sabado":
                            //cboSabado.SelectedItem =  item.HorarioDia;
                            cboSabado.Text = item.Horario.Nombre;
                            break;
                        case "Domingo":
                            //cboDomingo.SelectedItem =  item.HorarioDia;
                            cboDomingo.Text = item.Horario.Nombre;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (item.NombreDiaSemana)
                    {
                        case "Lunes":
                            //cboLunes.SelectedItem = item.HorarioDia;
                            cboLunes.Text = "SIN HORARIO";
                            break;
                        case "Martes":
                            //cboMartes.SelectedItem = item.HorarioDia;
                            cboMartes.Text = "SIN HORARIO";
                            break;
                        case "Miercoles":
                            //cboMiercoles.SelectedItem =  item.HorarioDia;
                            cboMiercoles.Text = "SIN HORARIO";
                            break;
                        case "Jueves":
                            //cboJueves.SelectedItem  =  item.HorarioDia;
                            cboJueves.Text = "SIN HORARIO";
                            break;
                        case "Viernes":
                            //cboViernes.SelectedItem =  item.HorarioDia;
                            cboViernes.Text = "SIN HORARIO";
                            break;
                        case "Sabado":
                            //cboSabado.SelectedItem =  item.HorarioDia;
                            cboSabado.Text = "SIN HORARIO";
                            break;
                        case "Domingo":
                            //cboDomingo.SelectedItem =  item.HorarioDia;
                            cboDomingo.Text = "SIN HORARIO";
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
