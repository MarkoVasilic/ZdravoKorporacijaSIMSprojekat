using System;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class MeetingControler
    {
        private readonly MeetingService _meetingService;

        public MeetingControler(MeetingService meetingService)
        {
            this._meetingService = meetingService;
        }

        public List<Meeting> GetAllMeetings()
        {
            return _meetingService.GetAllMeetings();
        }

        public List<PossibleMeetingDTO> GetAllMeetingsAsPossibleMeetingsDto()
        {
            return _meetingService.GetAllMeetingsAsPossibleMeetingsDto();
        }

        public void CreateMeeting(List<String> userJmbgs, int roomId, DateTime startTime, int duration)
        {
            _meetingService.CreateMeeting(userJmbgs, roomId, startTime, duration);
        }

        public List<PossibleMeetingDTO> GetPossibleMeetingAppointments(List<String> userJmbgs, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return _meetingService.GetPossibleMeetingAppointments(userJmbgs, roomId, dateFrom, dateUntil, duration);
        }
    }
}
