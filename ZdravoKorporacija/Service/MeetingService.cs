using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Interfaces;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class MeetingService
    {
        private readonly IMeetingRepository meetingRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IManagerRepository managerRepository;
        private readonly ISecretaryRepository secretaryRepository;
        private readonly IRoomRepository roomRepository;


        public MeetingService(IMeetingRepository meetingRepository, IDoctorRepository doctorRepository,
            IManagerRepository managerRepository, ISecretaryRepository secretaryRepository,
            IRoomRepository roomRepository)
        {
            this.meetingRepository = meetingRepository;
            this.doctorRepository = doctorRepository;
            this.managerRepository = managerRepository;
            this.secretaryRepository = secretaryRepository;
            this.roomRepository = roomRepository;
        }

        public List<Meeting> GetAllMeetings()
        {
            return meetingRepository.FindAll();
        }

        public List<PossibleMeetingDTO> GetAllMeetingsAsPossibleMeetingsDto()
        {
            List<Meeting> meetings = meetingRepository.FindAll();
            List<PossibleMeetingDTO> possibleMeetingDtos = new List<PossibleMeetingDTO>();
            foreach (var meet in meetings)
            {
                Room room = roomRepository.FindOneById(meet.RoomId);
                possibleMeetingDtos.Add(new PossibleMeetingDTO(meet.UserJmbgs, CreateFullNamesOfUser(meet.UserJmbgs),
                    meet.RoomId, room.Name, meet.StartTime, meet.Duration));
            }

            return possibleMeetingDtos;
        }

        private List<String> CreateFullNamesOfUser(List<String> userJmbgs)
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

            meetingRepository.SaveMeeting(meeting);
        }

        private int GenerateNewId()
        {
            try
            {
                List<Meeting> meetings = meetingRepository.FindAll();
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
