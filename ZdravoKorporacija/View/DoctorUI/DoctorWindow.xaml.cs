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
using System.Windows.Shapes;
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
            DataContext = new DoctorWindowVM(this);
        }
    }
}
