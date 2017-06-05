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

namespace CapaPresentacion.caReportes
{
    /// <summary>
    /// Lógica de interacción para wAsistenciaMeses.xaml
    /// </summary>
    public partial class wAsistenciaMeses : Window
    {
        int sAño;
        int sMes;

        public wAsistenciaMeses()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarAños();
            CargarMeses();
            cboMes.Text = DateTime.Today.ToString("MMMM").ToUpper();
            CargarTrabajadores();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Trabajador> miListaTrabajadores = new List<Trabajador>();
                for (int i = 0; i < dgTrabajadores.Items.Count - 1; i++)
                {
                    bool Activo = false;
                    Activo = Convert.ToBoolean((dgTrabajadores.Items[i] as System.Data.DataRowView).Row.ItemArray[5]);
                    if (Activo == true)
                    {
                        Trabajador auxTrabajador = new Trabajador();
                        auxTrabajador.Id = Convert.ToInt32((dgTrabajadores.Items[i] as System.Data.DataRowView).Row.ItemArray[0]);
                        auxTrabajador.Nombre = Convert.ToString((dgTrabajadores.Items[i] as System.Data.DataRowView).Row.ItemArray[1]);
                        auxTrabajador.ApellidoPaterno = Convert.ToString((dgTrabajadores.Items[i] as System.Data.DataRowView).Row.ItemArray[2]);
                        auxTrabajador.ApellidoMaterno = Convert.ToString((dgTrabajadores.Items[i] as System.Data.DataRowView).Row.ItemArray[3]);
                        auxTrabajador.DNI = Convert.ToString((dgTrabajadores.Items[i] as System.Data.DataRowView).Row.ItemArray[4]);
                        miListaTrabajadores.Add(auxTrabajador);
                    }
                }
                CapaDeNegocios.cblReportes.blAsistenciaMeses miReporteAsistenciaMeses = new CapaDeNegocios.cblReportes.blAsistenciaMeses();
                miReporteAsistenciaMeses.Asistencia_Meses(miListaTrabajadores, sAño, sMes);
            }
            catch (Exception m)
            { }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cboAño_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cboAño.DisplayMemberPath != "")
            //{
            sAño = Convert.ToInt32(cboAño.SelectedValue);
            //}
        }

        private void cboMes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboMes.DisplayMemberPath != "")
            {
                sMes = Convert.ToInt32(cboMes.SelectedValue);
            }
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

        private void CargarTrabajadores()
        {
            CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();
            ICollection<Trabajador> ListaTrabajadores = oblTrabajador.ListaTrabajadores();

            System.Data.DataTable oData = new System.Data.DataTable();
            oData.Columns.Add("ID", typeof(int));
            oData.Columns.Add("NOMBRE");
            oData.Columns.Add("A_PATERNO");
            oData.Columns.Add("A_MATERNO");
            oData.Columns.Add("DNI");
            oData.Columns.Add("chk", typeof(bool));
            foreach (Trabajador item in ListaTrabajadores)
            {
                var row = oData.NewRow();
                row["ID"] = item.Id;
                row["NOMBRE"] = item.Nombre;
                row["A_PATERNO"] = item.ApellidoPaterno;
                row["A_MATERNO"] = item.ApellidoMaterno;
                row["DNI"] = item.DNI;
                row["chk"] = false;
                oData.Rows.Add(row);
            }
            dgTrabajadores.ItemsSource = oData.DefaultView;

            if (dgTrabajadores.Items.Count > 0)
            {
                object item = dgTrabajadores.Items[dgTrabajadores.Items.Count - 1];
                dgTrabajadores.SelectedItem = item;
            }
        }
    }
}
