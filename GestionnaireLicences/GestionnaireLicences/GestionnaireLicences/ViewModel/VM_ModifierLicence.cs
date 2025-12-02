using GestionnaireLicences.DataAccessLayer;
using GestionnaireLicences.Models.Licence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace GestionnaireLicences.ViewModel
{
    internal class VM_ModifierLicence
    {
        private Licence _nouvelleLicence;

        public Licence NouvelleLicence
        {
            get { return _nouvelleLicence; }
            set
            {
                _nouvelleLicence = value;
                OnPropertyChanged(nameof(NouvelleLicence));
            }
        }

        public ICommand ModifierCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public VM_ModifierLicence(Licence licence)
        {

            NouvelleLicence = licence;
            ModifierCommand = new RelayCommand(Modifier);
        }

        private void Modifier()
        {

            DAL dal = new DAL();
            dal.LicenceFact.Save(NouvelleLicence);
            MessageBox.Show($"Licence Modifié : {NouvelleLicence.NomLogiciel}");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

