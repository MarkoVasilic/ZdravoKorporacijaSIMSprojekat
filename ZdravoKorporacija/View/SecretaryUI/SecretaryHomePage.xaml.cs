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

namespace ZdravoKorporacija.View.SecretaryUI
{
    public partial class SecretaryHomePage : Page
    {
        public SecretaryHomePage()
        {
            InitializeComponent();
        }

        private void Patient_accounts_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientsView());
        }
    }
}
