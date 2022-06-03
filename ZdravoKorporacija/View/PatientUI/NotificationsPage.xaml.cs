using Repository;
using Service;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.PatientUI.ViewModels;

namespace ZdravoKorporacija.View.PatientUI
{
    /// <summary>
    /// Interaction logic for NotificationsPage.xaml
    /// </summary>
    public partial class NotificationsPage : Page
    {

        public NotificationsPage()
        {
            InitializeComponent();
            DataContext = new NotificationsPageVM();

        }


    }
}
