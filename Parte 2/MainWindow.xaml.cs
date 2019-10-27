using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Diagnostico_Paciente
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            EsperarAsync();
            MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
        }

        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public async void EsperarAsync()
        {
            Task Esperar = new Task(EsperarS);
            Esperar.Start();
            await Esperar;
            Principal principal = new Principal();
            principal.Show();
            Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
        }
        public void EsperarS()
        {
            Thread.Sleep(1000);
        }
    }
}
