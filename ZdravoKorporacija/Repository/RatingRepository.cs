using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Repository
{
    public class RatingRepository
    {
        private readonly String RatingFilePath = @"..\..\..\Resources\rating.json";

        public List<Rating> FindAll()
        {
            var values = GetValues();
            return values;
        }

        //Metoda za upravnika, da dobavi sve ocijene za konkretnog doktora
        public List<Rating> FindAllRatingsByDoctorJmbg(String doctorJmbg)
        {
            var values = GetValues();
            List<Rating> result = new List<Rating>();
            foreach (Rating rating in values)
                if (rating.PatientId.Equals(doctorJmbg))
                    result.Add(rating);
            return result;
        }

        public bool FindOneByAppointemntId(int appointmentId)
        {
            var values = GetValues();
            foreach (Rating rating in values)
                if (rating.AppointmentId.Equals(appointmentId))
                    return true;
            return false;
        }

        public void SaveRating(Rating ratingToSave)
        {
            var values = GetValues();
            values.Add(ratingToSave);
            Save(values);
        }

        public void Remove(int ratingId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.Id == ratingId);
            Save(values);
        }

        public Rating? FindOneById(int ratingId)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.Id == ratingId)
                {
                    return val;
                }
            }

            return null;
        }

        public List<Rating> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Rating>>(File.ReadAllText(RatingFilePath));
            if (values == null)
            {
                values = new List<Rating>();
            }

            return values;
        }


        public void Update(Rating ratingToModify)
        {
            var oneRating = FindOneById(ratingToModify.Id);
            if (oneRating != null)
            {
                var values = GetValues();
                values.RemoveAll(value => value.Id.Equals(ratingToModify.Id));
                values.Add(ratingToModify);
                Save(values);
            }
        }

        public void Save(List<Rating> values)
        {
            File.WriteAllText(RatingFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }


    }
}
