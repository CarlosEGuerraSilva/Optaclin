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
using System.Windows.Threading;

namespace Diagnostico_Paciente
{
    public partial class DeteccionDaltonismo : Page
    {
        Brush TargetColor, TargetColor2;
        Brush FillColor1, FillColor2, FillColor3;
        DispatcherTimer Timer;

        private async void StartTest_Click(object sender, RoutedEventArgs e)
        {
            StartTest.IsEnabled = false;
            Task GenerateDetectImg = new Task(Start);
            GenerateDetectImg.Start();
            await GenerateDetectImg;
            //Start();
        }

        public DeteccionDaltonismo()
        {
            InitializeComponent();
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
        }

        public async void Start()
        {
            Dispatcher.Invoke(async () =>
            {
                if (majorContainer.Children.Count != 0)
            {
                majorContainer.Children.Clear();
            }
            if (CBTipoDePadecimiento.SelectedIndex == 0)
            {
                //Rojo
                TargetColor = new SolidColorBrush(Color.FromArgb(255, 192, 57, 43));
                TargetColor2 = new SolidColorBrush(Color.FromArgb(255, 203, 67, 53));
                //Azules/Verdes
                FillColor1 = new SolidColorBrush(Color.FromArgb(255, 41, 128, 185));
                FillColor2 = new SolidColorBrush(Color.FromArgb(255, 84, 153, 199));
                FillColor3 = new SolidColorBrush(Color.FromArgb(255, 84, 123, 219));
            }
            if (CBTipoDePadecimiento.SelectedIndex == 1)
            {
                //Verde
                TargetColor = new SolidColorBrush(Color.FromArgb(255, 30, 132, 73));
                TargetColor2 = new SolidColorBrush(Color.FromArgb(255, 35, 155, 86));
                //Rojos/Azules
                FillColor1 = new SolidColorBrush(Color.FromArgb(255, 172, 47, 53));
                FillColor2 = new SolidColorBrush(Color.FromArgb(255, 203, 67, 53));
                FillColor3 = new SolidColorBrush(Color.FromArgb(255, 41, 128, 185));
            }
            if (CBTipoDePadecimiento.SelectedIndex == 2)
            {
                //Azul
                TargetColor = new SolidColorBrush(Color.FromArgb(255, 41, 128, 185));
                TargetColor2 = new SolidColorBrush(Color.FromArgb(255, 84, 153, 199));
                //Rojos/Amarillos
                FillColor1 = new SolidColorBrush(Color.FromArgb(255, 192, 57, 43));
                FillColor2 = new SolidColorBrush(Color.FromArgb(255, 203, 67, 53));
                FillColor3 = new SolidColorBrush(Color.FromArgb(255, 243, 156, 18));
            }
            for (int i = 1; i <= 1024; i++)
            {
                Task EsperarS = new Task(esperar1seg);
                EsperarS.Start();
                await EsperarS;
                Grid Pixel = new Grid();
                Pixel.Width = 15.5;
                Pixel.Height = 15.5;
                Random random = new Random(DateTime.Now.Millisecond);
                int Probabildades = random.Next(1, 100);
                if (Probabildades == 1)
                {
                    Pixel.Background = TargetColor;
                }
                else if (Probabildades == 2)
                {
                    Pixel.Background = TargetColor2;
                }
                else
                {
                        if (Probabildades >= 3 && Probabildades < 30)
                        {
                            Pixel.Background = FillColor1;
                        }
                        if (Probabildades == 30)
                        {
                            Pixel.Background = TargetColor2;
                        }
                        if (Probabildades > 30 && Probabildades < 66)
                        {
                            Pixel.Background = FillColor2;
                        }
                        if (Probabildades == 66)
                        {
                            Pixel.Background = TargetColor;
                        }
                        if (Probabildades > 66 && Probabildades < 101)
                        {
                            Pixel.Background = FillColor3;
                        }
                    }
                majorContainer.Children.Add(Pixel);
                }
                StartTest.IsEnabled = true;
            });
        }

        public void esperar1seg()
        {
            Thread.Sleep(10);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            
        }
        /*
        #region ClicPixelEvents
        public void ClicEvents()
        {
            Pixel1.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel2.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel3.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel4.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel5.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel6.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel7.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel8.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel9.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel10.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel11.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel12.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel13.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel14.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel15.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel16.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel17.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel18.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel19.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel20.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel21.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel22.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel23.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel24.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel25.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel26.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel27.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel28.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel29.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel30.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel31.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel32.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel33.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel34.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel35.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel36.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel37.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel38.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel39.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel40.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel41.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel42.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel43.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel44.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel45.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel46.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel47.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel48.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel49.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel50.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel51.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel52.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel53.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel54.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel55.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel56.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel57.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel58.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel59.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
            Pixel60.MouseLeftButtonUp += Pixel1_MouseLeftButtonUp;
        }

        private void Pixel1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Pixel1.Background == Brushes.DarkRed)
            {

            }
        }
        #endregion
        */
    }
}
