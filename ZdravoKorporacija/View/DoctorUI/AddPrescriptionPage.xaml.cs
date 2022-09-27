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
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for AddPrescriptionPage.xaml
    /// </summary>
    public partial class AddPrescriptionPage : Page
    {
        private String patientJmbg { get; set; }
        public AddPrescriptionPage(String jmbg, String medication)
        {
            InitializeComponent();
            this.patientJmbg = jmbg;
            this.DataContext = new AddPrescriptionVM(patientJmbg, medication);
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChooseMedicationPage(patientJmbg));
        }
    }
}
