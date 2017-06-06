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
using CapaEntities;
using CapaDeNegocios;

namespace CapaPresentacion.caReportes
{
    /// <summary>
    /// Lógica de interacción para wAsistenciaGeneral.xaml
    /// </summary>
    public partial class wAsistenciaGeneral : Window
    {
        System.Data.DataTable oDataTrabajadores = new System.Data.DataTable();

        public wAsistenciaGeneral()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dpInicio.SelectedDate = DateTime.Today;
            CargarTrabajadores();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Trabajador> miListaTrabajadores = new List<Trabajador>();
                foreach (System.Data.DataRowView item in dgTrabajadores.Items)
                {
                    bool Activo = false;
                    Activo = Convert.ToBoolean(item.Row.ItemArray[5]);
                    if (Activo == true)
                    {
                        Trabajador auxTrabajador = new Trabajador();
                        auxTrabajador.Id = Convert.ToInt32(item.Row.ItemArray[0]);
                        auxTrabajador.Nombre = Convert.ToString(item.Row.ItemArray[1]);
                        auxTrabajador.ApellidoPaterno = Convert.ToString(item.Row.ItemArray[2]);
                        auxTrabajador.ApellidoMaterno = Convert.ToString(item.Row.ItemArray[3]);
                        auxTrabajador.DNI = Convert.ToString(item.Row.ItemArray[4]);
                        miListaTrabajadores.Add(auxTrabajador);
                    }
                }
                CapaDeNegocios.cblReportes.blAsistenciaGeneral miReporteAsistencia = new CapaDeNegocios.cblReportes.blAsistenciaGeneral();
                miReporteAsistencia.ReporteAsistencia(miListaTrabajadores, Convert.ToDateTime(dpInicio.Text), Convert.ToDateTime(dpFin.Text));
            }
            catch (Exception m)
            { }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dpInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dpFin.DisplayDateStart = dpInicio.SelectedDate;
            dpFin.SelectedDate = dpInicio.SelectedDate;
        }

        private void dpFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CargarTrabajadores()
        {
            CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();
            ICollection<Trabajador> ListaTrabajadores = oblTrabajador.ListaTrabajadores();

            oDataTrabajadores = new System.Data.DataTable();
            oDataTrabajadores.Columns.Add("ID", typeof(int));
            oDataTrabajadores.Columns.Add("NOMBRE");
            oDataTrabajadores.Columns.Add("APATERNO");
            oDataTrabajadores.Columns.Add("AMATERNO");
            oDataTrabajadores.Columns.Add("DNI");
            oDataTrabajadores.Columns.Add("CHK", typeof(bool));
            foreach (Trabajador item in ListaTrabajadores)
            {
                var row = oDataTrabajadores.NewRow();
                row["ID"] = item.Id;
                row["NOMBRE"] = item.Nombre;
                row["APATERNO"] = item.ApellidoPaterno;
                row["AMATERNO"] = item.ApellidoMaterno;
                row["DNI"] = item.DNI;
                row["CHK"] = false;
                oDataTrabajadores.Rows.Add(row);
            }
            dgTrabajadores.ItemsSource = oDataTrabajadores.DefaultView;

            if (dgTrabajadores.Items.Count > 0)
            {
                object item = dgTrabajadores.Items[dgTrabajadores.Items.Count - 1];
                dgTrabajadores.SelectedItem = item;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (System.Data.DataRow dr in oDataTrabajadores.Rows)
            {
                dr["CHK"] = true;
            }
        }

        private void UnCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (System.Data.DataRow dr in oDataTrabajadores.Rows)
            {
                dr["CHK"] = false;
            }
        }

        private void Chk_Checked(object sender, RoutedEventArgs e)
        {
            int i = dgTrabajadores.SelectedIndex;
            System.Data.DataRow dr = oDataTrabajadores.Rows[i];
            if (Convert.ToBoolean(dr["CHK"]) == false)
            {
                dr["CHK"] = true;
            }
            else
            {
                dr["CHK"] = false;
            }
        }
    }
}
