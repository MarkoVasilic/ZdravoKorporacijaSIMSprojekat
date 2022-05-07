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
using System.Windows.Shapes;
using ZdravoKorporacija.View.ManagerUI.ViewModels;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for ManagerHomeWindow.xaml
    /// </summary>
    public partial class ManagerHomeWindow : Window
    {
        public ManagerHomeWindow()
        {

            InitializeComponent();
            DataContext = new ManagerWindowVM(this);
      
        }

        private void Button_Click_Logout(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }
    }
}
