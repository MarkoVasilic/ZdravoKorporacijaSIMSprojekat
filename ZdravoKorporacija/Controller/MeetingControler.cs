using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void CreateMeeting(List<String> userJmbgs, int roomId, DateTime startTime, int duration)
        {
            MeetingService.CreateMeeting(userJmbgs, roomId, startTime, duration);
        }


    }
}
