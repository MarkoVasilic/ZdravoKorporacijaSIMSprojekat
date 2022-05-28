using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ZdravoKorporacija.View.PatientUI
{
    /// <summary>
    /// Interaction logic for PatientInfoPage.xaml
    /// </summary>
    public partial class PatientInfoPage : Page
    {
        public PatientInfoPage()
        {
            InitializeComponent();
        }

        private void ProfilNazadButton(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
