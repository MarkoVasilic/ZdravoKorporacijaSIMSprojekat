using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.PatientUI.Commands;

namespace ZdravoKorporacija.View.PatientUI.ViewModels
{
    public class CreateNotePageVM : INotifyPropertyChanged
    {
        public Note note { get; set; }

        public NoteService NoteService { private set; get; }

        public CreateNotePageVM(int id)
        {
            SetCommands();
            NoteService = new NoteService(new NoteRepository());
            note = NoteService.GetOneById(id);
            title = note.Title;
            content = note.Content;

        }

        public RelayCommand CreateNoteCommand { get; private set; }
        public RelayCommand BackCommand { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String content;
        public String Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged("Content");
            }
        }

        private String title;

        public String Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }


        private void BackExecute(object parameter)
        {
            PatientWindowVM.NavigationService.Navigate(new PatientNotesPage());
        }

        public void SetCommands()
        {
            BackCommand = new RelayCommand(BackExecute);
        }

    }
}
