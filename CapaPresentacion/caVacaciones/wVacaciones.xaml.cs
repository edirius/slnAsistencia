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

                Calendario.SelectedDates.Clear();
                for (int i = 0; i < 12; i++)
                {
                    foreach (CapaDeNegocios.cblAsistenciaAnual.cAsistenciaDia item in miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual[miAsistenciaPeriodoTrabajador.miListaAsistenciaAnual.Count - 1].miListaAsistenciaMeses[i].miListaAsistenciaDias)
                    {
                        DateTime auxiliar = item.fecha;
                        Calendario.SelectedDates.Add(auxiliar.Date);
                    }
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
    }
}
