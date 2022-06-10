using Repository;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.ManagerUI.Help;
using ZdravoKorporacija.View.RoomCRUD;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ZdravoKorporacija.View.Equipment
{
    public partial class GetAllEquipment : Page, INotifyPropertyChanged
    {
        private EquipmentController equipmentController;

        public ObservableCollection<EquipmentDTO> equipment { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;


        private String equipmentType = " ";

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

        }

        private string equipmentName = "";

        public string EquipmentName
        {
            get { return equipmentName; }
            set
            {
                equipmentName = value;
                OnPropertyChanged("EquipmentName");
            }
        }
        public ObservableCollection<EquipmentDTO> Equipment
        {
            get { return equipment; }
            set
            {
                equipment = value;
                OnPropertyChanged("Equipment");
            }
        }

        public GetAllEquipment()
        {
            InitializeComponent();
            EquipmentRepository equipmentRepository = new EquipmentRepository();
            RoomRepository roomRepository = new RoomRepository();
            EquipmentService equipmentService = new EquipmentService(equipmentRepository, roomRepository);
            equipmentController = new EquipmentController(equipmentService);
            this.DataContext = this;
            equipment = new ObservableCollection<EquipmentDTO>(equipmentController.GetEquipmentDTOs());


        }


        public void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerHomePage());
        }

        private void SearchButtonCLick(object sender, RoutedEventArgs e)
        {
            Equipment = new ObservableCollection<EquipmentDTO>(equipmentController.Search(EquipmentName));
        }

        private void FilteringButtonClick(object sender, RoutedEventArgs e)
        {
            if (EquipmentTypeComboBox.SelectedIndex == 0)
            {
                equipmentType = "TRAJNA";
            }
            else if (EquipmentTypeComboBox.SelectedIndex == 1)
            {
                equipmentType = "POTROŠNA";
            }


            Equipment = new ObservableCollection<EquipmentDTO>(equipmentController.Filter(equipmentType));
        }


        public void EquipmentHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void EquipmentHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EquipmentHelp equipmentHelp = new EquipmentHelp();
            equipmentHelp.Show();
        }

        private void PDFClick(object sender, RoutedEventArgs e)
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("../../../Resources/PDFs/Equipment.pdf", FileMode.Create));
            doc.Open();
            string header = "DOSTUPNA OPREMA U BOLNICI \n\n";
            string text = "";
            iTextSharp.text.Paragraph p2 = new iTextSharp.text.Paragraph("");
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(header);
            doc.Add(paragraph);
            foreach(var eq in equipment)
            {
                text += eq.toString();
                p2 = new iTextSharp.text.Paragraph(text);
            }

            doc.Add(p2);
            doc.Close();

            MessageBox.Show("Izveštaj o opremi je kreiran. Izveštaj se nalazi u ../../ZdravoKorporacija/Resources/PDFs", "Obaveštenje", MessageBoxButton.OK);
        }
    }
}
