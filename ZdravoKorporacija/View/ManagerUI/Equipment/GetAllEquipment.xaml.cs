using Repository;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.Equipment
{
    public partial class GetAllEquipment : Page
    {
        private EquipmentController equipmentController;

        public ObservableCollection<EquipmentDTO> equipment { get; set; }
        public GetAllEquipment()
        {
            InitializeComponent();
            EquipmentRepository equipmentRepository = new EquipmentRepository();
            RoomRepository roomRepository = new RoomRepository();
            DisplacementRepository displacementRepository = new DisplacementRepository();
            EquipmentService equipmentService = new EquipmentService(equipmentRepository, roomRepository, displacementRepository);
            equipmentController = new EquipmentController(equipmentService);
            this.DataContext = this;
            equipment = new ObservableCollection<EquipmentDTO>(equipmentController.GetEquipmentDTOs());


        }


        /*private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerHomePage());
        }*/

        private void Button_Click_Logout(object sender, RoutedEventArgs e)
        {

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
