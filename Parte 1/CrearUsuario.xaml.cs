using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

namespace Optaclin
{
    /// <summary>
    /// Lógica de interacción para CrearUsuario.xaml
    /// </summary>
    public partial class CrearUsuario : Window
    {
        public CrearUsuario()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Optaclin.OptaclinDataSet optaclinDataSet = ((Optaclin.OptaclinDataSet)(this.FindResource("optaclinDataSet")));
            // Cargar datos en la tabla Usuarios. Puede modificar este código según sea necesario.
            Optaclin.OptaclinDataSetTableAdapters.UsuariosTableAdapter optaclinDataSetUsuariosTableAdapter = new Optaclin.OptaclinDataSetTableAdapters.UsuariosTableAdapter();
            optaclinDataSetUsuariosTableAdapter.Fill(optaclinDataSet.Usuarios);
            System.Windows.Data.CollectionViewSource usuariosViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("usuariosViewSource")));
            usuariosViewSource.View.MoveCurrentToFirst();
        }

        private void AgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Boolean EsAdmin;
                if (administrador.IsChecked == true)
                {
                    EsAdmin = true;
                }
                else
                {
                    EsAdmin = false;
                }
                string conect = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Optaclin.mdb";
                OleDbConnection conexion = new OleDbConnection(conect);
                conexion.Open();
                string insertar = "INSERT INTO `Usuarios` (`NombreCompleto`, `NombreUsuario`, `Contraseña`, `Administrador`) VALUES ('" + txtNComp.Text + "', '" + txtUsern.Text + "', '" + txtContraseña.Text + "',  " + EsAdmin + ")";
                OleDbCommand cmd = new OleDbCommand(insertar, conexion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuario guardado");
                Close();
            }

            catch (DBConcurrencyException ex)
            {
                MessageBox.Show("Error de concurrencia:\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
