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
            createRoomVM = new CreateRoomVM(this);
            DataContext = createRoomVM;

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
