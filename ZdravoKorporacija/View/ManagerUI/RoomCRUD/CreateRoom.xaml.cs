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
using System.Windows.Shapes;
using ZdravoKorporacija.View.ManagerUI.ViewModels;

namespace ZdravoKorporacija.View.ManagerUI
{
    /// <summary>
    /// Interaction logic for AddRoom.xaml
    /// </summary>
    public partial class CreateRoom : Window
    {

        private CreateRoomVM createRoomVM;
        public CreateRoom()
        {
            InitializeComponent();
            createRoomVM = new CreateRoomVM();
            this.DataContext = createRoomVM;
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

        }
    }
}
