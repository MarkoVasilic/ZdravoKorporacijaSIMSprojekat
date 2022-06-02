using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class NotesPageVM
    {
        public ObservableCollection<Note> notes { get; set; }

        public NoteService NoteService { private set; get; }

        public NotesPageVM()
        {
            SetProperties();
            SetCommands();
        }

        #region Commands
        public RelayCommand BackCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public RelayCommand CreateNewNoteCommand { get; set; }
        #endregion

        #region CommandActions
        private void BackExecute(object parameter)
        {
            PatientWindowVM.NavigationService.Navigate(new PatientHomePage());
        }

        private void CreateExecute(object parameter)
        {
            PatientWindowVM.NavigationService.Navigate(new CreateNotePage((int)parameter));
        }



        private void DeleteExecute(object parameter)
        {
            NoteService.Delete((int)parameter);
            notes.Remove(notes.Where(note => note.Id == (int)parameter).Single());
            MessageBox.Show("Bilješka uspješno obrisana! \n ID: " + parameter, "USPJEŠNO!", MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void CreateNewNoteExecute(object parameter)
        {
            PatientWindowVM.NavigationService.Navigate(new CreateNewNotePage());
        }


        #endregion

        #region Methods
        public void SetProperties()
        {
            NoteService = new NoteService(new NoteRepository());
            notes = new ObservableCollection<Note>(NoteService.FindAllByPatientJmbg(App.loggedUser.Jmbg));

            for (int i = 0; i < notes.Count; i++)
            {

                if (notes[i].Content.Length >= 150)
                {

                    notes[i].Content = notes[i].Content.Substring(0, 150) + "...";
                    
                }
            }

        }

        public void SetCommands()
        {
            BackCommand = new RelayCommand(BackExecute);
            CreateCommand = new RelayCommand(CreateExecute);
            DeleteCommand = new RelayCommand(DeleteExecute);
            CreateNewNoteCommand = new RelayCommand(CreateNewNoteExecute);
        }
        #endregion
    }
}
