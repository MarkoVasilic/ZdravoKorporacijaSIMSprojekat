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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKorporacija.View.PatientUI.ViewModels;

namespace ZdravoKorporacija.View.PatientUI
{
    /// <summary>
    /// Interaction logic for CreateNotificationPage.xaml
    /// </summary>
    public partial class CreateNotificationPage : Page
    {
        public CreateNotificationPage()
        {
            InitializeComponent();
            DataContext = new CreateNotificationPageVM();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
