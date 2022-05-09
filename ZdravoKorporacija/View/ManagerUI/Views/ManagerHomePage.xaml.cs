using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.View.Equipment;
using ZdravoKorporacija.View.ManagerUI.Views;

namespace ZdravoKorporacija.View.RoomCRUD
{

    public partial class ManagerHomePage : Page

    {

        public ManagerHomePage()
        {
            InitializeComponent();
        }

        private void CreateRoomClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateRoom());
        }

        private void DeleteRoomClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeleteRoom());
        }

        private void GetAllRoomsClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GetAllRooms());
        }

        private void ModifyRoomClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsBeforeModification());
        }

        private void GetEquipmentClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GetAllEquipment());
        }

        private void EquipmentDisplacementClick(object sender, RoutedEventArgs e)
        {

        }

        private void CreateMedicationClick(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyMedicationClick(object sender, RoutedEventArgs e)
        {

        }

        private void RoomsRenovationClick(object sender, RoutedEventArgs e)
        {

        }

        private void ProfileClick(object sender, RoutedEventArgs e)
        {

        }

        private void GradesClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
