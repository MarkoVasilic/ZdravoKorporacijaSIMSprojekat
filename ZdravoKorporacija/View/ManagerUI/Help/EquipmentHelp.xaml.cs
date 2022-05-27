using System.Windows;

namespace ZdravoKorporacija.View.ManagerUI.Help
{
    /// <summary>
    /// Interaction logic for EquipmentHelp.xaml
    /// </summary>
    public partial class EquipmentHelp : Window
    {
        public EquipmentHelp()
        {
            InitializeComponent();
        }

        private void Button_Click_OK(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
