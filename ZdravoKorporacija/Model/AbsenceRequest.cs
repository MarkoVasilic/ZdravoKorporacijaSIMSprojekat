using System;

namespace Model
{
    public class AbsenceRequest
    {
        public int Id { get; set; }
        public String DoctorJmbg { get; set; }
        public String DoctorScecialtyType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int IntervalInDays { get; set; }
        public String Reason { get; set; }
        public Boolean isUgent { get; set; }
        public AbsenceRequestState State { get; set; }

        public AbsenceRequest(int id, string doctorJmbg, string doctorScecialtyType, DateTime dateFrom, DateTime dateTo, int intervalInDays, string reason, bool isUgent, AbsenceRequestState state)
        {
            Id = id;
            DoctorJmbg = doctorJmbg;
            DoctorScecialtyType = doctorScecialtyType;
            DateFrom = dateFrom;
            DateTo = dateTo;
            IntervalInDays = intervalInDays;
            Reason = reason;
            this.isUgent = isUgent;
            State = state;
        }

        public AbsenceRequest()
        {
        }
    }
}
