using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Repository
{
    public class AbsenceRequestRepository
    {
        private readonly String AbsenceRequestFilePath = @"..\..\..\Resources\absenceRequests.json";

        public List<AbsenceRequest> FindAll()
        {
            var values = GetValues();

            return values;
        }

        public AbsenceRequest? FindOneById(int id)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.Id == id)
                {
                    return val;
                }
            }

            return null;
        }

        public List<AbsenceRequest>? FindAllByDoctorJmbg(String doctorJmbg)
        {
            var values = GetValues();
            List<AbsenceRequest> result = new List<AbsenceRequest>();
            foreach (AbsenceRequest absenceRequest in values)
                if (absenceRequest.DoctorJmbg.Equals(doctorJmbg))
                    result.Add(absenceRequest);
            return result;
        }

        public List<AbsenceRequest>? FindAllByDoctorSpecialtyType(String doctorSpecialtyType)
        {
            var values = GetValues();
            List<AbsenceRequest> result = new List<AbsenceRequest>();
            foreach (AbsenceRequest absenceRequest in values)
                if (absenceRequest.DoctorScecialtyType.Equals(doctorSpecialtyType))
                    result.Add(absenceRequest);
            return result;
        }

        public List<AbsenceRequest> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<AbsenceRequest>>(File.ReadAllText(AbsenceRequestFilePath));
            if (values == null)
            {
                values = new List<AbsenceRequest>();
            }

            return values;
        }

        public void SaveAbsenceRequest(AbsenceRequest absenceRequestToSave)
        {
            var values = GetValues();
            values.Add(absenceRequestToSave);
            Save(values);
        }

        public void Save(List<AbsenceRequest> values)
        {
            File.WriteAllText(AbsenceRequestFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }
    }
}
