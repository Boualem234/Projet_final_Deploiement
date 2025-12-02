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
    /// Logique d'interaction pour AjoutModificationLicence.xaml
    /// </summary>
    public partial class AjouterLicenceWindow: Window
    {
        public Licence NouvelleLicence { get; private set; }

        public AjouterLicenceWindow()
        {
            InitializeComponent();
            NouvelleLicence = new Licence(); // Initialiser une nouvelle licence
            this.DataContext = new VM_AjoutLicence(); // Définir le DataContext sur cette fenêtre
        }

        /*private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {

        }*/
        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            //DialogResult = false; // Fermer la fenêtre sans ajouter
            Close();
        }
    }
}

