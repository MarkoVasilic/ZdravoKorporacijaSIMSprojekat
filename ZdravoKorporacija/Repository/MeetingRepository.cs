using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Repository
{
    public class MeetingRepository
    {
        private readonly String MeetingFilePath = @"..\..\..\Resources\meeting.json";

        public List<Meeting> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public void SaveMeeting(Meeting meetingToSave)
        {
            var values = GetValues();
            values.Add(meetingToSave);
            Save(values);
        }

        public void RemoveMeeting(int meetingId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.Id == meetingId);
            Save(values);
        }


        public Meeting? FindOneById(int meetingId)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.Id == meetingId)
                {
                    return val;
                }
            }

            return null;
        }

        public List<Meeting> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Meeting>>(File.ReadAllText(MeetingFilePath));
            if (values == null)
            {
                values = new List<Meeting>();
            }

            return values;
        }

        public void UpdateMeeting(Meeting meetingToModify)
        {
            var oneMeeting = FindOneById(meetingToModify.Id);
            if (oneMeeting != null)
            {
                var values = GetValues();
                values.RemoveAll(value => value.Id.Equals(meetingToModify.Id));
                values.Add(meetingToModify);
                Save(values);
            }
        }

        public void Save(List<Meeting> values)
        {
            File.WriteAllText(MeetingFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }
    }
}
