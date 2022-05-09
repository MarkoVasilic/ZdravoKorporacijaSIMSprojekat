using System.Windows;

namespace ZdravoKorporacija.View.ManagerUI.Help
{
    /// <summary>
    /// Interaction logic for DeleteRoomHelp.xaml
    /// </summary>
    public partial class DeleteRoomHelp : Window
    {
        public DeleteRoomHelp()
        {
            InitializeComponent();
        }

        private void Button_Click_OK(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
