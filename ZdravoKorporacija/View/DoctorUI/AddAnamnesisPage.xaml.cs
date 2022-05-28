using System;
using System.Windows.Controls;
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for AddAnamnesis.xaml
    /// </summary>
    public partial class AddAnamnesisPage : Page
    {
        public AddAnamnesisPage(String Jmbg)
        {
            InitializeComponent();
            this.DataContext = new AddAnamnesisVM(Jmbg);
        }
    }
}
