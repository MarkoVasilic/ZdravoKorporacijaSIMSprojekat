using System;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class MeetingControler
    {
        private readonly MeetingService MeetingService = new MeetingService();

        public MeetingControler()
        {
        }

        public List<Meeting> GetAllMeetings()
        {
            return MeetingService.GetAllMeetings();
        }

        public List<PossibleMeetingDTO> GetAllMeetingsAsPossibleMeetingsDto()
        {
            return MeetingService.GetAllMeetingsAsPossibleMeetingsDto();
        }

        public void CreateMeeting(List<String> userJmbgs, int roomId, DateTime startTime, int duration)
        {
            MeetingService.CreateMeeting(userJmbgs, roomId, startTime, duration);
        }


    }
}
