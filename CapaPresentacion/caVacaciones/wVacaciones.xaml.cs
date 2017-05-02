﻿using System;
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
        public Trabajador miTrabajador = new Trabajador();
        public PeriodoTrabajador miPeriodoTrabajador = new PeriodoTrabajador();
        public AsistenciaPeriodoLaborado miAsistenciaPeriodoLaborado = new AsistenciaPeriodoLaborado();
        public Vacaciones miVacaciones = new Vacaciones();
        CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();
        CapaDeNegocios.blPeriodoTrabajador.blPeriodoTrabajador oblPeriodoTrabajador = new CapaDeNegocios.blPeriodoTrabajador.blPeriodoTrabajador();
        CapaDeNegocios.blAsistenciaPeriodoLaborado.blAsistenciaPeriodoLaborado oblAsistenciaPeriodoLaborado = new CapaDeNegocios.blAsistenciaPeriodoLaborado.blAsistenciaPeriodoLaborado();
        CapaDeNegocios.blVacaciones.blVacaciones oblVacaciones = new CapaDeNegocios.blVacaciones.blVacaciones();
        CapaDeNegocios.cblAsistenciaAnual.blAsistenciaAnual oblAsistenciaAnual = new CapaDeNegocios.cblAsistenciaAnual.blAsistenciaAnual();

        public wVacaciones()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnAsignarVacaciones_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (miAsistenciaPeriodoTrabajador.miListaAsistenciaPeriodoLaborado.Count == 0)
                //{
                //    MessageBox.Show("EL TRABAJADOR TIENE QUE TENER UN PERIODO ACTIVO", "GESTIÓN DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Information);
                //    return;
                //}
                if ((MessageBox.Show("Esta seguro de asignar el Periodo Laboral al Trabajador???", "GESTIÓN DEL SISTEMA", MessageBoxButton.OKCancel, MessageBoxImage.Question)) == MessageBoxResult.Cancel) { return; }

                oblAsistenciaPeriodoLaborado.AgregarAsistenciaPeriodoLaborado(miAsistenciaPeriodoLaborado);
                caVacaciones.wMensaje fMensaje = new caVacaciones.wMensaje();
                if (fMensaje.ShowDialog() == true)
                {
                    if (fMensaje.rbtAsignar.IsChecked == true)
                    {
                        miVacaciones.AsistenciaPeriodoLaborado = miAsistenciaPeriodoLaborado;
                        oblVacaciones.AgregarVacaciones(miVacaciones);
                    }
                }
            }
            catch (Exception m)
            { }
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
                CapaDeNegocios.cblAsistenciaAnual.cAsistenciaPeriodoLaborado micAsistenciaPeriodoLaborado = new CapaDeNegocios.cblAsistenciaAnual.cAsistenciaPeriodoLaborado();
                micAsistenciaPeriodoLaborado = oblAsistenciaAnual.LlenarAsistenciaPeriodoLaborado(miTrabajador, miPeriodoTrabajador, micAsistenciaPeriodoLaborado);
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
            }
        }

        private void CargarPeriodoTrabajador(Trabajador miTrabajador)
        {
            ICollection<PeriodoTrabajador> ListaPeriodoTrabajador = oblPeriodoTrabajador.ListarPeriodoTrabajador(miTrabajador);
            foreach (PeriodoTrabajador item in ListaPeriodoTrabajador)
            {
                miPeriodoTrabajador = item;
            }
        }
    }
}
