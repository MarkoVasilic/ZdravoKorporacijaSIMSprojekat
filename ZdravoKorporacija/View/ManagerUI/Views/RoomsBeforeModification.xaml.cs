﻿using Controller;
using Model;
using Repository;
using Service;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKorporacija.View.ManagerUI.Help;

namespace ZdravoKorporacija.View.RoomCRUD
{
    public partial class RoomsBeforeModification : Page
    {

        private RoomController roomController;
        public ObservableCollection<Room> rooms { get; set; }
        public int checkedRoomId     { get; set; }
        public RoomsBeforeModification()
        {
            InitializeComponent();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            this.DataContext = this;
            rooms = new ObservableCollection<Room>(roomController.GetAllRooms());
        }


        private void ModifyButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ModifyRoom(checkedRoomId));
        }

        private void RadioButtonList_Checked(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(((RadioButton)sender).Tag);
            int id = (int)((RadioButton)sender).Tag ;

            foreach(Room r in rooms)
            {
                if (r.Id == id)
                    checkedRoomId = id;
            }

            //Console.WriteLine(checkedRoomId);


        }

        private void Button_Click_Logout(object sender, RoutedEventArgs e)
        {

        }

        /*private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerHomePage());
        }*/

        private void ModifyRoomHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ModifyRoomHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ModifyRoomHelp modifyRoomHelp  = new ModifyRoomHelp();
            modifyRoomHelp.Show();
        }

        public void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerHomePage());
        }
    }
}