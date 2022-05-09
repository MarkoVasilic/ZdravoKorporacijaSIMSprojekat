using System.Windows;

namespace ZdravoKorporacija.View.ManagerUI.Help
{
    /// <summary>
    /// Interaction logic for CreateRoomHelp.xaml
    /// </summary>
    public partial class CreateRoomHelp : Window
    {
        public CreateRoomHelp()
        {
            InitializeComponent();
        }

        private void Button_Click_OK(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
