using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZdravoKorporacija.Model
{
    internal class Notification
    {
        public int Id;

        public String? Title;

        public String? Description;

        public DateTime StartTime;

        public String? userJmbg;

        public bool Seen;

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
            else if (Id == null ||  !onlyNumberRegex.IsMatch(userJmbg))
                return false;
            else
                return true;

        }

        public  void ToStringNotification()
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
