﻿using System;
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
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class CreateAbsenceRequest : Page
    {
        public CreateAbsenceRequestVM createAbsenceRequestVm;
        public CreateAbsenceRequest()
        {
            InitializeComponent();
            createAbsenceRequestVm = new CreateAbsenceRequestVM();
            this.DataContext = createAbsenceRequestVm;
        }


        private void ConfirmAbsenceRequest_OnClick(object sender, RoutedEventArgs e)
        {
            if (UrgentCheckBox.IsChecked == true)
                createAbsenceRequestVm.urgentChecked(true);
            else
                createAbsenceRequestVm.urgentChecked(false);
        }
    }
}
