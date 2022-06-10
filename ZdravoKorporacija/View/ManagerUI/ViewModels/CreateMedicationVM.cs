using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using ZdravoKorporacija.View.ManagerUI.Commands;
using ZdravoKorporacija.View.ManagerUI.Help;
using ZdravoKorporacija.View.ManagerUI.Views;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI.ViewModels
{
    public class CreateMedicationVM : INotifyPropertyChanged
    {
        private Medication medication;
        private String name;
        private String alternative;
        private String ingredient;
        public ObservableCollection<String> MedicationIngredients { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public String SelectedIngredient { get; set; }
        public MedicationController medicationController { get; set; }
        public ObservableCollection<Medication> medications;


        public Medication Medication
        {
            get { return medication; }
            set
            {
                medication = value;
                OnPropertyChanged("Medication");
            }
        }

        public String Name
        {
            get { return name; }    
            set { name = value; 
                OnPropertyChanged("Name"); 
            }
        }

        public String Alternative
        {
            get { return alternative; }
            set
            {
                alternative = value;
                OnPropertyChanged("Alternative");
            }
        }

        public String Ingredient
        {
            get { return ingredient; }
            set
            {
                ingredient = value;
                OnPropertyChanged("Ingredient");
            }
        }

        public ObservableCollection<Medication> Medications
        {
            get
            {
                return medications;
            }
            set
            {
                medications = value;
                OnPropertyChanged("Medications");
            }
        }


        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        Page page;

        public CreateMedicationVM(Page page)
        {
            this.page = page;
            Medication = new Medication();
            Medication.Ingredients = new List<String>();
            MedicationRepository medicationRepository = new MedicationRepository();
            MedicationService medicationService = new MedicationService(medicationRepository);
            medicationController = new MedicationController(medicationService);
            MedicationIngredients = new ObservableCollection<String>();
            SaveCommand = new RelayCommand(saveExecute);
            AddIngredientCommand = new RelayCommand(addIngredientExecute);
            RemoveIngredientCommand = new RelayCommand(removeIngredientExecute);
            GetAllUnverifiedMedicationsCommand = new RelayCommand(getAllUnverifiedMedications);
            Medications = new ObservableCollection<Medication>(medicationController.GetAllUnverified());

            page.CommandBindings.Add(new CommandBinding(ZdravoKorporacija.View.ManagerUI.Commands.RoutedCommand.Help, CreateMedicationHelp_Executed, CreateMedicationHelp_CanExecute));

            page.CommandBindings.Add(new CommandBinding(ZdravoKorporacija.View.ManagerUI.Commands.RoutedCommand.Back, GoBack_Executed,  GoBack_CanExecute));

        }

        public ICommand SaveCommand { get; set; }
        public ICommand AddIngredientCommand { get; set; }
        public ICommand RemoveIngredientCommand { get; set; }
        public ICommand GetAllUnverifiedMedicationsCommand { get; set; }


        private void saveExecute(object parameter)
        {
            
            try
            {
                medicationController.Create(Medication.Name, Medication.Ingredients, Medication.Alternative);
                ManagerWindowVM.NavigationService.Navigate(new GetAllUnverifiedMedications());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message , "Greška");
            }
        }


        private void addIngredientExecute(object parameter)
        {
            if (Ingredient.Length > 0)
            {
                
                MedicationIngredients.Add(Ingredient);
                Medication.Ingredients.Add(Ingredient);
                Ingredient = "";
            }
        }

        private void removeIngredientExecute(object parameter)
        {
            Medication.Ingredients.Remove(SelectedIngredient);
            MedicationIngredients.Remove(SelectedIngredient);
        }

        private void getAllUnverifiedMedications(object sender)
        {
            medicationListToMedicationList(medicationController.GetAllUnverified());

        }



        private void medicationListToMedicationList(List<Medication> medicationsToAdd)
        {
            Medications= new ObservableCollection<Medication>();
            foreach (var m in medicationsToAdd)
            {
                Medications.Add(m);
            }
        }

        private void CreateMedicationHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CreateMedicationHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CreateMedicationHelp createMedicationHelp = new CreateMedicationHelp();
            createMedicationHelp.Show();
        }


        private void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            page.NavigationService.Navigate(new ManagerHomePage());
        }


    }
}
