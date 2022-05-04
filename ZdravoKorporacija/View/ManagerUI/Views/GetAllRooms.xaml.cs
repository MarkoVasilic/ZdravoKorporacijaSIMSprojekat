using Controller;
using Model;
using Repository;
using Service;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ZdravoKorporacija.View.ManagerUI.ViewModels;

namespace ZdravoKorporacija.View.RoomCRUD
{

    public partial class GetAllRooms : Page
    {

        private CreateRoomVM createRoomVM;
        public GetAllRooms()
        {
            InitializeComponent();
            createRoomVM = new CreateRoomVM();
            DataContext = createRoomVM;
      
        }


        private void Button_Click_Logout(object sender, RoutedEventArgs e)
        {

        }

        /*private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerHomePage());
        }*/


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
