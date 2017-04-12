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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using CapaDeNegocios;
using CapaEntities;

namespace CapaPresentacion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Empresa miEntidad;
        CapaDeNegocios.blEmpresa.blEmpresa oblEmpresa = new CapaDeNegocios.blEmpresa.blEmpresa();

        public MainWindow()
        {
            InitializeComponent();
            Iniciar();
        }

        public void Iniciar()
        {
            string nombre=""; 
            miEntidad = new Empresa();
            ICollection<Empresa> miListaEmpresa = oblEmpresa.ListarEmpresas();
            if (miListaEmpresa.Count() == 0)
            {
                System.Windows.Forms.MessageBox.Show("No existe registrada una entidad.");
                string value = "Document 1";
                if (InputBox("New document", "Nombre Entidad", ref value) ==   System.Windows.Forms.DialogResult.OK)
                {
                    nombre = value;
                }
                miEntidad.Nombre = nombre;
                oblEmpresa.AgregarEmpresa(miEntidad);
                miListaEmpresa = oblEmpresa.ListarEmpresas();
            }
            miEntidad = miListaEmpresa.First();
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            System.Windows.Forms.Label label = new System.Windows.Forms.Label();
            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
            System.Windows.Forms.Button buttonOk = new  System.Windows.Forms.Button();
            System.Windows.Forms.Button buttonCancel = new  System.Windows.Forms.Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new System.Drawing.Size (396, 107);
            form.Controls.AddRange(new System.Windows.Forms.Control [] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new System.Drawing.Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private void mnuEntidad_Click(object sender, RoutedEventArgs e)
        {
            Entidad.wListaEntidad fListaEntidad = new Entidad.wListaEntidad();
            fListaEntidad.miEntidad = miEntidad;
            fListaEntidad.ShowDialog();
        }

        private void mnuOficina_Click(object sender, RoutedEventArgs e)
        {
            Oficina.wListaOficinas fListaOficinas = new Oficina.wListaOficinas();
            //fListaOficinas.MD
            fListaOficinas.Show();
        }

        private void mnuTrabajador_Click(object sender, RoutedEventArgs e)
        {
            caTrabajadores.wListaTrabajadores fListaTrabajadores = new caTrabajadores.wListaTrabajadores();
            fListaTrabajadores.ShowDialog();
        }

        private void mnuAsistencia_Click(object sender, RoutedEventArgs e)
        {
            Asistencia.wListaAsistencias fListaAsistencias = new Asistencia.wListaAsistencias();
            fListaAsistencias.ShowDialog();
        }

        private void mnuHorario_Click(object sender, RoutedEventArgs e)
        {
            caHorario.wListaHorario fListaHorario = new caHorario.wListaHorario();
            fListaHorario.ShowDialog();
        }

        private void mnuHorarioDia_Click(object sender, RoutedEventArgs e)
        {
            caHorarioDia.wListaHorarioDia fListaHorarioDia = new caHorarioDia.wListaHorarioDia();
            fListaHorarioDia.ShowDialog();
        }

        private void mnuHorarioSemana_Click(object sender, RoutedEventArgs e)
        {
            caHorarioSemana.wListaHorarioSemana fListaHorarioSemana = new caHorarioSemana.wListaHorarioSemana();
            fListaHorarioSemana.ShowDialog();
        }

        private void mnuPermisoHoras_Click(object sender, RoutedEventArgs e)
        {
            PermisosHoras.wListaPermisosHoras fListaPermisosHoras = new PermisosHoras.wListaPermisosHoras();
            fListaPermisosHoras.ShowDialog();
        }

        private void mnuPermisoDias_Click(object sender, RoutedEventArgs e)
        {
            PermisosDias.wListaPermisosDias fListaPermisosDias = new PermisosDias.wListaPermisosDias();
            fListaPermisosDias.ShowDialog();
        }

        private void mnuPermisos_Click(object sender, RoutedEventArgs e)
        {
            caPermisos.wListaPermisos fListaPermisos = new caPermisos.wListaPermisos();
            fListaPermisos.ShowDialog();
        }

        private void mnuTipoPermisos_Click(object sender, RoutedEventArgs e)
        {
            caTipoPermisos.wListaTipoPermisos fListaTipoPermisos = new caTipoPermisos.wListaTipoPermisos();
            fListaTipoPermisos.ShowDialog();
        }

        private void mnuCronogramaVacaciones_Click(object sender, RoutedEventArgs e)
        {
            CronogramaVacaciones.wListaCronogramaVacaciones fListaCronogramaVacaciones = new CronogramaVacaciones.wListaCronogramaVacaciones();
            fListaCronogramaVacaciones.ShowDialog();
        }

        private void mnuDiasFestivos_Click(object sender, RoutedEventArgs e)
        {
            DiasFestivos.wListaDiasFestivos fListaDiasFestivos = new DiasFestivos.wListaDiasFestivos();
            fListaDiasFestivos.ShowDialog();
        }

        private void mnuReglasTardanza_Click(object sender, RoutedEventArgs e)
        {
            ReglasTardanza.wListaReglasTardanzas fListaReglasTardanzas = new ReglasTardanza.wListaReglasTardanzas();
            fListaReglasTardanzas.ShowDialog();
        }

        private void mnuImportarAsistencia_Click(object sender, RoutedEventArgs e)
        {
            wImportarAsistencia.wImportarAsistencia fImportarAsistencia = new wImportarAsistencia.wImportarAsistencia();
            fImportarAsistencia.ShowDialog();
        }
    }
}
