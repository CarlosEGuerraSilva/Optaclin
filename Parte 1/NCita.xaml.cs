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
    /// Lógica de interacción para NCita.xaml
    /// </summary>
    public partial class NCita : Window
    {
        string Hora;
        public NCita()
        {
            InitializeComponent();
            CBT.Items.Add("AM");
            CBT.Items.Add("PM");
            UPH.Click += UPH_Click;
            DWH.Click += DWH_Click;
            UPM.Click += UPM_Click;
            DWM.Click += DWM_Click;
        }

        private void DWM_Click(object sender, RoutedEventArgs e)
        {
            int Num = Convert.ToInt32(txtMinutos.Text);
            if (Num <= 0)
            {
                Num = 59;
            }
            else
            {
                Num--;
            }
            txtMinutos.Text = Num.ToString();
        }

        private void UPM_Click(object sender, RoutedEventArgs e)
        {
            int Num = Convert.ToInt32(txtMinutos.Text);
            if (Num >= 59)
            {
                Num = 0;
            }
            else
            {
                Num++;
            }
            txtMinutos.Text = Num.ToString();
        }

        private void DWH_Click(object sender, RoutedEventArgs e)
        {
            int Num = Convert.ToInt32(txtHora.Text);
            if (Num >= 0)
            {
                Num = 11;
            }
            else
            {
                Num--;
            }
            txtHora.Text = Num.ToString();
        }

        private void UPH_Click(object sender, RoutedEventArgs e)
        {
            int Num = Convert.ToInt32(txtHora.Text);
            if (Num >= 11)
            {
                Num = 0;
            }
            else
            {
                Num++;
            }
            txtHora.Text = Num.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtHora.Text.Length == 1)
                {
                    txtHora.Text = "0"+ txtHora.Text;
                }
                if (txtMinutos.Text.Length == 1)
                {
                    txtMinutos.Text = "0" + txtMinutos.Text;
                }
                Hora = txtHora.Text + ":" + txtMinutos.Text + " " + CBT.Text;
                MessageBox.Show(Hora);
                string conect = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Optaclin.mdb";
                OleDbConnection conexion = new OleDbConnection(conect);
                conexion.Open();
                string insertar = "INSERT INTO `Citas` (`Dia`, `Hora`, `Nombres`, `Apellidos`, `CorreoE`, `MotivoDeCita`) VALUES ('" + calendario.SelectedDate+"', '"+ Hora + "', '" + txtNombres.Text + "', '" + txtApellidos.Text + "', '" + txtCorreoE.Text + "', '" + txtMotivoDeCita.Text + "')";
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
