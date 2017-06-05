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
using CapaEntities;
using CapaDeNegocios;

namespace CapaPresentacion.caReportes
{
    /// <summary>
    /// Lógica de interacción para wAsistencia.xaml
    /// </summary>
    public partial class wAsistencia : Window
    {

        public wAsistencia()
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
