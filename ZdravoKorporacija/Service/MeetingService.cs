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
        private readonly IRoomRepository roomRepository;
        private readonly ScheduleService scheduleService;
        private readonly UserService userService;

        public MeetingService(IMeetingRepository meetingRepository,
            IRoomRepository roomRepository, ScheduleService scheduleService, UserService userService)
        {
            this.meetingRepository = meetingRepository;
            this.roomRepository = roomRepository;
            this.scheduleService = scheduleService;
            this.userService = userService;
        }

        public List<PossibleMeetingDTO> GetPossibleMeetingAppointments(List<String> userJmbgs, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            scheduleService.ValidateInputParametersForGetPossibleAppointments("*", userJmbgs, roomId, dateFrom, dateUntil);
            List<DateTime>  possibleAppointments =
                scheduleService.FindPossibleStartTimesOfAppointment("", userJmbgs, roomId, dateFrom, dateUntil, duration);
            var possibleMeetingDtos = CreatePossibleMeetingsDtos(userJmbgs, roomId, duration, possibleAppointments);
            return possibleMeetingDtos;
        }

        private List<PossibleMeetingDTO> CreatePossibleMeetingsDtos(List<String> userJmbgs, int roomId, int duration,
            List<DateTime> possibleAppointments)
        {
            List<PossibleMeetingDTO> possibleMeetingDtos = new List<PossibleMeetingDTO>();
            Room selectedRoom = roomRepository.FindOneById(roomId);
            foreach (var pa in possibleAppointments)
            {
                if (pa > DateTime.Now.AddHours(1))
                {
                    PossibleMeetingDTO possibleMeetingDto = new PossibleMeetingDTO(new List<string>(),
                        new List<string>(), selectedRoom.Id, selectedRoom.Name, pa, duration);
                    foreach (var userJmbg in userJmbgs)
                    {
                        var user = userService.CheckUserJmbgExistence(userJmbg);
                        possibleMeetingDto.UserJmbgs.Add(userJmbg);
                        possibleMeetingDto.UserFullNames.Add(user.FirstName + " " + user.LastName);
                    }

                    possibleMeetingDtos.Add(possibleMeetingDto);
                }
            }
            return possibleMeetingDtos;
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
                possibleMeetingDtos.Add(new PossibleMeetingDTO(meet.UserJmbgs, userService.CreateFullNamesOfUser(meet.UserJmbgs),
                    meet.RoomId, room.Name, meet.StartTime, meet.Duration));
            }

            return possibleMeetingDtos;
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
