using System;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class MeetingControler
    {
        private readonly MeetingService MeetingService;

        public MeetingControler(MeetingService meetingService)
        {
            this.MeetingService = meetingService;
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

        public List<PossibleMeetingDTO> GetPossibleMeetingAppointments(List<String> userJmbgs, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return MeetingService.GetPossibleMeetingAppointments(userJmbgs, roomId, dateFrom, dateUntil, duration);
        }
    }
}
