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
using System.Windows.Media.Animation;
using System.Data.OleDb;
using System.Data;

namespace Optaclin
{
    /// <summary>
    /// Lógica de interacción para GestionarPacientes.xaml
    /// </summary>
    public partial class GestionarPacientes : Window
    {
        Storyboard AbrirEditor;
        Storyboard CerrarEditor;
        public GestionarPacientes()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Optaclin.OptaclinDataSet optaclinDataSet = ((Optaclin.OptaclinDataSet)(this.FindResource("optaclinDataSet")));
            // Cargar datos en la tabla CrearPacientes. Puede modificar este código según sea necesario.
            Optaclin.OptaclinDataSetTableAdapters.CrearPacientesTableAdapter optaclinDataSetCrearPacientesTableAdapter = new Optaclin.OptaclinDataSetTableAdapters.CrearPacientesTableAdapter();
            optaclinDataSetCrearPacientesTableAdapter.Fill(optaclinDataSet.CrearPacientes);
            System.Windows.Data.CollectionViewSource crearPacientesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("crearPacientesViewSource")));
            crearPacientesViewSource.View.MoveCurrentToFirst();
        }

        private void EditarPaciente_Click(object sender, RoutedEventArgs e)
        {
            AbrirEditor = (Storyboard)this.Resources["AbrirEditor"];
            AbrirEditor.Completed += AbrirEditor_Completed;
            AbrirEditor.Begin();
        }

        private void AbrirEditor_Completed(object sender, EventArgs e)
        {
            AbrirEditor = null;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string conect = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Optaclin.mdb";
                OleDbConnection conexion = new OleDbConnection(conect);
                conexion.Open();
                string insertar = "UPDATE `CrearPacientes` SET `Nombres` = @nombres, `Apellidos` = @apellidos, `Alergias` = @alergias, `ProblemaVisual` = @problemavisual, `Telefono` = @telefono, `Correo` = @correo WHERE `Id` = @id";
                OleDbCommand cmd = new OleDbCommand(insertar, conexion);
                cmd.Parameters.AddWithValue("@nombres", nombresTextBox.Text);
                cmd.Parameters.AddWithValue("@apellidos", apellidosTextBox.Text);
                cmd.Parameters.AddWithValue("@alergias", alergiasTextBox.Text);
                cmd.Parameters.AddWithValue("@problemavisual", problemaVisualTextBox.Text);
                cmd.Parameters.AddWithValue("@telefono", telefonoTextBox.Text);
                cmd.Parameters.AddWithValue("@correo", correoTextBox.Text);
                cmd.Parameters.AddWithValue("@id", idTextBox.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Paciente actualizado");
            }

            catch (DBConcurrencyException ex)
            {
                MessageBox.Show("Error de concurrencia:\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ". " + ex.Data.ToString()+  ex.HResult.ToString() + ". " + ex.Source.ToString());
            }
            CerrarEditor = (Storyboard)this.Resources["CerrarEditor"];
            CerrarEditor.Completed += CerrarEditor_Completed;
            CerrarEditor.Begin();
        }

        private void CerrarEditor_Completed(object sender, EventArgs e)
        {
            CerrarEditor = null;
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string conect = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Optaclin.mdb";
                OleDbConnection conexion = new OleDbConnection(conect);
                conexion.Open();
                string insertar = "DELETE FROM `CrearPacientes` WHERE `Id` = @id";
                OleDbCommand cmd = new OleDbCommand(insertar, conexion);
                cmd.Parameters.AddWithValue("@id", idTextBox.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Paciente eliminado");
            }

            catch (DBConcurrencyException ex)
            {
                MessageBox.Show("Error de concurrencia:\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CerrarEditor = (Storyboard)this.Resources["CerrarEditor"];
            CerrarEditor.Begin();
            CerrarEditor.Completed += CerrarEditor_Completed;
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            CerrarEditor = (Storyboard)this.Resources["CerrarEditor"];
            CerrarEditor.Begin();
            CerrarEditor.Completed += CerrarEditor_Completed;
        }

        private void NuevoPaciente_Click(object sender, RoutedEventArgs e)
        {
            CrearPacientes crearPacientes = new CrearPacientes();
            crearPacientes.ShowDialog();
            crearPacientesDataGrid.Items.Refresh();
        }

        private void CrearPacientesDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private void CrearPacientesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
