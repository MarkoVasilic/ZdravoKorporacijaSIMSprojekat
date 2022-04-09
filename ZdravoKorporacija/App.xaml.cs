using Controller;
using Repository;
using Service;
using Model;
using System.Windows;
using System;
using System.Diagnostics;

namespace ZdravoKorporacija
{
    public partial class App : Application
    {
        public PatientController? patientController { get; set; }
        public RoomController? roomController { get; set; }

        public App()
        {
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            RoomController roomController = new RoomController(roomService);
            //Room room1 = new Room("soba1", 1, "neka soba", RoomType.EXAMINATION);
            //roomController.CreateRoom(room1);
            //room1.name = "konj";
            //roomController.ModifyRoom(room1);
            //roomController.DeleteRoom(room1.id);
            //Trace.WriteLine(roomController.FindRoomByName(room1.name).description);


        }

    }
}
