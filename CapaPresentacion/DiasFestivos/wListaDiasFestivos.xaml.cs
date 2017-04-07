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
using CapaDeNegocios;
using CapaEntities;

namespace CapaPresentacion.DiasFestivos
{
    /// <summary>
    /// Lógica de interacción para wListaDiasFestivos.xaml
    /// </summary>
    public partial class wListaDiasFestivos : Window
    {
        CapaDeNegocios.blDiaFestivo.blDiaFestivo oblDiaFestivo = new CapaDeNegocios.blDiaFestivo.blDiaFestivo();

        public wListaDiasFestivos()
        {
            InitializeComponent();
        }

        private void btnAgregarDiaFestivo_Click(object sender, RoutedEventArgs e)
        {
            DiasFestivos.wDiaFestivo fDiaFestivo = new wDiaFestivo();
            fDiaFestivo.miDiaFestivo = new DiaFestivo();

            if (fDiaFestivo.ShowDialog() == true)
            {
                oblDiaFestivo.AgregarDiaFestivo(fDiaFestivo.miDiaFestivo);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            dtgListaDiasFestivos.ItemsSource = oblDiaFestivo.ListarDiaFestivos();

        }

        private void btnModificarDiaFestivo_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListaDiasFestivos.SelectedItem != null)
            {
                DiasFestivos.wDiaFestivo fDiaFestivo = new wDiaFestivo();
                fDiaFestivo.miDiaFestivo =(DiaFestivo) dtgListaDiasFestivos.SelectedItem;

                if (fDiaFestivo.ShowDialog() == true)
                {
                    oblDiaFestivo.ModificarDiaFestivo(fDiaFestivo.miDiaFestivo);
                }
            }
            
        }
    }
}
