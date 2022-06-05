using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ZdravoKorporacija.View.ManagerUI.Help;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for MedicationsBeforeModification.xaml
    /// </summary>
    public partial class MedicationsBeforeModification : Page
    {

        private int checkedMedicationId;

        public ObservableCollection<Medication> medications { get; set; }

        private MedicationController medicationController;

        public MedicationsBeforeModification()
        {
            InitializeComponent();
            MedicationRepository medicationRepository = new MedicationRepository();
            MedicationService medicationService = new MedicationService(medicationRepository);
            medicationController = new MedicationController(medicationService);
            this.DataContext = this;
            medications = new ObservableCollection<Medication>(medicationController.GetAllRejected());
        }

        private void ModifyButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new ModifyMedication(checkedMedicationId));
        }

        private void ModifyRoomHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ModifyRoomHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ModifyMedicationHelp modifyMedicationHelp = new ModifyMedicationHelp();
            modifyMedicationHelp.Show();
        }

        public void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerHomePage());
        }

        private void RadioButtonList_Checked(object sender, RoutedEventArgs e)
        {
            
            int id = (int)((RadioButton)sender).Tag;

            foreach (Medication m in medications)
            {
                if (m.Id == id)
                    checkedMedicationId = id;
            }
        }

        public void GetNotifications_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GetNotifications_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NotificationWindow notificationWindow = new NotificationWindow();
            notificationWindow.Show();
        }

        public void NotificationsClick(object sender, RoutedEventArgs e)
        {
            NotificationWindow notificationWindow = new NotificationWindow();
            notificationWindow.Show();
        }
    }
}
