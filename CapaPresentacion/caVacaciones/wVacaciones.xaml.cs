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
        public PeriodoTrabajador miPeriodoTrabajador = new PeriodoTrabajador();
        public AsistenciaPeriodoLaborado miAsistenciaPeriodoLaborado = new AsistenciaPeriodoLaborado();
        public Vacaciones miVacaciones = new Vacaciones();
        CapaDeNegocios.blPeriodoTrabajador.blPeriodoTrabajador oblPeriodoTrabajador = new CapaDeNegocios.blPeriodoTrabajador.blPeriodoTrabajador();
        CapaDeNegocios.blAsistenciaPeriodoLaborado.blAsistenciaPeriodoLaborado oblAsistenciaPeriodoLaborado = new CapaDeNegocios.blAsistenciaPeriodoLaborado.blAsistenciaPeriodoLaborado();
        CapaDeNegocios.blVacaciones.blVacaciones oblVacaciones = new CapaDeNegocios.blVacaciones.blVacaciones();

        CapaDeNegocios.cblAsistenciaAnual.blAsistenciaAnual oblAsistenciaAnual = new CapaDeNegocios.cblAsistenciaAnual.blAsistenciaAnual();
        CapaDeNegocios.cblAsistenciaAnual.cAsistenciaPeriodoLaborado micAsistenciaPeriodoLaborado = new CapaDeNegocios.cblAsistenciaAnual.cAsistenciaPeriodoLaborado();

        public wVacaciones()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnTrabajador_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LimpiarBotones();
                miPeriodoTrabajador = new PeriodoTrabajador();
                caPermisos.wBuscarTrabajadores fTrabajadores = new caPermisos.wBuscarTrabajadores();
                fTrabajadores.Owner = this.Owner;
                if (fTrabajadores.ShowDialog() == true)
                {
                    txtTrabajador.Text = fTrabajadores.miTrabajador.Nombre + " " + fTrabajadores.miTrabajador.ApellidoPaterno + " " + fTrabajadores.miTrabajador.ApellidoMaterno;
                    CargarPeriodoTrabajador(fTrabajadores.miTrabajador);
                }
                CargarRecordAsistencia();
            }
            catch (Exception m)
            { }
        }

        private void btnPeriodoLaboradoAnterior_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LimpiarBotones();
                btnAsignarVacaciones.IsEnabled = false;
                if (miPeriodoTrabajador.Trabajador == null)
                {
                    MessageBox.Show("El Trabajador debe tener un periodo activo.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                caVacaciones.wAPeriodoLaboradoAnteriores fAPeriodoLaboradoAnteriores = new caVacaciones.wAPeriodoLaboradoAnteriores();
                fAPeriodoLaboradoAnteriores.Owner = this.Owner;
                fAPeriodoLaboradoAnteriores.miPeriodoTrabajador = miPeriodoTrabajador;
                if (fAPeriodoLaboradoAnteriores.ShowDialog() == true)
                {
                    dtpInicio.SelectedDate = fAPeriodoLaboradoAnteriores.miAsistenciaPeriodoLaborado.Inicio;
                    dtpFin.SelectedDate = fAPeriodoLaboradoAnteriores.miAsistenciaPeriodoLaborado.Fin;
                    Calendario.DisplayDateStart = dtpInicio.DisplayDate;
                    Calendario.DisplayDateEnd = dtpFin.DisplayDate;
                    txtDiasLaborados.Text = fAPeriodoLaboradoAnteriores.miAsistenciaPeriodoLaborado.DiasLaborados.ToString();
                    txtPermisosComputables.Text = fAPeriodoLaboradoAnteriores.miAsistenciaPeriodoLaborado.DiasPermisosComputables.ToString();
                    txtPermisosNoComputables.Text = fAPeriodoLaboradoAnteriores.miAsistenciaPeriodoLaborado.DiasPermisosNoComputables.ToString();
                    txtTotalDiaslaborados.Text = (fAPeriodoLaboradoAnteriores.miAsistenciaPeriodoLaborado.DiasLaborados + fAPeriodoLaboradoAnteriores.miAsistenciaPeriodoLaborado.DiasPermisosNoComputables).ToString();
                    if (fAPeriodoLaboradoAnteriores.miAsistenciaPeriodoLaborado.Vacaciones == null)
                    {
                        txtVacacionesAdelantadas.Text = "V. Acumulada";
                        txtVacacionesDisponibles.Text = "V. Acumulada";
                    }
                    else
                    {
                        txtVacacionesAdelantadas.Text = fAPeriodoLaboradoAnteriores.miAsistenciaPeriodoLaborado.Vacaciones.DiasVacacionesAdelantadas.ToString();
                        txtVacacionesDisponibles.Text = fAPeriodoLaboradoAnteriores.miAsistenciaPeriodoLaborado.Vacaciones.DiasVacacionesDisponibles.ToString();
                    }
                }
            }
            catch (Exception m)
            { }
        }

        private void btnAsignarVacaciones_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (miPeriodoTrabajador.Trabajador == null || miVacaciones.AsistenciaPeriodoLaborado == null)
                //{
                //    MessageBox.Show("El Trabajador debe tener un periodo activo o cumplir con los requisitos para asignar vacaciones.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                //    return;
                //}
                if ((MessageBox.Show("Esta seguro de asignar el Periodo Laboral al Trabajador???", "GESTIÓN DEL SISTEMA", MessageBoxButton.OKCancel, MessageBoxImage.Question)) == MessageBoxResult.Cancel) { return; }

                caVacaciones.wMensaje fMensaje = new caVacaciones.wMensaje();
                fMensaje.Owner = this.Owner;
                if (fMensaje.ShowDialog() == true)
                {
                    if (fMensaje.rbtAsignar.IsChecked == true)
                    {
                        caVacaciones.wAsignarVacaciones fAsignarVacaciones = new caVacaciones.wAsignarVacaciones();
                        fAsignarVacaciones.Owner = this.Owner;
                        if (fAsignarVacaciones.ShowDialog() == true)
                        {
                            oblAsistenciaPeriodoLaborado.AgregarAsistenciaPeriodoLaborado(miAsistenciaPeriodoLaborado);
                            miVacaciones.AsistenciaPeriodoLaborado = miAsistenciaPeriodoLaborado;
                            oblVacaciones.AgregarVacaciones(miVacaciones);
                        }
                    }
                    else
                    {
                        if (oblAsistenciaAnual.AsignarVacaciones(miPeriodoTrabajador, micAsistenciaPeriodoLaborado) == true)
                        {
                            oblAsistenciaPeriodoLaborado.AgregarAsistenciaPeriodoLaborado(miAsistenciaPeriodoLaborado);
                        }
                    }
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.ToString(), "Gestión del Sistema", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LimpiarBotones()
        {
            dtpInicio.SelectedDate = DateTime.Today.Date;
            dtpFin.SelectedDate = DateTime.Today.Date;
            txtDiasLaborados.Text = "";
            txtPermisosComputables.Text = "";
            txtPermisosNoComputables.Text = "";
            txtTotalDiaslaborados.Text = "";
            txtVacacionesAdelantadas.Text = "";
            txtVacacionesDisponibles.Text = "";
        }

        private void CargarRecordAsistencia()
        {
            try
            {
                if (miPeriodoTrabajador.Trabajador == null)
                {
                    MessageBox.Show("El Trabajador debe tener un periodo activo.", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                micAsistenciaPeriodoLaborado = oblAsistenciaAnual.LlenarAsistenciaPeriodoLaborado(miPeriodoTrabajador, micAsistenciaPeriodoLaborado);
                micAsistenciaPeriodoLaborado = oblAsistenciaAnual.CalculoDiasAsistenciaPeriodoLaborado(micAsistenciaPeriodoLaborado);
                miVacaciones = oblAsistenciaAnual.CalcularVacaciones(micAsistenciaPeriodoLaborado);
                miAsistenciaPeriodoLaborado = micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado;

                dtpInicio.SelectedDate = micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Inicio;
                dtpFin.SelectedDate = micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.Fin;
                Calendario.DisplayDateStart = dtpInicio.DisplayDate;
                Calendario.DisplayDateEnd = dtpFin.DisplayDate;
                txtDiasLaborados.Text = micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.DiasLaborados.ToString();
                txtPermisosComputables.Text = micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.DiasPermisosComputables.ToString();
                txtPermisosNoComputables.Text = micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.DiasPermisosNoComputables.ToString();
                txtTotalDiaslaborados.Text = (micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.DiasLaborados + micAsistenciaPeriodoLaborado.miAsistenciaPeriodoLaborado.DiasPermisosNoComputables).ToString();
                txtVacacionesAdelantadas.Text = miVacaciones.DiasVacacionesAdelantadas.ToString();
                txtVacacionesDisponibles.Text = miVacaciones.DiasVacacionesDisponibles.ToString();

                Calendario.SelectedDates.Clear();
                for (int i = 0; i < 12; i++)
                {
                    foreach (CapaDeNegocios.cblAsistenciaAnual.cAsistenciaDia item in micAsistenciaPeriodoLaborado.miListaAsistenciaMeses[i].miListaAsistenciaDias)
                    {
                        DateTime auxiliar = item.fecha;
                        Calendario.SelectedDates.Add(auxiliar.Date);
                    }
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.ToString(), "Gestión del Sistema", MessageBoxButton.OK, MessageBoxImage.Error);
                btnAsignarVacaciones.IsEnabled = false;
            }
        }

        private void CargarPeriodoTrabajador(Trabajador miTrabajador)
        {
            try
            {
                ICollection<PeriodoTrabajador> ListaPeriodoTrabajador = oblPeriodoTrabajador.ListarPeriodoTrabajador(miTrabajador);
                foreach (PeriodoTrabajador item in ListaPeriodoTrabajador)
                {
                    miPeriodoTrabajador = item;
                }
            }
            catch (Exception m)
            {
            }
        }
    }
}
