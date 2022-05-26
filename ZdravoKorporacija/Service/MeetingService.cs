using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class MeetingService
    {
        private readonly MeetingRepository MeetingRepository = new MeetingRepository();
        private readonly DoctorRepository doctorRepository= new DoctorRepository();
        private readonly ManagerRepository managerRepository= new ManagerRepository();
        private readonly SecretaryRepository secretaryRepository= new SecretaryRepository();
        private readonly RoomRepository roomRepository = new RoomRepository();

        public MeetingService()
        {
        }

        public List<Meeting> GetAllMeetings()
        {
            return MeetingRepository.FindAll();
        }

        public List<PossibleMeetingDTO> GetAllMeetingsAsPossibleMeetingsDto()
        {
            List<Meeting> meetings = MeetingRepository.FindAll();
            List<PossibleMeetingDTO> possibleMeetingDtos = new List<PossibleMeetingDTO>();
            foreach (var meet in meetings)
            {
                Room room = roomRepository.FindOneById(meet.RoomId);
                possibleMeetingDtos.Add(new PossibleMeetingDTO(meet.UserJmbgs, createFullNamesOfUser(meet.UserJmbgs),
                    meet.RoomId, room.Name, meet.StartTime, meet.Duration));
            }

            return possibleMeetingDtos;
        }

        private List<String> createFullNamesOfUser(List<String> userJmbgs)
        {
            List<String> userFullNames = new List<string>();
            foreach (var userJmbg in userJmbgs)
            {
                User user = doctorRepository.FindOneByJmbg(userJmbg);
                if (user == null)
                    user = managerRepository.FindOneByJmbg(userJmbg);
                if (user == null)
                    user = secretaryRepository.FindOneByJmbg(userJmbg);
                userFullNames.Add(user.FirstName + " " + user.LastName);
            }

            return userFullNames;
        }
        public void CreateMeeting(List<String> userJmbgs, int roomId, DateTime startTime, int duration)
        {
            int meetingId = GenerateNewId();

            Meeting meeting = new Meeting(meetingId, userJmbgs, roomId, startTime, duration);

            if (!meeting.validateMeeting())
            {
                throw new Exception("Something went wrong, meeting isn't saved");
            }

            MeetingRepository.SaveMeeting(meeting);
        }

        public int GenerateNewId()
        {
            try
            {
                List<Meeting> meetings = MeetingRepository.FindAll();
                int currentMax = meetings.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }
    }
}
