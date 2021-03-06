using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using ZdravoKorporacija.View.ManagerUI.Help;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for ChooseRenovationType.xaml
    /// </summary>
    public partial class ChooseRenovationType : Page, INotifyPropertyChanged
    {

        public DateTime dateFrom { get; set; }
        public DateTime dateUntil { get; set; }
        public String duration { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public DateTime DateFrom
        {
            get { return dateFrom; }
            set
            {
                dateFrom = value;
                OnPropertyChanged("DateFrom");
            }
        }

        public DateTime DateUntil
        {
            get { return dateUntil; }
            set
            {
                dateUntil = value;
                OnPropertyChanged("DateUntil");
            }
        }

        public String Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged("Duration");
            }
        }

        public ChooseRenovationType()
        {
            InitializeComponent();
            firstDatePicker.Focus();
            this.DataContext = this;
        }


        private void RenovationHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RenovationHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RenovationsHelp renovationsHelp = new RenovationsHelp();
            renovationsHelp.Show();
        }

        private void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerHomePage());
        }

    
        private void StartRenovationClick(object sender, RoutedEventArgs e)
        {

            if (validation() == false)
            {
                MessageBox.Show("Trajanje mora biti u formi broja!");
                NavigationService.Refresh();
            }
            else
            {

                if (RenovationTypeComboBox.SelectedIndex == 0)
                {
                    NavigationService.Navigate(new BasicRenovation(DateFrom, DateUntil, Duration));
                }
                else if (RenovationTypeComboBox.SelectedIndex == 1)
                {
                    NavigationService.Navigate(new RoomJoining(DateFrom, DateUntil, Duration));
                }
                else if (RenovationTypeComboBox.SelectedIndex == 2)
                {
                    NavigationService.Navigate(new RoomSeparation(DateFrom, DateUntil, Duration));
                }
            }
            

        }

        private Boolean validation()
        {
            Regex onlyNumberRegex = new Regex("^[0-9]+$");
            if (!onlyNumberRegex.IsMatch(Duration))
            {
                return false;

            }
            else
            {
                return true;
            }
        }
    }
}
