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
    /// Lógica de interacción para wAcuTardanzasMeses.xaml
    /// </summary>
    public partial class wAcuTardanzasMeses : Window
    {
        int sAño;
        int sMes;
        System.Data.DataTable oDataTrabajadores = new System.Data.DataTable();

        public wAcuTardanzasMeses()
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
                CapaDeNegocios.cblReportes.blAcuTardanzasMeses miReporteAcuTardanzasMeses = new CapaDeNegocios.cblReportes.blAcuTardanzasMeses();
                miReporteAcuTardanzasMeses.Asistencia_Meses(miListaTrabajadores, sAño, sMes);
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
