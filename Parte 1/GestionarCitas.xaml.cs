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

namespace Optaclin
{
    /// <summary>
    /// Lógica de interacción para GestionarCitas.xaml
    /// </summary>
    public partial class GestionarCitas : Window
    {
        public GestionarCitas()
        {
            InitializeComponent();
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

        private void AgregarCita_Click(object sender, RoutedEventArgs e)
        {
            NCita nCita = new NCita();
            nCita.ShowDialog();
            citasDataGrid.Items.Refresh();
        }

        private void CitasDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
