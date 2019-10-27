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
    /// Lógica de interacción para CrearPacientes.xaml
    /// </summary>
    public partial class CrearPacientes : Window
    {
        public CrearPacientes()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string conect = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Optaclin.mdb";
                OleDbConnection conexion = new OleDbConnection(conect);
                conexion.Open();
                string insertar = "INSERT INTO `CrearPacientes` (`Nombres`, `Apellidos`, `Alergias`, `ProblemaVisual`, `Telefono`, `Correo`) VALUES ('" + txtNombres.Text + "', '" + txtApellidos.Text + "', '" + txtAlergias.Text + "',  '" + txtProblemaVisual.Text + "', '" + txtTelefono.Text + "',  '" + txtCorreoE.Text + "')";
                OleDbCommand cmd = new OleDbCommand(insertar, conexion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro guardado");
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
