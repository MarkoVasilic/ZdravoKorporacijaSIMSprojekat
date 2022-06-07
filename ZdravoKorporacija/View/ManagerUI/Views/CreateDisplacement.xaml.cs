using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI
{
    /// <summary>
    /// Interaction logic for CreateDisplacement.xaml
    /// </summary>
    public partial class CreateDisplacement : Page
    {
        public int startRoomId { get; set; }
        public int checkedEndRoom { get; set; }
        public int equipmentId { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
        private RoomController roomController;
        //private EquipmentController equipmentController;
        private DisplacementController displacementController;
        public DateTime displacementDate;
        public String errorMessage;


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public String ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public DateTime DisplacementDate
        {
            get { return displacementDate; }
            set { displacementDate = value;
                OnPropertyChanged("DisplacementDate");
                }
        }

        public CreateDisplacement(int startRoom, int equipment)
        {
            InitializeComponent();
            startRoomId = startRoom;
            equipmentId = equipment;
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            this.DataContext = this;
            Rooms = new ObservableCollection<Room>(roomController.GetAllExceptOne(startRoom));
            EquipmentRepository equipmentRepository = new EquipmentRepository();
            DisplacementRepository displacementRepository = new DisplacementRepository();
            DisplacementService displacementService = new DisplacementService(displacementRepository, equipmentRepository, roomRepository);
            displacementController = new DisplacementController(displacementService);

            
        }

        private void CreateDisplacement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                displacementController.Create(startRoomId, checkedEndRoom, equipmentId, DisplacementDate);
                MessageBox.Show("Premeštanje opreme je uspešno zakazano!", "Obaveštenje", MessageBoxButton.OK);
                NavigationService.Navigate(new ManagerHomePage());

            }catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                MessageBox.Show(ErrorMessage, "Greška");
            }
        }

        public void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new EquipmentInRoom(startRoomId));
        }

        private void RadioButtonList_Checked(object sender, RoutedEventArgs e)
        {
            int id = (int)((RadioButton)sender).Tag;

            foreach (Room r in Rooms)
            {
                if (r.Id == id)
                    checkedEndRoom = id;
            }



        }

    }
}
