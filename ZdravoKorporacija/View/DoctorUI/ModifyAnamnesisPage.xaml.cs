﻿using System.Windows.Controls;
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for ModifyAnamnesisPage.xaml
    /// </summary>
    public partial class ModifyAnamnesisPage : Page
    {
        public ModifyAnamnesisPage(int id)
        {
            InitializeComponent();
            this.DataContext = new ModifyAnamnesisVM(id);
        }
    }
}
