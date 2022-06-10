using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
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
        public CreateRoom()
        {
            InitializeComponent();
            createRoomVM = new CreateRoomVM(this);
            this.DataContext = createRoomVM;
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




    }
}
