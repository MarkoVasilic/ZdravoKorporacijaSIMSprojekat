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
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for DeleteRoom.xaml
    /// </summary>
    public partial class DeleteRoom : Page
    {
        public DeleteRoom()
        {
            InitializeComponent();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerHomePage());
        }


        private void DeleteRoomClick (object sender, RoutedEventArgs e)
        {
            
        }
    }

}
