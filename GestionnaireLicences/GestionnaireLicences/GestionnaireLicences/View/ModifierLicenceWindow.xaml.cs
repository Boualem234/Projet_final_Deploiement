using GestionnaireLicences.Models.Licence;
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
using GestionnaireLicences.ViewModel;
namespace GestionnaireLicences.View
{
    /// <summary>
    /// Logique d'interaction pour ModifierLicenceWindow.xaml
    /// </summary>
    public partial class ModifierLicenceWindow : Window
    {
        public ModifierLicenceWindow(Licence licence)
        {

            InitializeComponent();
            this.DataContext = new VM_ModifierLicence(licence);
        }
        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            //DialogResult = false; // Fermer la fenêtre sans ajouter
            Close();
        }
    }
}
