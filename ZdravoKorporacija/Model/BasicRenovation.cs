using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZdravoKorporacija.Model
{
    public class BasicRenovation
    {

        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public String Description  { get; set; }

        public BasicRenovation(int id, int roomId, DateTime startDate, DateTime endDate, int duration, string description)
        {
            Id = id;
            RoomId = roomId;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
            Description = description;
        }

        public Boolean validateBasicRenovation()
        {
            Regex descriptionRegex = new Regex("^$|[a-zA-Z]+[a-zA-Z0-9_\\.\\s]*$");
            Regex onlyNumberRegex = new Regex("^[0-9]+$");

            if (Description== null || Description.Length < 3 || !descriptionRegex.IsMatch(Description))
            {
                return false;
            }
            else if (Duration == null || !onlyNumberRegex.IsMatch(Duration.ToString()))
            {
                return false;
            }
            else if (RoomId == null)
            {
                return false;
            }else if (StartDate == null || StartDate < DateTime.Now)
            {
                return false;
            }else if(EndDate == null || EndDate < DateTime.Now || EndDate < StartDate)
            {
                return false;
            }
            else return true;
        }

    }
}
