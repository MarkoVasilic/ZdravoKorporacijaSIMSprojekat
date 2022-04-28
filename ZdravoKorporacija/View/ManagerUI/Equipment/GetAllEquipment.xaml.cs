﻿using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.Equipment
{
    public partial class GetAllEquipment : Window
    {
        private EquipmentController equipmentController;

        public ObservableCollection<EquipmentDTO> equipment { get; set; }
        public GetAllEquipment()
        {
            InitializeComponent();
            EquipmentRepository equipmentRepository = new EquipmentRepository();
            RoomRepository roomRepository = new RoomRepository();
            DisplacementRepository displacementRepository = new DisplacementRepository();   
            EquipmentService equipmentService = new EquipmentService(equipmentRepository, roomRepository, displacementRepository);
            equipmentController = new EquipmentController(equipmentService);
            this.DataContext = this;
            equipment = new ObservableCollection<EquipmentDTO>(equipmentController.GetEquipmentDTOs());


        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            ManagerHomePage managerHomePage = new ManagerHomePage();
            managerHomePage.Show();
        }
    }
}