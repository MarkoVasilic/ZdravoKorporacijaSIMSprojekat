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
