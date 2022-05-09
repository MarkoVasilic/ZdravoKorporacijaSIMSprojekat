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
    /// Interaction logic for EmergencyPatientInformations.xaml
    /// </summary>
    public partial class EmergencyPatientInformations : Page
    {
        private ScheduleEmergencyVM scheduleEmergencyVM;
        public EmergencyPatientInformations(ScheduleEmergencyVM scheduleEmergencyVM)
        {
            InitializeComponent();
            this.scheduleEmergencyVM = scheduleEmergencyVM;
            DataContext = scheduleEmergencyVM;
        }
    }
}
