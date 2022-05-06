using Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKorporacija.View.ManagerUI.Help;
using ZdravoKorporacija.View.ManagerUI.ViewModels;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for CreateRoom.xaml
    /// </summary>
    public partial class CreateRoom : Page
    {

        private CreateRoomVM createRoomVM;
        private ManagerHomeWindow managerHomeWindow;
        public CreateRoom()
        {
            InitializeComponent();
            createRoomVM = new CreateRoomVM();
            this.DataContext = createRoomVM;
            managerHomeWindow = new ManagerHomeWindow();
            textBoxName.Focus();
        }

        public void Button_Create_Room_Click(object sender, RoutedEventArgs e)
        {
            if (RoomTypeComboBox.SelectedIndex == 0)
            {
                createRoomVM.setRoomType(RoomType.EXAMINATION);
            }
            else if (RoomTypeComboBox.SelectedIndex == 1)
            {
                createRoomVM.setRoomType(RoomType.CONFERENCE);
            }
            else if (RoomTypeComboBox.SelectedIndex == 2)
            {
                createRoomVM.setRoomType(RoomType.SURGERY);
            }
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerHomePage());
        }

        private void Button_Click_Logout(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            managerHomeWindow.Close();
            mainWindow.Show();
        }

       /* private void ButtonHelp(object sender, RoutedEventArgs e)
        {
           CreateRoomHelp createRoomHelp = new CreateRoomHelp();
            createRoomHelp.Show();
        }*/

        private void CreateRoomHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CreateRoomHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CreateRoomHelp createRoomHelp = new CreateRoomHelp();
            createRoomHelp.Show();
        }

        private void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Console.WriteLine("Nadja");
            NavigationService.Navigate(new ManagerHomePage());
        }

       
    }
}
