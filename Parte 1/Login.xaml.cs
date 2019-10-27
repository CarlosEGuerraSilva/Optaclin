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
using System.Data.OleDb;
using System.Data;

namespace Optaclin
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        public List<string[]> ObtenerDatosDeUsuarios()
        {
            try
            {
                string conect = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Optaclin.mdb";
                string query = "SELECT NombreUsuario, Contraseña, Administrador FROM Usuarios";
                OleDbConnection conexion = new OleDbConnection(conect);
                OleDbCommand cmd = new OleDbCommand(query, conexion);
                conexion.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                List<string[]> usuarios = new List<string[]>();
                while (reader.Read())
                {
                    string[] user = new string[3];
                    user[0] = reader.GetValue(0).ToString();
                    user[1] = reader.GetValue(1).ToString();
                    user[2] = reader.GetValue(2).ToString();
                    usuarios.Add(user);
                }
                reader.Close();
                return usuarios;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            string[] UsuarioBuscado = new string[3];
            foreach (string[] usuario in ObtenerDatosDeUsuarios())
            {
                if (TBUname.Text == usuario[0])
                {
                    UsuarioBuscado = usuario;
                }
            }
            if (UsuarioBuscado[0] != null)
            {
                string username = UsuarioBuscado[0];
                string contraseña = UsuarioBuscado[1];

                if (PBContraseña.Password == contraseña)
                {
                    MainWindow MenuPrincipal = new MainWindow();
                    MenuPrincipal.CargarSesion(UsuarioBuscado[0], Boolean.Parse(UsuarioBuscado[2]));
                    MenuPrincipal.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Contraseña errónea", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Usuario no encontrado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
