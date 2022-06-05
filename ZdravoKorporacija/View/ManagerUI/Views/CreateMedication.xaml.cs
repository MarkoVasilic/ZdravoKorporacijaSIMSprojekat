using System.Windows.Controls;
using System.Windows.Input;
using ZdravoKorporacija.View.ManagerUI.Help;
using ZdravoKorporacija.View.ManagerUI.ViewModels;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for CreateMedication.xaml
    /// </summary>
    public partial class CreateMedication : Page
    {
        public CreateMedication()
        {
            InitializeComponent();
            this.DataContext = new CreateMedicationVM();
            textBoxName.Focus();
        }

        private void Button_Create_Medication_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void CreateRoomHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CreateRoomHelp_Executed(object sender, ExecutedRoutedEventArgs e)
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
            NavigationService.Navigate(new ManagerHomePage());
        }
    }
}
