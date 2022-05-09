using System;
using System.Text.RegularExpressions;

namespace ZdravoKorporacija.Model
{
    public class BasicRenovation
    {

        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public String Description { get; set; }


        public BasicRenovation() { }
        public BasicRenovation(int id, int roomId, DateTime startTime, int duration, string description)
        {
            Id = id;
            RoomId = roomId;
            StartTime = startTime;
            Duration = duration;
            Description = description;
        }

        public Boolean validateBasicRenovation()
        {
            Regex descriptionRegex = new Regex("^$|[a-zA-Z]+[a-zA-Z0-9_\\.\\s]*$");
            Regex onlyNumberRegex = new Regex("^[0-9]+$");

            if (Description == null || Description.Length < 3 || !descriptionRegex.IsMatch(Description))
            {
                return false;
            }
            else if (Duration == null || !onlyNumberRegex.IsMatch(Duration.ToString()))
            {
                return false;
            }
            else if (RoomId == null)
            {
                return false;
            }
            else if (StartTime == null || StartTime < DateTime.Now)
            {
                return false;
            }
            else return true;
        }

    }
}
