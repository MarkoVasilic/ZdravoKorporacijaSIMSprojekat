using Model;
using System;
using System.Text.RegularExpressions;

namespace ZdravoKorporacija.Model
{
    public class AdvancedRenovationJoining
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public int FirstStartRoom { get; set; }
        public int SecondStartRoom { get; set; }
        public String ResultRoomName { get; set; }
        public String ResultRoomDescription { get; set; }
        public RoomType ResultRoomType { get; set; }

        public AdvancedRenovationJoining(int id, int firstStartRoom, int secondStartRoom, DateTime startTime, int duration, string resultRoomName, string resultRoomDescription, RoomType resultRoomType)
        {
            Id = id;
            this.FirstStartRoom = firstStartRoom;
            this.SecondStartRoom = secondStartRoom;
            this.StartTime = startTime;
            this.Duration = duration;
            this.ResultRoomName = resultRoomName;
            this.ResultRoomDescription = resultRoomDescription;
            this.ResultRoomType = resultRoomType;
        }


        public Boolean validate()
        {
            Regex onlyNumberRegex = new Regex("^[0-9]+$");

            if (Duration == null || !onlyNumberRegex.IsMatch(Duration.ToString()))
            {
                return false;
            }
            else if (FirstStartRoom == null)
            {
                return false;
            }
            else if (SecondStartRoom == null)
            {
                return false;
            }
            else if (StartTime == null || StartTime < DateTime.Now)
            {
                return false;
            }
            else if (ResultRoomName == null || ResultRoomName.Length < 2)
            {
                return false;
            }
            else if (ResultRoomDescription == null || ResultRoomDescription.Length < 2)
            {
                return false;
            }
            else if (ResultRoomType == null)
            {
                return false;
            }
            else return true;
        }
    }
}
