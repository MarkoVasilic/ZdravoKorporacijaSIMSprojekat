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
    /// Interaction logic for AbsceneRequestsPage.xaml
    /// </summary>
    public partial class AbsceneRequestsPage : Page
    {
        public AbsceneRequestsPage()
        {
            InitializeComponent();
            this.DataContext = new AbsceneRequestsVM();
        }
    }
}
