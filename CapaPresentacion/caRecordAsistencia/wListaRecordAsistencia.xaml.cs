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

namespace CapaPresentacion.caRecordAsistencia
{
    /// <summary>
    /// Lógica de interacción para wListaRecordAsistencia.xaml
    /// </summary>
    public partial class wListaRecordAsistencia : Window
    {
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
                if (fTrabajadores.ShowDialog() == true)
                {
                    txtTrabajador.Text = fTrabajadores.miTrabajador.Nombre + " " + fTrabajadores.miTrabajador.ApellidoPaterno + " " + fTrabajadores.miTrabajador.ApellidoMaterno;
                    miTrabajador = fTrabajadores.miTrabajador;
                }
            }
            catch
            { }
        }

        private void cboAño_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CargarRecordAsistencia();
        }

        private void cboMes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
            //ICollection<PermisosDias> ListaPermisoDias = oblPermisos.ListarPermisosDias();
            //dgPermisos.ItemsSource = ListaPermisoDias;
        }


    }
}
