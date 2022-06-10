using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class NoteService
    {

        private readonly NoteRepository _noteRepository;

        public NoteService(NoteRepository noteRepository)
        {
            this._noteRepository = noteRepository;
        }

        public NoteService() { }


        public List<Note> GetAll()
        {
            return _noteRepository.FindAll();
        }
                
        public Note GetOneById(int noteId)
        {
            return _noteRepository.FindOneById(noteId);
        }

        public int GenerateNewId()
        {
            try
            {
                List<Note> notes = _noteRepository.FindAll();
                int currentMax = notes.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

       public List<Note> FindAllByPatientJmbg(String patientJmbg)
        {
            return _noteRepository.FindAllByPatientJmbg(patientJmbg);
        }

        public void Delete(int noteID)
        {
            if (_noteRepository.FindOneById(noteID) == null)
            {
                throw new Exception("Note with that id doesn't exist!");
            }
            else
            {
                _noteRepository.Remove(noteID);
            }
        }

        public void Create(String Title, String Content)
        {
            int id = GenerateNewId();
            Note newNote = new Note(id, Title, Content, System.DateTime.Now, App.loggedUser.Jmbg);
            _noteRepository.SaveNote(newNote);
        }



    }

    }

