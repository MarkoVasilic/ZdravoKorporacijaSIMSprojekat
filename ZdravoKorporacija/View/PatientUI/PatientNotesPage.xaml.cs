using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.PatientUI
{
    /// <summary>
    /// Interaction logic for PatientNotesPage.xaml
    /// </summary>
    public partial class PatientNotesPage : Page
    {
        public static NoteController noteController { get; set; }
        public ObservableCollection<Note> notes { get; set; }

        public PatientNotesPage()
        {
            NoteRepository noteRepository = new NoteRepository();
            NoteService noteService = new NoteService(noteRepository);
            NoteController noteController1 = new NoteController(noteService);
            noteController = noteController1;
            InitializeComponent();
            DataContext = this;

            notes = new ObservableCollection<Note>(noteController.FindAllByPatientJmbg(App.loggedUser.Jmbg));

            for (int i = 0; i < notes.Count; i++)
            {
                Console.WriteLine(notes[i].Content);
                Console.WriteLine("--------");
                Console.WriteLine(notes[i].Content.Length);
                Console.WriteLine("--------");
                if (notes[i].Content.Length >= 150)
                {
                    Console.WriteLine("USAO SAM " + i);
                    notes[i].Content = notes[i].Content.Substring(0, 150) + "...";
                    Console.WriteLine(notes[i].Content.Substring(0, 150));
                }
            }
        }


        private void GoBackButton(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CreateNoteButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateNotePage());
        }

        private void OtvoriButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateNotePage());
        }

        private void IzmjenIButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateNotePage());
        }
    }
}
