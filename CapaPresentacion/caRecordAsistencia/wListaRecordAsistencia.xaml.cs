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
using System.Data;
using CapaDeNegocios;
using CapaEntities;

namespace CapaPresentacion.caRecordAsistencia
{
    /// <summary>
    /// Lógica de interacción para wListaRecordAsistencia.xaml
    /// </summary>
    public partial class wListaRecordAsistencia : Window
    {
        int sMes;
        public Trabajador miTrabajador = new Trabajador();
        CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();

        public wListaRecordAsistencia()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarAños();
            CargarMeses();
            cboMes.Text = DateTime.Today.ToString("MMMM").ToUpper();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnTrabajador_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                caPermisos.wBuscarTrabajadores fTrabajadores = new caPermisos.wBuscarTrabajadores();
                fTrabajadores.Owner = this.Owner;
                if (fTrabajadores.ShowDialog() == true)
                {
                    txtTrabajador.Text = fTrabajadores.miTrabajador.Nombre + " " + fTrabajadores.miTrabajador.ApellidoPaterno + " " + fTrabajadores.miTrabajador.ApellidoMaterno;
                    miTrabajador = fTrabajadores.miTrabajador;
                }
                CargarRecordAsistencia();
            }
            catch (Exception m)
            { }
        }

        private void cboAño_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CargarRecordAsistencia();
        }

        private void cboMes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboMes.DisplayMemberPath != "")
            {
                sMes = Convert.ToInt32(cboMes.SelectedValue);
            }
            CargarRecordAsistencia();
        }

        private void CargarAños()
        {
            for (int i = DateTime.Now.Year; i >= 2000; i--)
            {
                cboAño.Items.Add(i);
            }
            cboAño.Text = Convert.ToString(DateTime.Now.Year);
        }

        private void CargarMeses()
        {
            List<ComboData> dsMeses = new List<ComboData>();
            dsMeses.Add(new ComboData { Id = 1, Nombre = "ENERO" });
            dsMeses.Add(new ComboData { Id = 2, Nombre = "FEBRERO" });
            dsMeses.Add(new ComboData { Id = 3, Nombre = "MARZO" });
            dsMeses.Add(new ComboData { Id = 4, Nombre = "ABRIL" });
            dsMeses.Add(new ComboData { Id = 5, Nombre = "MAYO" });
            dsMeses.Add(new ComboData { Id = 6, Nombre = "JUNIO" });
            dsMeses.Add(new ComboData { Id = 7, Nombre = "JULIO" });
            dsMeses.Add(new ComboData { Id = 8, Nombre = "AGOSTO" });
            dsMeses.Add(new ComboData { Id = 9, Nombre = "SETIEMBRE" });
            dsMeses.Add(new ComboData { Id = 10, Nombre = "OCTUBRE" });
            dsMeses.Add(new ComboData { Id = 11, Nombre = "NOVIEMBRE" });
            dsMeses.Add(new ComboData { Id = 12, Nombre = "DICIEMBRE" });

            cboMes.ItemsSource = dsMeses;
            cboMes.DisplayMemberPath = "Nombre";
            cboMes.SelectedValuePath = "Id";
            cboMes.SelectedIndex = -1;
        }

        public class ComboData
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
        }

        private void CargarRecordAsistencia()
        {
            if (cboMes.Text == "" || cboAño.Text == "" || txtTrabajador.Text == "")
            {
                return;
            }
            else
            {
                DibujarDataGrid();
            }

            DateTime auxiliar;
            DateTime fechinicio = Convert.ToDateTime("01/" + sMes + "/" + cboAño.Text);
            int diasMes = System.DateTime.DaysInMonth(Convert.ToInt32(cboAño.Text), sMes);
            CapaDeNegocios.cblAsistenciaAnual.blAsistenciaAnual miRecordAsistencia = new CapaDeNegocios.cblAsistenciaAnual.blAsistenciaAnual();
            CapaDeNegocios.cblAsistenciaAnual.cAsistenciaAnual miAsistenciaAnual = miRecordAsistencia.LlenarAsistencia(miTrabajador);
            bool[] miAsistencia = new bool[31];
            int columna = 0;
            //if (sMes == 4)
            //{
            //    foreach (CapaDeNegocios.cblAsistenciaAnual.cAsistenciaDia dia in miAsistenciaAnual.AsistenciaMeses[3].AsistenciaDias)
            //    {
            //        miAsistencia[columna] = dia.asistencia;
            //        columna += 1;
            //        //for (int i = 0; i < diasMes; i++)
            //        //{
            //        //    auxiliar = fechinicio.AddDays(i);
            //        //    if (auxiliar.Date == dia.fecha.Date)
            //        //    {
            //        //        miAsistencia[0, i] = dia.asistencia;
            //        //    }
            //        //}
            //    }
            //}
            //dgRecordAsistencia.ItemsSource = new Array(miAsistencia);
        }

        //private void BuscarTodos()
        //{
        //    try
        //    {
        //        dgRecordAsistencia.Items.Clear();
        //        dgRecordAsistencia.Columns.Clear();
        //        //columnas a mostrar:
        //        ArrayList columnas = new ArrayList();
        //        CADTipo usuarioCAD = CADTipo.tipoAcesso();
        //        columnas = ObtenerColumnas();
        //        string nomcolumna = "columna_";
        //        /*for (int i = 0; i < columnas.Count; i++)
        //        {
        //        nomcolumna += i;
        //        dataGridViewUsuarios.Columns.Add(nomcolumna, columnas.ToString()); 
        //        }*/
        //        System.Data.DataTable table = new System.Data.DataTable();
        //        table.Columns.Add("ID");
        //        table.Columns.Add("NIF");
        //        table.Columns.Add("Clave");
        //        table.Columns.Add("Rol");
        //        table.Columns.Add("Nombre");
        //        table.Columns.Add("Telefono");
        //        table.Columns.Add("Email");
        //        table.Columns.Add("Direccion");
        //        table.Columns.Add("Ciudad");
        //        table.Columns.Add("Provincia");
        //        table.Columns.Add("CodigoPostal");
        //        table.Columns.Add("Saldo");
        //        //mostrar contenido
        //        DataSet usuarios = new DataSet();
        //        usuarios = usuarioCAD.ObtenerListadoUsuarios();
        //        if (usuarios.Tables[0].Rows.Count == 0)
        //            MessageBox.Show("No hay ningún usuario en la base de datos.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        else
        //        {
        //            //si hay clientes en la BD, pinto del datagridview
        //            if (usuarios != null && usuarios.Tables[0].Rows.Count > 0)
        //            {
        //                entidadesUsuario = new DTOUsuario[usuarios.Tables[0].Rows.Count];
        //                for (int i = 0; i < usuarios.Tables[0].Rows.Count; i++)
        //                {
        //                    object[] filaDelDataGridView = new object[12]; //numero columnas a mostrar
        //                                                                   //DataRow filaDelDataGridView = table.NewRow();
        //                    filaDelDataGridView[0] = usuarios.Tables[0].Rows.ItemArray[0].ToString();
        //                    filaDelDataGridView[1] = usuarios.Tables[0].Rows.ItemArray[1].ToString();
        //                    filaDelDataGridView[2] = usuarios.Tables[0].Rows.ItemArray[2].ToString();
        //                    filaDelDataGridView[3] = usuarios.Tables[0].Rows.ItemArray[3].ToString();
        //                    filaDelDataGridView[4] = usuarios.Tables[0].Rows.ItemArray[4].ToString();
        //                    filaDelDataGridView[5] = usuarios.Tables[0].Rows.ItemArray[5].ToString();
        //                    filaDelDataGridView[6] = usuarios.Tables[0].Rows.ItemArray[6].ToString();
        //                    filaDelDataGridView[7] = usuarios.Tables[0].Rows.ItemArray[7].ToString();
        //                    filaDelDataGridView[8] = usuarios.Tables[0].Rows.ItemArray[8].ToString();
        //                    filaDelDataGridView[9] = usuarios.Tables[0].Rows.ItemArray[9].ToString();
        //                    filaDelDataGridView[10] = usuarios.Tables[0].Rows.ItemArray[10].ToString();
        //                    filaDelDataGridView[11] = usuarios.Tables[0].Rows.ItemArray[11].ToString();
        //                    //dataGridViewUsuarios.Rows.Add(filaDelDataGridView);
        //                    //table.Rows.Add(filaDelDataGridView);
        //                    dataGridViewUsuarios.ItemsSource = filaDelDataGridView;
        //                }
        //                //dataGridViewUsuarios.Rows[0].Selected = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR. " + ex.Message.ToString());
        //    }
        //}

        private void DibujarDataGrid()
        {
            //dgRecordAsistencia.Columns.Clear();
            //dgRecordAsistencia.Items.Clear();
            DateTime auxiliar;
            DateTime fechinicio = Convert.ToDateTime("01/" + sMes + "/" + cboAño.Text);
            int diasMes = System.DateTime.DaysInMonth(Convert.ToInt32(cboAño.Text), sMes);
            for (int i = 0; i < diasMes; i++)
            {
                auxiliar = fechinicio.AddDays(i);
                DataGridCheckBoxColumn col = new DataGridCheckBoxColumn();
                //col.con = "col" + i.ToString();
                string auxiliarDiaSemana = "";
                switch (auxiliar.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        auxiliarDiaSemana = "L " + auxiliar.Day;
                        break;
                    case DayOfWeek.Tuesday:
                        auxiliarDiaSemana = "M " + auxiliar.Day;
                        break;
                    case DayOfWeek.Wednesday:
                        auxiliarDiaSemana = "M " + auxiliar.Day;
                        break;
                    case DayOfWeek.Friday:
                        auxiliarDiaSemana = "V " + auxiliar.Day;
                        break;
                    case DayOfWeek.Thursday:
                        auxiliarDiaSemana = "J " + auxiliar.Day;
                        break;
                    case DayOfWeek.Saturday:
                        auxiliarDiaSemana = "S " + auxiliar.Day;
                        break;
                    case DayOfWeek.Sunday:
                        auxiliarDiaSemana = "D " + auxiliar.Day;
                        break;
                    default:
                        break;
                }
                col.Header = auxiliarDiaSemana;
                col.Width = 40;
                dgRecordAsistencia.Columns.Add(col);
            }
            //dgRecordAsistencia.Items.Add(1);
            //DataGridViewTextBoxColumn TotalDias = new DataGridViewTextBoxColumn();
            //TotalDias.Name = "txtTotalDias";
            //TotalDias.HeaderText = "Total Dias";
            //TotalDias.Width = 40;
            //dgvDetalleTareo.Columns.Add(TotalDias);
            //DataGridViewTextBoxColumn TotalHoras = new DataGridViewTextBoxColumn();
            //TotalHoras.Name = "txtTotalHoras";
            //TotalHoras.HeaderText = "Total Horas";
            //TotalHoras.Width = 40;
            //dgvDetalleTareo.Columns.Add(TotalHoras);
        }
    }
}
