using System;
using System.Text.RegularExpressions;

namespace ZdravoKorporacija.Model
{
    public class Notification
    {
        public int Id { get; set; }

        public String? Title { get; set; }

        public String? Description { get; set; }

        public DateTime StartTime { get; set; }

        public String? userJmbg { get; set; }

        public bool Seen { get; set; }

        public Notification(string title, string description, DateTime startTime, string receiverJmbg, bool seen, int newId)
        {
            Id = newId;
            Title = title;
            Description = description;
            this.StartTime = startTime;
            this.userJmbg = receiverJmbg;
            this.Seen = seen;
        }
        public Notification() { }

        public Boolean validateNotification()
        {
            Regex onlyNumberRegex = new Regex("^[0-9]+$");
            if (StartTime == null || StartTime < DateTime.Now)
                return false;
            else if (userJmbg == null || userJmbg.Length != 13 || !onlyNumberRegex.IsMatch(userJmbg))
                return false;
            else if (Description == null)
                return false;
            else if (Title == null)
                return false;
            else if (Id == null || !onlyNumberRegex.IsMatch(userJmbg))
                return false;
            else
                return true;

        }

        public void ToStringNotification()
        {
            Console.WriteLine("ID = " + Id);
            Console.WriteLine("userJMBG = " + userJmbg);
            Console.WriteLine("Title = " + Title);
            Console.WriteLine("StartTime = " + StartTime);
            Console.WriteLine("Description = " + Description);
            Console.WriteLine("Seen = " + Seen);
            Console.WriteLine();
        }

    }
}
