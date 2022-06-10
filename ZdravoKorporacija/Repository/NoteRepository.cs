using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Repository
{
    public class NoteRepository 
    {
        private readonly String _noteFilePath = @"..\..\..\Resources\notes.json";


        public List<Note> FindAll()
        {
            var values = GetValues();

            return values;
        }

        public List<Note> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Note>>(File.ReadAllText(_noteFilePath));
            if (values == null)
            {
                values = new List<Note>();
            }

            return values;
        }

        public List<Note> FindAllByPatientJmbg(String patientJmbg)
        {
            var values = GetValues();
            List<Note> result = new List<Note>();
            foreach (Note note in values)
                if (note.PatientId.Equals(patientJmbg))
                    result.Add(note);
            return result;
        }

        public Note? FindOneById(int noteId)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.Id == noteId)
                {
                    return val;
                }
            }

            return null;
        }


        public void Update(Note noteToModify)
        {
            var oneMedication = FindOneById(noteToModify.Id);
            if (oneMedication != null)
            {
                var values = GetValues();
                values.RemoveAll(value => value.Id.Equals(noteToModify.Id));
                values.Add(noteToModify);
                Save(values);
            }
        }


        public void SaveNote(Note noteToSave)
        {
            var values = GetValues();
            values.Add(noteToSave);
            Save(values);
        }

        public void Save(List<Note> values)
        {
            File.WriteAllText(_noteFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        public void Remove(int noteId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.Id == noteId);
            Save(values);
        }

    }
}
