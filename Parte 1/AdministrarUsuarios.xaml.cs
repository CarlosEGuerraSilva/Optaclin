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
    /// Lógica de interacción para AdministrarUsuarios.xaml
    /// </summary>
    public partial class AdministrarUsuarios : Window
    {
        public AdministrarUsuarios()
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

        private void UsuariosDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private void AñadirUsuario_Click(object sender, RoutedEventArgs e)
        {
            CrearUsuario crearUsuario = new CrearUsuario();
            crearUsuario.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            RefreshDataView();
        }

        private void RefreshDataView()
        {
            Optaclin.OptaclinDataSet optaclinDataSet = ((Optaclin.OptaclinDataSet)(this.FindResource("optaclinDataSet")));
            // Cargar datos en la tabla Usuarios. Puede modificar este código según sea necesario.
            Optaclin.OptaclinDataSetTableAdapters.UsuariosTableAdapter optaclinDataSetUsuariosTableAdapter = new Optaclin.OptaclinDataSetTableAdapters.UsuariosTableAdapter();
            optaclinDataSetUsuariosTableAdapter.Fill(optaclinDataSet.Usuarios);
            System.Windows.Data.CollectionViewSource usuariosViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("usuariosViewSource")));
            usuariosViewSource.View.MoveCurrentToFirst();
        }
    }
}
