using System;
using System.Collections.Generic;

namespace ZdravoKorporacija.DTO
{
    public class PossibleMeetingDTO
    {
        public List<String> UserJmbgs { get; set; }
        public List<String> UserFullNames { get; set; }
        public int RoomId { get; set; }
        public String RoomName { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }

        public PossibleMeetingDTO(List<String> userJmbgs, List<String> userFullNames, int roomId, String roomName, DateTime startTime, int duration)
        {
            this.UserJmbgs = userJmbgs;
            this.UserFullNames = userFullNames;
            this.RoomId = roomId;
            this.RoomName = roomName;
            this.StartTime = startTime;
            this.Duration = duration;
        }

        public PossibleMeetingDTO()
        {
            this.UserJmbgs = new List<String>();
            this.UserFullNames = new List<String>();
        }
    }
}
