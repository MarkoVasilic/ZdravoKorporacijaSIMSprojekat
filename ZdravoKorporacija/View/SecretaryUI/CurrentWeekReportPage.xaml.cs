using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Controller;
using Model;
using Repository;
using Service;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Service;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for CurrentWeekReportPage.xaml
    /// </summary>
    public partial class CurrentWeekReportPage : Page, INotifyPropertyChanged
    {
        public AppointmentController appointmentController;
        public DoctorController doctorController;
        public PatientController patientController;
        public RoomController roomController;
        public String dateFrom;
        public String dateTo;
        public DateTime dateFromDate;
        public DateTime dateToDate;
        public DateTime selectedDate;
        public ObservableCollection<PossibleAppointmentsDTO> appointments;
        public String DateFrom
        {
            get { return dateFrom; }
            set
            {
                dateFrom = value;
                OnPropertyChanged("DateFrom");
            }
        }
        public String DateTo
        {
            get { return dateTo; }
            set
            {
                dateTo = value;
                OnPropertyChanged("DateTo");
            }
        }
        public DateTime DateFromDate
        {
            get { return dateFromDate; }
            set
            {
                dateFromDate = value;
                OnPropertyChanged("DateFromDate");
            }
        }
        public DateTime DateToDate
        {
            get { return dateToDate; }
            set
            {
                dateToDate = value;
                OnPropertyChanged("DateToDate");
            }
        }
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                getWeekDates();
                setAppointmentsByWeek();
                OnPropertyChanged("SelectedDate");
            }
        }
        public ObservableCollection<PossibleAppointmentsDTO> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged("Appointments");
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
        public CurrentWeekReportPage()
        {
            SecretaryWindowVM.setWindowTitle("Weekly report");
            InitializeComponent();
            this.DataContext = this;
            PatientRepository patientRepository = new PatientRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            RoomRepository roomRepository = new RoomRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository);
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
            ManagerRepository managerRepository = new ManagerRepository();
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            MeetingRepository meetingRepository = new MeetingRepository();
            AdvancedRenovationJoiningRepository advancedRenovationJoining = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationSeparationRepository advancedRenovationSeparation =
                new AdvancedRenovationSeparationRepository();
            ScheduleService scheduleService = new ScheduleService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, managerRepository, secretaryRepository, meetingRepository);
            EmergencyService emergencyService = new EmergencyService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, scheduleService);
            PatientService patientService = new PatientService(patientRepository);
            DoctorService doctorService = new DoctorService(doctorRepository);
            patientController = new PatientController(patientService, appointmentService);
            doctorController = new DoctorController(doctorService);
            roomController = new RoomController(roomService);
            appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
            Appointments = new ObservableCollection<PossibleAppointmentsDTO>();
            SelectedDate = DateTime.Now;
            getWeekDates();
            setAppointmentsByWeek();
        }

        private void getWeekDates()
        {
            DateTime todayDate = SelectedDate.Date;
            if (DayOfWeek.Monday == todayDate.DayOfWeek)
            {
                DateFromDate = todayDate;
                DateToDate = todayDate.AddDays(7);
            }
            else if (DayOfWeek.Tuesday == todayDate.DayOfWeek)
            {
                DateFromDate = todayDate.AddDays(-1);
                DateToDate = todayDate.AddDays(6);
            }
            else if (DayOfWeek.Wednesday == todayDate.DayOfWeek)
            {
                DateFromDate = todayDate.AddDays(-2);
                DateToDate = todayDate.AddDays(5);
            }
            else if (DayOfWeek.Thursday == todayDate.DayOfWeek)
            {
                DateFromDate = todayDate.AddDays(-3);
                DateToDate = todayDate.AddDays(4);
            }
            else if (DayOfWeek.Friday == todayDate.DayOfWeek)
            {
                DateFromDate = todayDate.AddDays(-4);
                DateToDate = todayDate.AddDays(3);
            }
            else if (DayOfWeek.Saturday == todayDate.DayOfWeek)
            {
                DateFromDate = todayDate.AddDays(-5);
                DateToDate = todayDate.AddDays(2);
            }
            else
            {
                DateFromDate = todayDate.AddDays(-6);
                DateToDate = todayDate.AddDays(1);
            }

            datesToString();
        }

        private void datesToString()
        {
            String[] temp = DateFromDate.Date.ToString().Split(" ");
            DateFrom = temp[0];
            temp = DateToDate.Date.ToString().Split(" ");
            DateTo = temp[0];
        }

        private void setAppointmentsByWeek()
        {
            Appointments.Clear();
            List<Appointment> allAppointments = appointmentController.GetAllAppointments();
            foreach (var app in allAppointments)
            {
                if (app.StartTime >= DateFromDate && app.StartTime <= DateToDate)
                {
                    Patient patient = patientController.GetOnePatient(app.PatientJmbg);
                    Doctor doctor = doctorController.GetOneDoctor(app.DoctorJmbg);
                    Room room = roomController.GetRoomById(app.RoomId);
                    Appointments.Add(new PossibleAppointmentsDTO(app.PatientJmbg,
                        patient.FirstName + " " + patient.LastName, app.DoctorJmbg,
                        doctor.FirstName + " " + doctor.LastName, doctor.SpecialtyType, app.RoomId, room.Name,
                        app.StartTime, app.Duration, -1));
                }
                    
            }
        }

        private void Download_PDF_Button_OnClick(object sender, RoutedEventArgs e)
        {
            Document doc = new Document(PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("../../../Resources/PDFs/WeeklyReport_" + DateFrom + ".pdf", FileMode.Create));
            Paragraph lineSeparator = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            doc.Open();

            Paragraph title = new Paragraph("WEEKLY APPOINTMENTS REPORT\n\n");
            title.Font.Size = 18;
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);

            Paragraph header = new Paragraph("Week report for date interval: " + DateFrom + " - " + DateTo);
            header.Font.Size = 16;
            doc.Add(header);
            doc.Add(lineSeparator);
            doc.Add(lineSeparator);

            string text = "";
            Paragraph content = new Paragraph("");
            content.Font.Size = 14;
            int count = 0;
            foreach (var p in Appointments)
            {
                content.Add(++count + ". Appointment \n\n" + p.ToStringPDF() + "\n\n");
            }
            doc.Add(content);
            doc.Close();
        }
    }
}
