using System.Windows;
using ZdravoKorporacija.View.DoctorUI.ViewModel;


namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for DoctorHomeWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        public DoctorWindow()
        {
            InitializeComponent();
            this.DataContext = new DoctorWindowVM(this);
        }
    }
}
