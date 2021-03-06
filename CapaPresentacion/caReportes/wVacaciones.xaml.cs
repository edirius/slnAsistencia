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

namespace CapaPresentacion.caReportes
{
    /// <summary>
    /// Lógica de interacción para wVacaciones.xaml
    /// </summary>
    public partial class wVacaciones : Window
    {
        int sAño;
        Local miLocal = new Local();
        System.Data.DataTable oDataTrabajadores = new System.Data.DataTable();

        public wVacaciones()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarAños();
            CargarLocales();
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
                CapaDeNegocios.cblReportes.blControlVacaciones miReporteVacaciones = new CapaDeNegocios.cblReportes.blControlVacaciones();
                miReporteVacaciones.Control_Vacaciones(miListaTrabajadores, sAño);
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
            sAño = Convert.ToInt32(cboAño.Text);
            //}
        }

        private void cbolocales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboLocales.DisplayMemberPath != "")
            {
                miLocal.Id = Convert.ToInt32(cboLocales.SelectedValue);
                CargarTrabajadores();
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

        private void CargarLocales()
        {
            CapaDeNegocios.blLocal.blLocal oblLocal = new CapaDeNegocios.blLocal.blLocal();
            cboLocales.ItemsSource = oblLocal.ListarLocales();
            cboLocales.DisplayMemberPath = "Nombre";
            cboLocales.SelectedValuePath = "Id";
            cboLocales.SelectedIndex = -1;
        }

        private void CargarTrabajadores()
        {
            CapaDeNegocios.blTrabajador.blTrabajador oblTrabajador = new CapaDeNegocios.blTrabajador.blTrabajador();
            ICollection<Trabajador> ListaTrabajadores = oblTrabajador.ListaTrabajadores(miLocal);

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
