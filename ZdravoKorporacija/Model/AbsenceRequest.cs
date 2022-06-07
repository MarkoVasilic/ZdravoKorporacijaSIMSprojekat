using System;

namespace Model
{
    public class AbsenceRequest
    {
        public int Id { get; set; }
        public String DoctorJmbg { get; set; }
        public String DoctorSpecialtyType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int IntervalInDays { get; set; }
        public String Reason { get; set; }
        public Boolean isUrgent { get; set; }
        public AbsenceRequestState State { get; set; }
        public String? Response { get; set; }

        public AbsenceRequest(int id, string doctorJmbg, string doctorSpecialtyType, DateTime dateFrom, DateTime dateTo, int intervalInDays, string reason, bool isUrgent, AbsenceRequestState state, String? response)
        {
            Id = id;
            DoctorJmbg = doctorJmbg;
            DoctorSpecialtyType = doctorSpecialtyType;
            DateFrom = dateFrom;
            DateTo = dateTo;
            IntervalInDays = intervalInDays;
            Reason = reason;
            this.isUrgent = isUrgent;
            State = state;
            Response = response;
        }


        public AbsenceRequest()
        {
        }

        public void ToString()
        {
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("DoctorJmbg: " + DoctorJmbg);
            Console.WriteLine("Specialty: " + DoctorSpecialtyType);
            Console.WriteLine("From: " + DateFrom);
            Console.WriteLine("To: " + DateTo);
            Console.WriteLine("To: " + IntervalInDays);
            Console.WriteLine("To: " + Reason);
            Console.WriteLine("To: " + isUrgent);
            Console.WriteLine("To: " + State);
            Console.WriteLine("Rejection reason" + Response);
        }
    }
}
