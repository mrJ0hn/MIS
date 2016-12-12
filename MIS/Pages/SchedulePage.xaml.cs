using MIS.Controllers;
using MIS.Model;
using QuickWPFMonthCalendar;
using System;
using System.Collections.Generic;
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

namespace MIS.Pages
{
    /// <summary>
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        private ControlEmployees controlEmployee;
        private List<Appointment> data = new List<Appointment>();
        public SchedulePage()
        {
            InitializeComponent();
            controlEmployee = new ControlEmployees();
            listEmployees.ItemsSource = controlEmployee.GetAll();
            LoadAppointment();
            UpdateData();
            //calendarTuesday.Appointments = appointments2;
        }
        private void UpdateData()
        {
            aptCalendar.MonthAppointments = data;
        }
        private void LoadAppointment()
        {
            Random rand = new Random();
            for (int i = step; i < 5+step; i++)
            {
                Appointment apt = new Appointment()
                {
                    AppointmentID = i*step,
                    StartTime = new DateTime(aptCalendar.DisplayStartDate.Year,
                                         aptCalendar.DisplayStartDate.Month,
                                         rand.Next(1, DateTime.DaysInMonth(aptCalendar.DisplayStartDate.Year, aptCalendar.DisplayStartDate.Month))),
                    Subject = "10:50 Суханов Евгений"
                };
                data.Add(apt);
            }
        }
        private List<Visitor> GetVisitors()
        {
            List<Visitor> visitors = new List<Visitor>();
            
            return visitors;
        }
        private void btnNewVisitor_Click(object sender, RoutedEventArgs e)
        {
            new AddClientWindow().Show();
        }
        int step = 1;
        private void aptCalendar_DisplayMonthChanged(MonthChangedEventArgs e)
        {
            step+=5;
            LoadAppointment();
            UpdateData();
        }
    }
}
