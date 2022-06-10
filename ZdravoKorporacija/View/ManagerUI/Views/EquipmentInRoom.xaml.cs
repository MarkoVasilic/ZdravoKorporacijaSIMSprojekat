using Repository;
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
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.ManagerUI.Views;

namespace ZdravoKorporacija.View.ManagerUI
{
    /// <summary>
    /// Interaction logic for EquipmentInRoom.xaml
    /// </summary>
    public partial class EquipmentInRoom : Page
    {
        private EquipmentController equipmentController;
        public ObservableCollection<EquipmentDTO> Equipment { get; set; }
        public int checkedEquipment { get; set; }
        public int startRoom { get; set; }
        

        public EquipmentInRoom(int roomId)
        {
            InitializeComponent();
            EquipmentRepository equipmentRepository = new EquipmentRepository();
            RoomRepository roomRepository = new RoomRepository();
            EquipmentService equipmentService = new EquipmentService(equipmentRepository, roomRepository);
            equipmentController = new EquipmentController(equipmentService);
            startRoom = roomId;
            this.DataContext = this;
            Equipment = new ObservableCollection<EquipmentDTO>(equipmentController.GetAllByRoomId(roomId));
        }

        private void ChooseEquipment_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateDisplacement(startRoom, checkedEquipment));
        }


        private void RadioButtonList_Checked(object sender, RoutedEventArgs e)
        {
            int id = (int)((RadioButton)sender).Tag;

            foreach (EquipmentDTO eq in Equipment)
            {
                if (eq.Id == id)
                    checkedEquipment = id;
            }

        }


        public void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new EquipmentDisplacement());
        }

    }
}
