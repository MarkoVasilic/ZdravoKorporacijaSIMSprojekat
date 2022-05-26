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
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for ScheduleMeetingPage.xaml
    /// </summary>
    public partial class ScheduleMeetingPage : Page
    {
        private ScheduleMeetingVM scheduleMeetingVM;
        public ScheduleMeetingPage()
        {
            InitializeComponent();
            this.scheduleMeetingVM = new ScheduleMeetingVM();
            DataContext = scheduleMeetingVM;
        }

        private void Search_Meeting_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DurationComboBox.SelectedIndex == 0)
                scheduleMeetingVM.SelectedDuration = 30;
            else if (DurationComboBox.SelectedIndex == 1)
                scheduleMeetingVM.SelectedDuration = 45;
            else if (DurationComboBox.SelectedIndex == 2)
                scheduleMeetingVM.SelectedDuration = 60;
            else if (DurationComboBox.SelectedIndex == 3)
                scheduleMeetingVM.SelectedDuration = 90;
            else if (DurationComboBox.SelectedIndex == 4)
                scheduleMeetingVM.SelectedDuration = 120;
            else if (DurationComboBox.SelectedIndex == 5)
                scheduleMeetingVM.SelectedDuration = 150;
            else if (DurationComboBox.SelectedIndex == 6)
                scheduleMeetingVM.SelectedDuration = 180;
            else if (DurationComboBox.SelectedIndex == 7)
                scheduleMeetingVM.SelectedDuration = 240;
            else if (DurationComboBox.SelectedIndex == 8)
                scheduleMeetingVM.SelectedDuration = 300;
            else if (DurationComboBox.SelectedIndex == 9)
                scheduleMeetingVM.SelectedDuration = 360;
            else if (DurationComboBox.SelectedIndex == 10)
                scheduleMeetingVM.SelectedDuration = 420;
            else
                scheduleMeetingVM.SelectedDuration = 0;
        }
    }
}
