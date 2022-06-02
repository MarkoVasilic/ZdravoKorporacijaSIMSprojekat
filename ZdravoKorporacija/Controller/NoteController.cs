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
        private readonly NoteService NoteService;

        public NoteController(NoteService noteService)
        {
            this.NoteService = noteService;
        }

        

        public NoteController() { }


        public List<Note> GetAll()
        {
            return NoteService.GetAll();
        }

        public Note GetOneById(int noteId)
        {
            return NoteService.GetOneById(noteId);
        }


        public List<Note> FindAllByPatientJmbg(String patientJmbg)
        {
            return NoteService.FindAllByPatientJmbg(patientJmbg);
        }

        public void Delete(int noteID)
        {
            if (NoteService.GetOneById(noteID) == null)
            {
                throw new Exception("Note with that id doesn't exist!");
            }
            else
            {
                NoteService.Delete(noteID);
            }
        }
        
        public void Create(String Title, String Content)
        {
            NoteService.Create(Title, Content);
        }



    }

}
