using GestionnaireLicences.DataAccessLayer;
using GestionnaireLicences.Models.Licence;
using GestionnaireLicences.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace GestionnaireLicences.ViewModel
{
    public class VM_MainWindow : INotifyPropertyChanged
    {
        private ObservableCollection<Licence> _licences;
        private Licence _licenceSelectionnee;
        private string _statusMessage;

        public ObservableCollection<Licence> Licences
        {
            get { return _licences; }
            set
            {
                _licences = value;
                OnPropertyChanged(nameof(Licences));
            }
        }

        public Licence LicenceSelectionnee
        {
            get { return _licenceSelectionnee; }
            set
            {
                _licenceSelectionnee = value;
                OnPropertyChanged(nameof(LicenceSelectionnee));
            }
        }

        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        public ICommand AjouterCommand { get; private set; }
        public ICommand ModifierCommand { get; private set; }
        public ICommand SupprimerCommand { get; private set; }

        public VM_MainWindow()
        {
            Licences = new ObservableCollection<Licence>();
            ChargerLicences();

            AjouterCommand = new RelayCommand(Ajouter, () => true);
            ModifierCommand = new RelayCommand(Modifier, () => LicenceSelectionnee != null);
            SupprimerCommand = new RelayCommand(Supprimer, () => LicenceSelectionnee != null);
        }

        private void ChargerLicences()
        {
            // TODO: Charger les licences depuis la base de données
            // Exemple :
            DAL dal = new DAL();
            List<Licence> licences = dal.LicenceFact.GetAll();
            Licences = new ObservableCollection<Licence>(licences);
            StatusMessage = "Licences chargées";
        }

        private void Ajouter()
        {
            // TODO: Implémenter la logique d'ajout
            AjouterLicenceWindow alw = new AjouterLicenceWindow();
            alw.Show();
            StatusMessage = "Ajout d'une nouvelle licence";
        }

        private void Modifier()
        {
            if (LicenceSelectionnee != null)
            {
                ModifierLicenceWindow mlw = new ModifierLicenceWindow(LicenceSelectionnee);
                mlw.Show();
                StatusMessage = $"Modification de la licence : {LicenceSelectionnee.NomLogiciel}";
            }
        }

        private void Supprimer()
        {
            if (LicenceSelectionnee != null)
            {
                // TODO: Implémenter la logique de suppression
                DAL dal = new DAL();
                dal.LicenceFact.Delete(LicenceSelectionnee.Id);
                StatusMessage = $"Licence supprimée : {LicenceSelectionnee.NomLogiciel}";
                Licences = new ObservableCollection<Licence>(dal.LicenceFact.GetAll());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Classe RelayCommand pour implémenter ICommand
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();
    }
}