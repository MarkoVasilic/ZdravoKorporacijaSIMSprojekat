using Model;
using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for AddNewPatientPage.xaml
    /// </summary>
    public partial class AddNewPatientPage : Page
    {
        private AddAccountVM addAccountVM;
        public AddNewPatientPage()
        {
            InitializeComponent();
            addAccountVM = new AddAccountVM();
            this.DataContext = addAccountVM;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Male.IsChecked == true)
                addAccountVM.setPatientGenderMale((BloodType)BloodTypeComboBox.SelectedIndex);
            else if (Female.IsChecked == true)
                addAccountVM.setPatientGenderFemale((BloodType)BloodTypeComboBox.SelectedIndex);
            else
                addAccountVM.setBloodType((BloodType)BloodTypeComboBox.SelectedIndex);
        }
    }
}