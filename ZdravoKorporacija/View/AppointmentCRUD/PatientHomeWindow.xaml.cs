using System.Windows;
using ZdravoKorporacija.View.AppointmentCRUD;
using ZdravoKorporacija.View.AppointmentCRUD.ViewModels;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for PacijentHomePage.xaml
    /// </summary>
    public partial class PatientHomeWindow : Window
    {
        public PatientHomeWindow()
        {
            InitializeComponent();
            DataContext = new PatientWindowVM(this);
        }

       
    }
}
