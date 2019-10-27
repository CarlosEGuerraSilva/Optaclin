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

namespace Diagnostico_Paciente
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        DeteccionDaltonismo DeteccionDaltonismo;
        SimuladorProblemasDeVista SimuladorProblemasDeVista;
        MainPage PaginaPrincipal;

        public Principal()
        {
            InitializeComponent();
            ArrowContainer.Visibility = Visibility.Collapsed;
            PaginaPrincipal = new MainPage();
            PageTittle.Text = PaginaPrincipal.Title;
            PageHOST.Navigated += PageHOST_Navigated;
            PaginaPrincipal.DaltoExn.Click += DaltoExn_Click;
            PaginaPrincipal.Padec.Click += Padec_Click;
            PageHOST.Content = PaginaPrincipal;
        }

        private void PageHOST_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void Padec_Click(object sender, RoutedEventArgs e)
        {
            SimuladorProblemasDeVista = new SimuladorProblemasDeVista();
            PageHOST.Content = SimuladorProblemasDeVista;
            ArrowContainer.Visibility = Visibility.Visible;
            PageTittle.Text = SimuladorProblemasDeVista.Title;
        }

        private void DaltoExn_Click(object sender, RoutedEventArgs e)
        {
            DeteccionDaltonismo = new DeteccionDaltonismo();
            PageHOST.Content = DeteccionDaltonismo;
            ArrowContainer.Visibility = Visibility.Visible;
            PageTittle.Text = DeteccionDaltonismo.Title;
        }

        private void FrameGoBack_Click(object sender, RoutedEventArgs e)
        {
            if (PageHOST.CanGoBack)
            {
                if (SimuladorProblemasDeVista != null)
                {
                    SimuladorProblemasDeVista.CerrarCamara();
                }
                PageHOST.GoBack();
            }
            SimuladorProblemasDeVista = null;
            DeteccionDaltonismo = null;
            ArrowContainer.Visibility = Visibility.Collapsed;
            PageTittle.Text = PaginaPrincipal.Title;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (SimuladorProblemasDeVista != null)
            {
                SimuladorProblemasDeVista.CerrarCamara();
            }
        }
    }
}
