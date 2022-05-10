﻿using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for OrderEquipmentPage.xaml
    /// </summary>
    public partial class OrderEquipmentPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public EquipmentController EquipmentController { get; set; }
        public SecretaryWindowVM secretaryWindowVM { get; set; }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
        public OrderEquipmentPage(SecretaryWindowVM secretaryWindowVM)
        {
            InitializeComponent();
            this.DataContext = this;
            secretaryWindowVM = secretaryWindowVM;
            SecretaryWindowVM.setWindowTitle("Order equipment");
            EquipmentRepository equipmentRepository = new EquipmentRepository();
            RoomRepository roomRepository = new RoomRepository();
            DisplacementRepository displacementRepository = new DisplacementRepository();
            EquipmentService equipmentService = new EquipmentService(equipmentRepository, roomRepository, displacementRepository);
            EquipmentController = new EquipmentController(equipmentService);
        }

        private void Order_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DynamicEquimpentComboBox.SelectedValue == null || DynamicEquimpentComboBox.SelectedValue.ToString().Length < 1)
                ErrorMessage = "Select equipment you want to order!";
            else if(MyUpDownControl.Value == null)
                ErrorMessage = "Select quantity of equipment you want to order!";
            else if(MyUpDownControl.Value <= 0)
                ErrorMessage = "Quantity must be positive!";
            else
            {
                ErrorMessage = "";
                try
                {
                    EquipmentController.CreateEquipment(DynamicEquimpentComboBox.SelectedValue.ToString(), false, MyUpDownControl.Value, null, DateTime.Now.AddDays(3));
                    NavigationService.Navigate(new SecretaryHomePage(secretaryWindowVM));
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                }
            }
        }
    }
}