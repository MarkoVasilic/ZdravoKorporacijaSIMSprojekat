using System.Windows.Controls;
using System.Windows.Input;
using ZdravoKorporacija.View.ManagerUI.Help;
using ZdravoKorporacija.View.ManagerUI.ViewModels;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for CreateMedication.xaml
    /// </summary>
    public partial class CreateMedication : Page
    {
        public CreateMedication()
        {
            InitializeComponent();
            this.DataContext = new CreateMedicationVM(this);
            textBoxName.Focus();
            
        }
    }
}
