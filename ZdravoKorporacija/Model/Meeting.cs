using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ZdravoKorporacija.Model
{
    public class Meeting
    {
        public int Id { get; set; }
        public List<String> UserJmbgs { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }

        public Meeting(int id, List<String> userJmbgs, int roomId, DateTime startTime, int duration)
        {
            this.Id = id;
            this.UserJmbgs = userJmbgs;
            this.RoomId = roomId;
            this.StartTime = startTime;
            this.Duration = duration;
        }

        public Meeting()
        {
            this.UserJmbgs = new List<String>();
        }

        public Boolean validateMeeting()
        {
            Regex onlyNumberRegex = new Regex("^[0-9]+$");

            if (Duration == null || !onlyNumberRegex.IsMatch(Duration.ToString()))
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
            else if (UserJmbgs == null || UserJmbgs.Count < 1)
            {
                return false;
            }
            else return true;
        }
    }
}
