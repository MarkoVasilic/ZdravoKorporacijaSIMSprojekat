using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
namespace ZdravoKorporacija.Repository
{
    public class AbsenceRequestRepository
    {
        private readonly String _absenceRequestFilePath = @"..\..\..\Resources\absenceRequests.json";

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

        public void UpdateAbsenceRequest(AbsenceRequest absenceRequestToModify)
        {
            var oneAbsceneRequest = FindOneById(absenceRequestToModify.Id);
            if (oneAbsceneRequest != null)
            {
                var values = GetValues();
                values.RemoveAll(value => value.Id.Equals(oneAbsceneRequest.Id));
                values.Add(absenceRequestToModify);
                Save(values);
            }
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
                if (absenceRequest.DoctorSpecialtyType.Equals(doctorSpecialtyType))
                    result.Add(absenceRequest);
            return result;
        }

        public List<AbsenceRequest> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<AbsenceRequest>>(File.ReadAllText(_absenceRequestFilePath));
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
            File.WriteAllText(_absenceRequestFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }
    }
}
