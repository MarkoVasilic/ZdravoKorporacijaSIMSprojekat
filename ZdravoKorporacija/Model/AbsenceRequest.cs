using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AbsenceRequest
    {
        public int Id { get; set; }
        public String DoctorJmbg { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Interval { get; set; }
        public String Reason { get; set; }
        public Boolean Emergency { get; set; }

        public AbsenceRequest(int id, string doctorJmbg, DateTime from, DateTime to, int interval, string reason, bool emergency)
        {
            Id = id;
            DoctorJmbg = doctorJmbg;
            From = from;
            To = to;
            Interval = interval;
            Reason = reason;
            Emergency = emergency;
        }
    }
}
