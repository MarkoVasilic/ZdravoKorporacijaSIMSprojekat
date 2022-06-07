using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for ProfilePreview.xaml
    /// </summary>
    public partial class ProfilePreview : Page
    {

        private ManagerController managerController; 
        private Manager manager { get; set; }
        public ProfilePreview()
        {
            InitializeComponent();
            manager = App.managerController.GetOneManager(App.loggedUser.Jmbg);

            
            FirstName.Text = manager.FirstName;
            LastName.Text = manager.LastName;
            Address.Text = manager.Address;
            Email.Text = manager.Email;
            Username.Text = manager.Username;
            Password.Password = manager.Password;
            PhoneNumber.Text = manager.PhoneNumber;
            DateOfBirth.SelectedDate = manager.DateOfBirth;
            manager = App.managerController.GetOneManager(App.loggedUser.Jmbg);

        }

        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.managerController.ModifyManager(FirstName.Text, LastName.Text, DateOfBirth.SelectedDate, Email.Text, PhoneNumber.Text, Address.Text, Username.Text, Password.Password, App.loggedUser.Jmbg);
            MessageBox.Show("Uspešna izmena!");
            manager = App.managerController.GetOneManager(manager.Jmbg);
            FirstName.Text = manager.FirstName;
            LastName.Text = manager.LastName;
            Address.Text = manager.Address;
            Email.Text = manager.Email;
            Username.Text = manager.Username;
            Password.Password = manager.Password;
            PhoneNumber.Text = manager.PhoneNumber;
            DateOfBirth.SelectedDate = manager.DateOfBirth;
            NavigationService.Refresh();

        }


        public void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerHomePage());
        }
    }
}
