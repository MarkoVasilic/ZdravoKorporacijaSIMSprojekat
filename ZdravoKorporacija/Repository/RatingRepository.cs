using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Repository
{
    public class RatingRepository
    {
        private readonly String _ratingFilePath = @"..\..\..\Resources\rating.json";

        public List<Rating> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public List<Rating> FindAllByDoctorJmbg(String jmbg)
        {
            var values = GetValues();
            List<Rating> result = new List<Rating>();
            foreach (Rating rating in values)
                if (rating.DoctorId.Equals(jmbg))
                    result.Add(rating);
            return result;
        }

        public bool FindOneByAppointemntId(int id)
        {
            var values = GetValues();
            foreach (Rating rating in values)
                if (rating.AppointmentId.Equals(id))
                    return true;
            return false;
        }

        public void SaveRating(Rating ratingToSave)
        {
            var values = GetValues();
            values.Add(ratingToSave);
            Save(values);
        }

        public void Remove(int id)
        {
            var values = GetValues();
            values.RemoveAll(val => val.Id == id);
            Save(values);
        }

        public Rating? FindOneById(int id)
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

        public List<Rating> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Rating>>(File.ReadAllText(_ratingFilePath));
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
            File.WriteAllText(_ratingFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }


    }
}
