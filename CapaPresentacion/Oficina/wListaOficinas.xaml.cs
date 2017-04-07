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

namespace CapaPresentacion.Oficina
{
    /// <summary>
    /// Lógica de interacción para ListaOficinas.xaml
    /// </summary>
    /// 

        
    public partial class wListaOficinas : Window
    {
        public Empresa miEntidad;
        public CapaDeNegocios.blLocal.blLocal oblLocal = new CapaDeNegocios.blLocal.blLocal();
        CapaDeNegocios.blOficina.blOficina oblOficina = new CapaDeNegocios.blOficina.blOficina();
        Local miLocal = new Local();

        public wListaOficinas()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            cboLocales.ItemsSource = oblLocal.ListarLocales();
            cboLocales.DisplayMemberPath = "Nombre";
        }

        private void cboLocales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            miLocal = (Local)cboLocales.SelectedItem; 
            tvListaOficinas.ItemsSource = oblOficina.ListarOficinas(miLocal);
            //tvListaOficinas.DisplayMemberPath = "Nombre";
        }

        //private void llenarArbol()
        //{
        //    ICollection<CapaEntities.Oficina> ListaOficinas = oblOficina.ListarOficinas(miLocal);
        //    tvListaOficinas.Items.Clear();
        //    tvListaOficinas.DisplayMemberPath = "Nombre";
        //    foreach (CapaEntities.Oficina  auxOficina in ListaOficinas)
        //    {
        //        //significa que no tiene padre
        //        if (auxOficina.Oficina2 != null)
        //        {
        //            tvListaOficinas.Items.Add(auxOficina);
        //        }
        //        //significa que tiene hijos
        //        else
        //        {
                            
        //        }
        //    }
        //}
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                Oficina.wOficina fOficina = new wOficina();
                fOficina.miOficina = new CapaEntities.Oficina();
                fOficina.miOficina.Local = miLocal;
                if (tvListaOficinas.SelectedItem != null)
                {
                    fOficina.miOficina.OficinaPadre  = (CapaEntities.Oficina)tvListaOficinas.SelectedItem;
                }
                //fOficina.miOficina.Oficina1 = fOficina.miOficina;
                //fOficina.miOficina.Oficina2 = fOficina.miOficina;
                if ( fOficina.ShowDialog() == true)
                {
                    oblOficina.AgregarOficina(fOficina.miOficina);
                }
            //}
            //catch (Exception d)
            //{
            //    MessageBox.Show(d.Message);
            //}
        }

        private void tvListaOficinas_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tvListaOficinas.SelectedItem != null)
            {
                CapaEntities.Oficina oficinaSeleccionada = (CapaEntities.Oficina)tvListaOficinas.SelectedItem;

                if (oficinaSeleccionada.OficinaPadre != null)
                {
                    lblPadre.Content = oficinaSeleccionada.OficinaPadre.Nombre;
                }
                else
                {
                    lblPadre.Content = "sin padre";
                }
                cboHijos.ItemsSource = oficinaSeleccionada.OficinasHijas;
                cboHijos.DisplayMemberPath = "Nombre";
            }
           
        }
    }
}
