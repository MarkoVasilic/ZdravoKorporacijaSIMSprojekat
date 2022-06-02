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
    public class CreateNewNoteVM : INotifyPropertyChanged
    {
        public Note NoteToBeCreated { get; set; }

        public NoteService NoteService { private set; get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public CreateNewNoteVM()
        {
            SetCommands();
            NoteService = new NoteService(new NoteRepository());
            NoteToBeCreated = new Note();
        }

        public RelayCommand CreateNoteCommand { get; private set; }
        public RelayCommand BackCommand { get; set; }

        public void CreateNoteExecute(object parameter)
        {
            NoteService.Create(NoteToBeCreated.Title, NoteToBeCreated.Content);
            MessageBox.Show("Uspješno kreirana biljeska! \n Naslov:  " + NoteToBeCreated.Title, "USPJEŠNO!", MessageBoxButton.OK,MessageBoxImage.None);
            PatientWindowVM.NavigationService.Navigate(new PatientNotesPage());
        }
        public bool CreateNoteCanExecute(object parameter)
        {
            if(NoteToBeCreated.Content == null || NoteToBeCreated.Content.Length < 3 || NoteToBeCreated.Title == null)
            {
              return false;
            }

            return true;

        }



        private void BackExecute(object parameter)
        {
            PatientWindowVM.NavigationService.Navigate(new PatientNotesPage());
        }

        private void SetCommands()
        {
            BackCommand = new RelayCommand(BackExecute);
            CreateNoteCommand = new RelayCommand(CreateNoteExecute, CreateNoteCanExecute);
        }
    }

}
