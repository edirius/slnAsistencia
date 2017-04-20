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

namespace CapaPresentacion.caVacaciones
{
    /// <summary>
    /// Lógica de interacción para wVacaciones.xaml
    /// </summary>
    public partial class wVacaciones : Window
    {
        int sMes;
        public Trabajador miTrabajador = new Trabajador();
        public PeriodoTrabajador miPeriodoTrabajador = new PeriodoTrabajador();
        CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();
        CapaDeNegocios.blPeriodoTrabajador.blPeriodoTrabajador oblPeriodoTrabajador = new CapaDeNegocios.blPeriodoTrabajador.blPeriodoTrabajador();

        public wVacaciones()
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
                    CargarPeriodoTrabajador(miTrabajador);
                }
                CargarRecordAsistencia();
            }
            catch (Exception m)
            { }
        }

        private void cboAño_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //CargarRecordAsistencia();
        }

        private void cboMes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboMes.DisplayMemberPath != "")
            {
                sMes = Convert.ToInt32(cboMes.SelectedValue);
            }
            //CargarRecordAsistencia();
        }
        private void CargarRecordAsistencia()
        {
            try
            {
                CapaDeNegocios.cblAsistenciaAnual.blAsistenciaAnual oblAsistenciaAnual = new CapaDeNegocios.cblAsistenciaAnual.blAsistenciaAnual();
                CapaDeNegocios.cblVacaciones.blVacaciones oblVacaciones = new CapaDeNegocios.cblVacaciones.blVacaciones();
                CapaDeNegocios.cblAsistenciaAnual.cAsistenciaPeriodoTrabajador miAsistenciaPeriodoTrabajador = oblAsistenciaAnual.CalcularAsistenciaPeriodoTrabajador(miTrabajador, miPeriodoTrabajador);
                CapaDeNegocios.cblVacaciones.cVacaciones miVacaciones = oblVacaciones.CalculoDiasAsistencia(miAsistenciaPeriodoTrabajador);

                txtDiasLaborados.Text = miVacaciones.diasLaborados.ToString();
                txtPermisosComputables.Text = miVacaciones.diasPermisosComputables.ToString();
                txtPermisosNoComputables.Text = miVacaciones.diasPermisosNoComputables.ToString();
                txtTotalDiaslaborados.Text = miVacaciones.totalDiasComputables.ToString();
                txtVacacionesAdelantadas.Text = miVacaciones.diasVacacionesAdelantadas.ToString();
                txtVacacionesDisponibles.Text = miVacaciones.diasVacacionesDisponibles.ToString();

                if (sMes == 1)
                {
                    //dgRecordAsistencia.ItemsSource = miAsistenciaAnual.AsistenciaMeses[0].AsistenciaDias;
                    //foreach (CapaDeNegocios.cblAsistenciaAnual.cAsistenciaDia dia in miAsistenciaAnual.AsistenciaMeses[3].AsistenciaDias)
                    //{
                    //    miAsistencia[columna] = dia.asistencia;
                    //    columna += 1;
                    //    //for (int i = 0; i < diasMes; i++)
                    //    //{
                    //    //    auxiliar = fechinicio.AddDays(i);
                    //    //    if (auxiliar.Date == dia.fecha.Date)
                    //    //    {
                    //    //        miAsistencia[0, i] = dia.asistencia;
                    //    //    }
                    //    //}
                    //}
                }
            }
            catch (Exception m)
            { }
        }

        private void CargarPeriodoTrabajador(Trabajador miTrabajador)
        {
            ICollection<PeriodoTrabajador> ListaPeriodoTrabajador = oblPeriodoTrabajador.ListarPeriodoTrabajador(miTrabajador);
            foreach (PeriodoTrabajador name in ListaPeriodoTrabajador)
            {
                miPeriodoTrabajador = name;
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
    }
}
