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
using System.Windows.Shapes;
using Controller;
using Service;
using Repository;

namespace ZdravoKorporacija.View.RoomCRUD
{
    /// <summary>
    /// Interaction logic for DeleteRoom.xaml
    /// </summary>
    public partial class DeleteRoom : Window
    {

        private RoomController roomController;
        private int roomId;

        public DeleteRoom()
        {
            InitializeComponent();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
        }

        private void DeleteRoomClick(object sender, RoutedEventArgs e)
        {
      
            roomId = int.Parse(textBoxDeleteRoom.Text);
            roomController.DeleteRoom(roomId);
            this.Close();

        }
    }
}
