using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for ModifyMedication.xaml
    /// </summary>
    public partial class ModifyMedication : Page, INotifyPropertyChanged
    {
        private MedicationController medicationController;
        private Medication selectedMedication;
        private String medicationName;
        private String medicationAlternative;
        private static ObservableCollection<String> medicationIngredients;
        public event PropertyChangedEventHandler? PropertyChanged;
        public string SelectedIngredient { get; set; }
        private String ingredient;
        private int medicationId;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ObservableCollection<String> MedicationIngredients
        {
            get { return medicationIngredients; }
            set { medicationIngredients = value; OnPropertyChanged("MedicationIngredients"); }
        }


        public int MedicationId
        {
            get { return medicationId; }
            set { medicationId = value; OnPropertyChanged("MedicationId"); }
        }

        public String Ingredient
        {
            get { return ingredient; }
            set { ingredient = value; OnPropertyChanged("Ingredient"); }
        }

        public String MedicationName {
            get { return medicationName; }
            set { medicationName = value; OnPropertyChanged("Name"); }
        }

        public String Alternative
        {
            get { return medicationAlternative; }
            set { medicationAlternative = value; OnPropertyChanged("Alternative"); }
        }

        public Medication SelectedMedication
        {
            get { return selectedMedication; }
            set { selectedMedication = value; OnPropertyChanged("SelectedMedication"); }
        }

        public ModifyMedication(int medicationId)
        {
            InitializeComponent();
            textBoxName.Focus();
            MedicationRepository medicationRepository = new MedicationRepository();
            MedicationService medicationService = new MedicationService(medicationRepository);
            medicationController = new MedicationController(medicationService);

            selectedMedication = medicationController.GetOneById(medicationId);
            if (selectedMedication is not null)
            {
                MedicationId = selectedMedication.Id;
                MedicationName = selectedMedication.Name;
                Alternative = selectedMedication.Alternative;
                ingredientListToStringList(selectedMedication.Ingredients);
            }
            else
            {
                NavigationService.GoBack();
            }

            this.DataContext = this;
        }


        private void Add_Ingredient_Click(object sender, RoutedEventArgs e)
        {
            if(Ingredient.Length > 0)
            {
                SelectedMedication.Ingredients.Add(Ingredient);
                medicationIngredients.Add(Ingredient);
                Ingredient = "";
            }
        }

        private void Remove_Ingredient_Click(object sender, RoutedEventArgs e)
        {
            SelectedMedication.Ingredients.Remove(SelectedIngredient);
            medicationIngredients.Remove(SelectedIngredient);
        }

        private void ingredientListToStringList(List<String> medicationsIngredients)
        {
            MedicationIngredients = new ObservableCollection<String>();
            foreach (var i in medicationsIngredients)
            {
                MedicationIngredients.Add(i);
            }
        }

        private void Button_Modify_Medication_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                medicationController.Modify(selectedMedication.Id, MedicationName, selectedMedication.Ingredients, Alternative);
                MessageBox.Show("Lek je uspešno modifikovan", "Obaveštenje", MessageBoxButton.OK);
                NavigationService.Navigate(new GetAllUnverifiedMedications());

            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška");
            }
        }


        public void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new MedicationsBeforeModification());
        }
    }
}

