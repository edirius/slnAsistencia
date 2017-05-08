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
        public ICollection<HorarioDia> HorarioDias;
        



        public wHorarioDelaSemana()
        {
            InitializeComponent();
        }

        public void Iniciar()
        {
            txtNombres.Text = miHorarioSemana.Nombre;
            HorarioDia auxHorario = new HorarioDia();
            auxHorario.Nombre = "SIN HORARIO";
            
            HorarioDias = oblHorarioDia.ListarHorarioDias();
            HorarioDias.Add(auxHorario);
         
            cboLunes.ItemsSource = HorarioDias;
            cboLunes.DisplayMemberPath = "Nombre";
            cboMartes.ItemsSource = HorarioDias;
            cboMartes.DisplayMemberPath = "Nombre";
            cboMiercoles.ItemsSource = HorarioDias;
            cboMiercoles.DisplayMemberPath = "Nombre";
            cboJueves.ItemsSource = HorarioDias;
            cboJueves.DisplayMemberPath = "Nombre";
            cboViernes.ItemsSource = HorarioDias;
            cboViernes.DisplayMemberPath = "Nombre";
            cboSabado.ItemsSource = HorarioDias;
            cboSabado.DisplayMemberPath = "Nombre";
            cboDomingo.ItemsSource = HorarioDias;
            cboDomingo.DisplayMemberPath = "Nombre";

            foreach (Dia item in miHorarioSemana.Dia)
            {
                if (item.HorarioDia !=null)
                {
                    switch (item.NombreDiaSemana)
                    {
                        case "Lunes":
                            //cboLunes.SelectedItem = item.HorarioDia;
                            cboLunes.Text = item.HorarioDia.Nombre;
                            break;
                        case "Martes":
                            //cboMartes.SelectedItem = item.HorarioDia;
                            cboMartes.Text = item.HorarioDia.Nombre;
                            break;
                        case "Miercoles":
                            //cboMiercoles.SelectedItem =  item.HorarioDia;
                            cboMiercoles.Text = item.HorarioDia.Nombre;
                            break;
                        case "Jueves":
                            //cboJueves.SelectedItem  =  item.HorarioDia;
                            cboJueves.Text = item.HorarioDia.Nombre;
                            break;
                        case "Viernes":
                            //cboViernes.SelectedItem =  item.HorarioDia;
                            cboViernes.Text = item.HorarioDia.Nombre;
                            break;
                        case "Sabado":
                            //cboSabado.SelectedItem =  item.HorarioDia;
                            cboSabado.Text = item.HorarioDia.Nombre;
                            break;
                        case "Domingo":
                            //cboDomingo.SelectedItem =  item.HorarioDia;
                            cboDomingo.Text = item.HorarioDia.Nombre;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            foreach (Dia item in miHorarioSemana.Dia )
            {
                switch (item.NombreDiaSemana )
                {
                    case "Lunes":
                        item.HorarioDia = (HorarioDia ) cboLunes.SelectedItem;
                        break;
                    case "Martes":
                        item.HorarioDia = (HorarioDia)cboMartes.SelectedItem;
                        break;
                    case "Miercoles":
                        item.HorarioDia = (HorarioDia)cboMiercoles.SelectedItem;
                        break;
                    case "Jueves":
                        item.HorarioDia = (HorarioDia)cboJueves.SelectedItem;
                        break;
                    case "Viernes":
                        item.HorarioDia = (HorarioDia)cboViernes.SelectedItem;
                        break;
                    case "Sabado":
                        item.HorarioDia = (HorarioDia)cboSabado.SelectedItem;
                        break;
                    case "Domingo":
                        item.HorarioDia = (HorarioDia)cboDomingo.SelectedItem;
                        break;
                    
                    default:
                        break;
                }
                if (item.HorarioDia.Nombre == "SIN HORARIO")
                {
                    item.HorarioDia = null;
                }
            }
            miHorarioSemana.Nombre = txtNombres.Text;
            DialogResult = true;
        }

        private void btnCANCELAR_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
