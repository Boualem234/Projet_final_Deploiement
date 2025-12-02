using GestionnaireLicences.Models.Licence;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using GestionnaireLicences.DataAccessLayer.Factories;
using GestionnaireLicences.DataAccessLayer;

namespace GestionnaireLicences.ViewModel
{
    public class VM_AjoutLicence : INotifyPropertyChanged
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

        public ICommand AjouterCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public VM_AjoutLicence()
        {

            NouvelleLicence = new Licence();
            AjouterCommand = new RelayCommand(Ajouter);
        }

        private void Ajouter()
        {

            DAL dal = new DAL();
            //dal.LicenceFact.
            // Logique pour ajouter la licence à la base de données
            // Par exemple, vous pouvez appeler un service ou un contexte de base de données ici

            // Exemple de message de succès (vous pouvez le remplacer par une logique réelle)
            dal.LicenceFact.Save(NouvelleLicence);
            MessageBox.Show($"Licence ajoutée : {NouvelleLicence.NomLogiciel}");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}