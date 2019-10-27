using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using AForge.Video;
using AForge.Video.DirectShow;

namespace Diagnostico_Paciente
{
    public partial class SimuladorProblemasDeVista : Page
    {
        private bool TieneCamara;
        private FilterInfoCollection MisDispositivos;
        private VideoCaptureDevice Camara;

        public SimuladorProblemasDeVista()
        {
            InitializeComponent();
            CargarDispositivos();
            VistaNormal();
            CBTipoVista.Items.Add("Vista normal");
            CBTipoVista.Items.Add("Cataratas");
            CBTipoVista.Items.Add("Retinopatía");
            CBTipoVista.Items.Add("Glaucoma");
        }

        private void CargarDispositivos()
        {
            MisDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (MisDispositivos.Count > 0)
            {
                TieneCamara = true;
                for (int i = 0; i < MisDispositivos.Count; i++)
                {
                    CBDispositivos.Items.Add(MisDispositivos[i].Name.ToString());
                }
                CBDispositivos.Text = MisDispositivos[0].Name.ToString();
            }
            else
            {
                TieneCamara = false;
            }
        }

        private void Capturando(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    Bitmap Frame = (Bitmap)eventArgs.Frame.Clone();
                    ImageCapture.Width = Frame.Width;
                    ImageCapture.Height = Frame.Height;
                    ImageCapture.Source = Convert(Frame);
                });
            }
            catch (Exception e)
            {
                CerrarCamara();
            }
        }

        public BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        public void CerrarCamara()
        {
            if (Camara!= null && Camara.IsRunning)
            {
                Camara.SignalToStop();
                Camara = null;
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            CerrarCamara();
            int i = CBDispositivos.SelectedIndex;
            string NombreVideo = MisDispositivos[i].MonikerString;
            Camara = new VideoCaptureDevice(NombreVideo);
            Camara.NewFrame += new NewFrameEventHandler(Capturando);
            Camara.Start();
        }

        private void CBTipoVista_DropDownClosed(object sender, EventArgs e)
        {
            if (CBTipoVista.SelectedIndex == 0)
            {
                VistaNormal();
            }
            if (CBTipoVista.SelectedIndex == 1)
            {
                VistaCataratas();
            }
            if (CBTipoVista.SelectedIndex == 2)
            {
                VistaRetinopatia();
            }
            if (CBTipoVista.SelectedIndex == 3)
            {
                VistaGlaucoma();
            }
        }

        public void VistaNormal()
        {
            ImageCapture.Effect = null;
            GridRetinopatia.Visibility = Visibility.Hidden;
            GlaucomaSimulator.Visibility = Visibility.Hidden;
        }
        public void VistaGlaucoma()
        {
            GlaucomaSimulator.Visibility = Visibility.Visible;
            GridRetinopatia.Visibility = Visibility.Hidden;
            ImageCapture.Effect = null;
        }
        public void VistaRetinopatia()
        {
            GridRetinopatia.Visibility = Visibility.Visible;
            GlaucomaSimulator.Visibility = Visibility.Hidden;
            System.Windows.Media.Effects.BlurEffect BlurReti = new System.Windows.Media.Effects.BlurEffect();
            BlurReti.Radius = 5;
            BlurReti.KernelType = System.Windows.Media.Effects.KernelType.Gaussian;
            ImageCapture.Effect = BlurReti;

        }
        public void VistaCataratas()
        {
            System.Windows.Media.Effects.BlurEffect BlurCatarata = new System.Windows.Media.Effects.BlurEffect();
            BlurCatarata.Radius = 9;
            BlurCatarata.KernelType = System.Windows.Media.Effects.KernelType.Gaussian;
            ImageCapture.Effect = BlurCatarata;
        }
    }
}