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

        private readonly NoteRepository NoteRepository;

        public NoteService(NoteRepository noteRepository)
        {
            this.NoteRepository = noteRepository;
        }

        public NoteService() { }


        public List<Note> GetAll()
        {
            return NoteRepository.FindAll();
        }
                
        public Note GetOneById(int noteId)
        {
            return NoteRepository.FindOneById(noteId);
        }

        public int GenerateNewId()
        {
            try
            {
                List<Note> notes = NoteRepository.FindAll();
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
            return NoteRepository.FindAllByPatientJmbg(patientJmbg);
        }

        public void Delete(int noteID)
        {
            if (NoteRepository.FindOneById(noteID) == null)
            {
                throw new Exception("Note with that id doesn't exist!");
            }
            else
            {
                NoteRepository.Remove(noteID);
            }
        }

        public void Create(String Title, String Content)
        {
            int id = GenerateNewId();
            Note newNote = new Note(id, Title, Content, System.DateTime.Now, App.loggedUser.Jmbg);
            NoteRepository.SaveNote(newNote);
        }



    }

    }

