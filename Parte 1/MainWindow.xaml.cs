using Optaclin.Properties;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Optaclin
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool Administrador = true;
        DispatcherTimer MostrarHora;
        OleDbDataAdapter CitasAdapter;

        public MainWindow()
        {
            InitializeComponent();
            MostrarHora = new DispatcherTimer(DispatcherPriority.Send);
            MostrarHora.Interval = new TimeSpan(0, 0, 1);
            MostrarHora.Tick += MostrarHora_Tick;
            MostrarHora.Start();
            CargarDatosDeCitas();
        }

        private async void CargarDatosDeCitas()
        {
            /*ListaCitas.Items.Refresh();
            string conect = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Optaclin.mdb";
            OleDbConnection conexion = new OleDbConnection(conect);
            conexion.Open();
            string instr = "SELECT Nombres, Hora FROM Citas WHERE Dia LIKE " + DateTime.Now.ToShortDateString();
            CitasAdapter = new OleDbDataAdapter(instr, conexion);
            OleDbCommandBuilder cmdBuilder = new OleDbCommandBuilder(CitasAdapter);
            ListaCitas.Items.Clear();
            CitasAdapter.Fill(ListaCitas);
            //cmd.ExecuteNonQuery();
            try
            {
                string conect = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Optaclin.mdb";
                string query = "SELECT Hora, Nombres FROM Citas WHERE Dia LIKE " + DateTime.Now.ToShortDateString();
                OleDbConnection conexion = new OleDbConnection(conect);
                OleDbCommand cmd = new OleDbCommand(query, conexion);
                conexion.Open();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }*/
        }

        public void CargarSesion(string UserName, bool Admin)
        {
            SessionUNAME.Text = UserName;
            if (Admin == true)
            {
                Administrador = true;
            }
            else
            {
                Administrador = false;
            }
        }

        private void MostrarHora_Tick(object sender, EventArgs e)
        {
            DPHora.Text = DateTime.Now.ToLongTimeString() + " - " + DateTime.Now.ToShortDateString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NCita cita = new NCita();
            cita.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CrearPacientes cp = new CrearPacientes();
            cp.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GestionarCitas gc = new GestionarCitas();
            gc.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            GestionarPacientes gpi = new GestionarPacientes();
            gpi.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (Administrador)
            {
                AdministrarUsuarios au = new AdministrarUsuarios();
                au.Show();
            }
            else
            {
                MessageBox.Show("Tu perfil no está autorizado para acceder a este contenido", "Se requiere permiso de administraodr", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Optaclin.OptaclinDataSet optaclinDataSet = ((Optaclin.OptaclinDataSet)(this.FindResource("optaclinDataSet")));
            // Cargar datos en la tabla Citas. Puede modificar este código según sea necesario.
            Optaclin.OptaclinDataSetTableAdapters.CitasTableAdapter optaclinDataSetCitasTableAdapter = new Optaclin.OptaclinDataSetTableAdapters.CitasTableAdapter();
            optaclinDataSetCitasTableAdapter.Fill(optaclinDataSet.Citas);
            System.Windows.Data.CollectionViewSource citasViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("citasViewSource")));
            citasViewSource.View.MoveCurrentToFirst();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            CargarDatosDeCitas();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.TestVisualPath != "")
            {
                System.Diagnostics.Process.Start(Settings.Default.TestVisualPath);
            }
            else
            {
                string LocalPath = Environment.CurrentDirectory;
                LocalPath = LocalPath + "/Tools/PruebaDeVista/Diagnostico Paciente.exe";
                try
                {
                    System.Diagnostics.Process.Start(LocalPath);
                }
                catch
                {
                    MessageBox.Show("No se encuentra el ejecutable", "Error");
                }
            }
        }

        private void Ajustes_Click(object sender, RoutedEventArgs e)
        {
            Ajustes ajustes = new Ajustes();
            ajustes.Show();
        }
    }
}
