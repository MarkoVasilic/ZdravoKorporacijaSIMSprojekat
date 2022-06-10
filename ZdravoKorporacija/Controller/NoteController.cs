using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class NoteController
    {
        private readonly NoteService _noteService;

        public NoteController(NoteService noteService)
        {
            this._noteService = noteService;
        }

        

        public NoteController() { }


        public List<Note> GetAll()
        {
            return _noteService.GetAll();
        }

        public Note GetOneById(int noteId)
        {
            return _noteService.GetOneById(noteId);
        }


        public List<Note> FindAllByPatientJmbg(String patientJmbg)
        {
            return _noteService.FindAllByPatientJmbg(patientJmbg);
        }

        public void Delete(int noteID)
        {
            if (_noteService.GetOneById(noteID) == null)
            {
                throw new Exception("Note with that id doesn't exist!");
            }
            else
            {
                _noteService.Delete(noteID);
            }
        }
        
        public void Create(String Title, String Content)
        {
            _noteService.Create(Title, Content);
        }



    }

}
