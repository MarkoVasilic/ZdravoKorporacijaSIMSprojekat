using System.Windows;

namespace ZdravoKorporacija.View.ManagerUI.Help
{
    /// <summary>
    /// Interaction logic for ModifyRoomHelp.xaml
    /// </summary>
    public partial class ModifyRoomHelp : Window
    {
        public ModifyRoomHelp()
        {
            InitializeComponent();
        }

        private void Button_Click_OK(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
