using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for EquipmentDisplacement.xaml
    /// </summary>
    public partial class EquipmentDisplacement : Page
    {

        private RoomController roomController;
        public ObservableCollection<Room> Rooms { get; set; }
        public int checkedRoomId { get; set; }



        public EquipmentDisplacement()
        {
            InitializeComponent();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            this.DataContext = this;
            Rooms = new ObservableCollection<Room>(roomController.GetAllRooms());
        }


        private void RadioButtonList_Checked(object sender, RoutedEventArgs e)
        {
            int id = (int)((RadioButton)sender).Tag;

            foreach (Room r in Rooms)
            {
                if (r.Id == id)
                    checkedRoomId = id;
            }



        }

        public void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerHomePage());
        }


        public void EquipmentDisplacementHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void EquipmentDisplacementHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DisplacementHelp displacementHelp = new DisplacementHelp();
            displacementHelp.Show();
        }

    

        private void ChooseRoom_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(checkedRoomId);
            NavigationService.Navigate(new EquipmentInRoom(checkedRoomId));
        }
    }



   
}
