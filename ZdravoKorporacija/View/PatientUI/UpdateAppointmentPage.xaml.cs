using Controller;
using Repository;
using Service;
using System;
using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.View.PatientUI;
using ZdravoKorporacija.View.PatientUI.ViewModels;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for UpdateAppointmentPage.xaml
    /// </summary>
    public partial class UpdateAppointmentPage : Page
    {
        private UpdateAppointmentVM updateAppointmentVM;
        public UpdateAppointmentPage(UpdateAppointmentVM updateAppointmentVM)
        {
            InitializeComponent();
            this.updateAppointmentVM = updateAppointmentVM;
            DataContext = updateAppointmentVM;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
