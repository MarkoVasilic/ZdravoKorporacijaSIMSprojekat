using System;

namespace ZdravoKorporacija.View.SecretaryUI.DTO
{
    public class AbsceneRequestDetailsDto
    {
        public int Id { get; set; }
        public String DoctorJmbg { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String DoctorSpecialtyType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public String IsUrgent { get; set; }
        public String Reason { get; set; }
        public String ReturnMessage { get; set; }

        public AbsceneRequestDetailsDto(int id, string doctorJmbg, string firstName, string lastName,
            string doctorSpecialtyType, DateTime dateFrom,
            DateTime dateTo, String isUrgent, String reason, String returnMessage)
        {
            this.Id = id;
            this.DoctorJmbg = doctorJmbg;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DoctorSpecialtyType = doctorSpecialtyType;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;
            this.IsUrgent = isUrgent;
            this.Reason = reason;
            this.ReturnMessage = returnMessage;
        }
    }
}
