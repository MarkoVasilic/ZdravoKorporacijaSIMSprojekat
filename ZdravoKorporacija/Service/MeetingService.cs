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
        private readonly IMeetingRepository _meetingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ScheduleService _scheduleService;
        private readonly UserService _userService;

        public MeetingService(IMeetingRepository meetingRepository,
            IRoomRepository roomRepository, ScheduleService scheduleService, UserService userService)
        {
            this._meetingRepository = meetingRepository;
            this._roomRepository = roomRepository;
            this._scheduleService = scheduleService;
            this._userService = userService;
        }

        public List<PossibleMeetingDTO> GetPossibleMeetingAppointments(List<String> userJmbgs, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            _scheduleService.ValidateInputParametersForGetPossibleAppointments("*", userJmbgs, roomId, dateFrom, dateUntil);
            List<DateTime> possibleAppointments =
                _scheduleService.FindPossibleStartTimesOfAppointment("", userJmbgs, roomId, dateFrom, dateUntil, duration);
            var possibleMeetingDtos = CreatePossibleMeetingsDtos(userJmbgs, roomId, duration, possibleAppointments);
            return possibleMeetingDtos;
        }

        private List<PossibleMeetingDTO> CreatePossibleMeetingsDtos(List<String> userJmbgs, int roomId, int duration,
            List<DateTime> possibleAppointments)
        {
            List<PossibleMeetingDTO> possibleMeetingDtos = new List<PossibleMeetingDTO>();
            Room selectedRoom = _roomRepository.FindOneById(roomId);
            foreach (var pa in possibleAppointments)
            {
                if (pa > DateTime.Now.AddHours(1))
                {
                    PossibleMeetingDTO possibleMeetingDto = new PossibleMeetingDTO(new List<string>(),
                        new List<string>(), selectedRoom.Id, selectedRoom.Name, pa, duration);
                    foreach (var userJmbg in userJmbgs)
                    {
                        var user = _userService.CheckUserJmbgExistence(userJmbg);
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
            return _meetingRepository.FindAll();
        }

        public List<PossibleMeetingDTO> GetAllMeetingsAsPossibleMeetingsDto()
        {
            List<Meeting> meetings = _meetingRepository.FindAll();
            List<PossibleMeetingDTO> possibleMeetingDtos = new List<PossibleMeetingDTO>();
            foreach (var meet in meetings)
            {
                Room room = _roomRepository.FindOneById(meet.RoomId);
                possibleMeetingDtos.Add(new PossibleMeetingDTO(meet.UserJmbgs, _userService.CreateFullNamesOfUser(meet.UserJmbgs),
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

            _meetingRepository.SaveMeeting(meeting);
        }

        private int GenerateNewId()
        {
            try
            {
                List<Meeting> meetings = _meetingRepository.FindAll();
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
