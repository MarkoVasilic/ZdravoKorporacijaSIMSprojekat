using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.Model
{
    public class Note
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }
        public DateTime DateNote { get; set; }
        public String PatientId { get; set; }

        public Note(int id, string title, string content, DateTime dateNote, string patientId)
        {
            Id = id;
            Title = title;
            Content = content;
            DateNote = dateNote;
            this.PatientId = patientId;
        }

        public Note() {}

        public void ispisiNotes()
        {
            Console.WriteLine("ID: "+Id);
            Console.WriteLine("Title: " + Title);
            Console.WriteLine("Content: " + Content);
            Console.WriteLine("DateTime: " + DateNote);
        }
    }
}
