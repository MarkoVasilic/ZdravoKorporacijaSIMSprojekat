using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Controller;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for AddNewPatientPage.xaml
    /// </summary>
    public partial class AddNewPatientPage : Page, INotifyPropertyChanged
    {
        private AddAccountVM addAccountVM;
        private PatientController patientController;
        private ObservableCollection<String> demoAllergens;
        public ObservableCollection<String> DemoAllergens
        {
            get => demoAllergens;
            set
            {
                demoAllergens = value;
                OnPropertyChanged("DemoAllergens");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public AddNewPatientPage()
        {
            InitializeComponent();
            addAccountVM = new AddAccountVM();
            this.DataContext = addAccountVM;
            DemoAllergens = new ObservableCollection<string>();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Male.IsChecked == true)
                addAccountVM.setPatientGenderMale((BloodType)BloodTypeComboBox.SelectedIndex);
            else if (Female.IsChecked == true)
                addAccountVM.setPatientGenderFemale((BloodType)BloodTypeComboBox.SelectedIndex);
            else
                addAccountVM.setBloodType((BloodType)BloodTypeComboBox.SelectedIndex);
            if (System.Windows.Controls.Validation.GetHasError(FirstNameTextBox) || System.Windows.Controls.Validation.GetHasError(LastNameTextBox)
                || System.Windows.Controls.Validation.GetHasError(JmbgTextBox) || System.Windows.Controls.Validation.GetHasError(PhoneNumberTextBox)
                || System.Windows.Controls.Validation.GetHasError(EmailTextBox) || System.Windows.Controls.Validation.GetHasError(AddressTextBox)
                || System.Windows.Controls.Validation.GetHasError(UsernameTextBox) || System.Windows.Controls.Validation.GetHasError(PasswordTextBox))
            {
                addAccountVM.setErrorMessage("Not all input fields are valid!");
            }
            else
            {
                addAccountVM.setErrorMessage("");
            }
        }

        private void Start_Demo_Button_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDemo();
        }

         #region DEMO
         public Thread DemoThread { get; set; }
         public void ExecuteDemo()
         {
             DemoThread = new Thread(CallDemoMethods);
             DemoThread.Start();
         }

         public void CallDemoMethods()
         {
             while (true)
             {
                 toggleStopVisibility();
                 disableComponents();
                 textBoxDemo(FirstNameTextBox, "Maja");
                 textBoxDemo(LastNameTextBox, "Bogdanovic");
                 textBoxDemo(JmbgTextBox, "0310983123453");
                 datepickerDemo();
                 radioButtonDemo();
                 scroll(200, 450);
                 comboboxDemo(BloodTypeComboBox);
                 textBoxDemo(PhoneNumberTextBox, "0634586354");
                 textBoxDemo(EmailTextBox, "majabog983@gmail.com");
                 textBoxDemo(AddressTextBox, "Gunduliceva 17, Novi Sad");
                 scroll(450, 700);
                 textBoxDemo(UsernameTextBox, "majabog");
                 textBoxDemo(PasswordTextBox, "password123");
                 scroll(700, 950);
                 textBoxDemo(AddAllergenTextBox, "gluten");
                 buttonDemoAddAlergen();
                 scroll(950, 1150);
                 buttonDemo();
                 scrollToTop();
                 clearComponents();
                 executeCountdown();
             }
         }

         private void toggleStopVisibility()
         {
             try
             {
                 this.Dispatcher.Invoke((Action)(() =>
                 {
                     StopDemoButton.Visibility = Visibility.Visible;
                     StartDemoButton.Visibility = Visibility.Hidden;
                     SecondsLeftTextBlock.Visibility = Visibility.Visible;
                 }));
             }
             catch (Exception ex) { }

         }

         private void executeCountdown()
         {
             try
             {
                 for (int i = 5; i >= 0; --i)
                 {
                     this.Dispatcher.Invoke((Action)(() =>
                     {
                         SecondsLeftTextBlock.Text = i.ToString();
                     }));
                     Thread.Sleep(1000);
                 }
             }
             catch (Exception ex) { }


         }

         private void disableComponents()
         {
             try
             {
                 this.Dispatcher.Invoke((Action)(() =>
                 {
                     FirstNameTextBox.IsReadOnly = true;
                     LastNameTextBox.IsReadOnly = true;
                     JmbgTextBox.IsReadOnly = true;
                     DateOfBirthPicker.IsEnabled = false;
                     Male.IsEnabled = false;
                     Female.IsEnabled = false;
                     BloodTypeComboBox.IsEnabled = false;
                     PhoneNumberTextBox.IsReadOnly = true;
                     EmailTextBox.IsReadOnly = true;
                     AddressTextBox.IsReadOnly = true;
                     UsernameTextBox.IsReadOnly = true;
                     PasswordTextBox.IsReadOnly = true;
                     AllergensListBox.ItemsSource = DemoAllergens;
                     AddAllergenTextBox.IsReadOnly = true;
                     RemoveAllergenButton.IsEnabled = false;
                     AddAllergenButton.IsEnabled = false;
                     SaveButton.IsEnabled = false;
                 }));
             }
             catch (Exception ex) { }

         }

         private void clearComponents()
         {
             try
             {
                 this.Dispatcher.Invoke((Action)(() =>
                 {
                     FirstNameTextBox.Text = "";
                     LastNameTextBox.Text = "";
                     JmbgTextBox.Text = "";
                     DateOfBirthPicker.SelectedDate = null;
                     Male.IsChecked = true;
                     Female.IsChecked = false;
                     BloodTypeComboBox.SelectedIndex = -1;
                     PhoneNumberTextBox.Text = "";
                     EmailTextBox.Text = "";
                     AddressTextBox.Text = "";
                     UsernameTextBox.Text = "";
                     PasswordTextBox.Text = "";
                     AllergensListBox.ItemsSource = "";
                     AddAllergenTextBox.Text = "";
                     DemoAllergens.Clear();
                 }));
             }
             catch (Exception ex) { }

         }

         private void textBoxDemo(TextBox textBox, string value)
         {
             try
             {
                 this.Dispatcher.Invoke((Action)(() =>
                 {
                     textBox.BorderBrush = new SolidColorBrush(Colors.MediumSpringGreen);
                 }));
                for (int i = 1; i <= value.Length; i++)
                 {
                     this.Dispatcher.Invoke((Action)(() =>
                     {
                         textBox.Text = value.Substring(0, i);
                     }));
                     Thread.Sleep(150);
                 }
                this.Dispatcher.Invoke((Action)(() =>
                {
                    textBox.BorderBrush = new SolidColorBrush(Colors.Gray);
                }));
             }
             catch (Exception ex) { }

         }

         private void datepickerDemo()
         {
             try
             {
                 this.Dispatcher.Invoke((Action)(() =>
                 {
                     DateOfBirthPicker.BorderBrush = new SolidColorBrush(Colors.MediumSpringGreen);
                     DateOfBirthPicker.IsEnabled = true;
                     DateOfBirthPicker.IsDropDownOpen = true;
                     DateOfBirthPicker.Text = new DateTime(1983, 10, 3).ToString();
                 }));
                 Thread.Sleep(300);
                 this.Dispatcher.Invoke((Action)(() =>
                 {
                     DateOfBirthPicker.BorderBrush = new SolidColorBrush(Colors.Gray);
                     DateOfBirthPicker.IsDropDownOpen = false;
                     DateOfBirthPicker.IsEnabled = false;
                 }));
             }
             catch (Exception ex)
             {

             }


         }

         private void radioButtonDemo()
         {
             try
             { 
                 Thread.Sleep(450);
                this.Dispatcher.Invoke((Action)(() =>
                 {
                     Male.IsChecked = false;
                     Female.IsChecked = true;
                 }));
                Thread.Sleep(450);
            }
             catch (Exception ex) { }

         }
        private void comboboxDemo(ComboBox comboBox)
         {
             try
             {
                 this.Dispatcher.Invoke((Action)(() =>
                 {
                     comboBox.BorderBrush = new SolidColorBrush(Colors.MediumSpringGreen);
                     comboBox.IsDropDownOpen = true;
                 }));
                 Thread.Sleep(450);
                 this.Dispatcher.Invoke((Action)(() =>
                 {
                     comboBox.SelectedIndex = 4;
                     comboBox.IsDropDownOpen = false;
                     comboBox.BorderBrush = new SolidColorBrush(Colors.Gray);
                 }));
             }
             catch (Exception ex) { }

         }

         private void scroll(int from, int to)
         {
             try
             {
                 for (int i = from; i < to; i++)
                 {
                     this.Dispatcher.Invoke((Action)(() =>
                     {
                         Scroller.ScrollToVerticalOffset(i);
                     }));
                     Thread.Sleep(2);
                 }
             }
             catch (Exception ex) { }

         }

         private void buttonDemoAddAlergen()
         {
             try
             {
                 this.Dispatcher.Invoke((Action)(() =>
                 {
                     AddAllergenButton.IsEnabled = true;
                     AddAllergenButton.Background = Brushes.GreenYellow;
                     DemoAllergens.Add("gluten");
                 }));
                 Thread.Sleep(300);
                 this.Dispatcher.Invoke((Action)(() =>
                 {
                     BrushConverter bc = new BrushConverter();
                     AddAllergenButton.Background = (Brush)bc.ConvertFrom("#4267B2");
                     AddAllergenButton.IsEnabled = false;
                 }));
                 Thread.Sleep(450);
             }
             catch (Exception ex) { }

         }

        private void buttonDemo()
         {
             try
             {
                 //Thread.Sleep(850);
                this.Dispatcher.Invoke((Action)(() =>
                 {
                     SaveButton.IsEnabled = true;
                     SaveButton.Background = Brushes.GreenYellow;
                 }));
                 Thread.Sleep(300);
                 this.Dispatcher.Invoke((Action)(() =>
                 {
                     BrushConverter bc = new BrushConverter();
                     SaveButton.Background = (Brush)bc.ConvertFrom("#4267B2");
                     SaveButton.IsEnabled = false;
                 }));
                 Thread.Sleep(450);
            }
             catch (Exception ex) { }

         }

         private void scrollToTop()
         {
             try
             {
                 for (int i = 700; i >= 0; i--)
                 {
                     this.Dispatcher.Invoke((Action)(() =>
                     {
                         Scroller.ScrollToVerticalOffset(i);
                     }));
                     Thread.Sleep(2);
                 }
             }
             catch (Exception ex) { }

         }

         private void Stop_Demo_Button_Click(object sender, RoutedEventArgs e)
         {
             try
             {
                 DemoThread.Abort();
             }
             catch (Exception ex) { }

             NavigationService.Navigate(new AddNewPatientPage());
         }

         #endregion
    }
}