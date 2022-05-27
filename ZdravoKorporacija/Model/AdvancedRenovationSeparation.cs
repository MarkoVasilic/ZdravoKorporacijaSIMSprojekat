using Model;
using System;
using System.Text.RegularExpressions;

namespace ZdravoKorporacija.Model
{
    public class AdvancedRenovationSeparation
    {
        public int Id { get; set; }
        public int StartRoomId { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public String ResultFirstRoomName { get; set; }
        public String ResultSecondRoomName { get; set; }
        public String ResultFirstRoomDescription { get; set; }
        public String ResultSecondRoomDescription { get; set; }
        public RoomType FirstRoomType { get; set; }
        public RoomType SecondRoomType { get; set; }


        public AdvancedRenovationSeparation() { }

        public AdvancedRenovationSeparation(int id, int startRoomId, DateTime startTime, int duration, string resultFirstRoomName, string resultSecondRoomName, string resultFirstRoomDescription, string resultSecondRoomDescription, RoomType firstRoomType, RoomType secondRoomType)
        {
            Id = id;
            StartRoomId = startRoomId;
            StartTime = startTime;
            Duration = duration;
            ResultFirstRoomName = resultFirstRoomName;
            ResultSecondRoomName = resultSecondRoomName;
            ResultFirstRoomDescription = resultFirstRoomDescription;
            ResultSecondRoomDescription = resultSecondRoomDescription;
            FirstRoomType = firstRoomType;
            SecondRoomType = secondRoomType;
        }

        public Boolean Validate()
        {
            Regex onlyNumberRegex = new Regex("^[0-9]+$");

            if (Duration == null || !onlyNumberRegex.IsMatch(Duration.ToString()))
            {
                return false;
            }
            else if (StartRoomId == null)
            {
                return false;
            }
            else if (StartTime == null || StartTime < DateTime.Now)
            {
                return false;
            }
            else if (ResultFirstRoomName == null || ResultFirstRoomName.Length < 2)
            {
                return false;
            }
            else if (ResultSecondRoomName == null || ResultSecondRoomName.Length < 2)
            {
                return false;
            }
            else if (ResultFirstRoomDescription == null || ResultFirstRoomDescription.Length < 2)
            {
                return false;
            }
            else if (ResultSecondRoomDescription == null || ResultSecondRoomDescription.Length < 2)
            {
                return false;
            }
            else if (FirstRoomType == null)
            {
                return false;
            }
            else if (SecondRoomType == null)
            {
                return false;
            }
            else return true;
        }



    }
}
