using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GestionnaireLicences.DataAccessLayer;
using GestionnaireLicences.DataAccessLayer.Factories;
using GestionnaireLicences.Models.Licence;
using Microsoft.AspNetCore.Identity;
using GestionnaireLicences.ViewModel;

namespace GestionnaireLicences.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new VM_MainWindow();
            //DAL dal = new DAL();
            //List<Licence> Licences = dal.LicenceFact.GetAll();
            //int p = 17;
        }
    }
}