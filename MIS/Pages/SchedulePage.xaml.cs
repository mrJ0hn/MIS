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
        private List<Appointment> data = new List<Appointment>();
        public SchedulePage()
        {
            InitializeComponent();
            listEmployees.ItemsSource = GetVisitors();
            LoadAppointment();
            UpdateData();
            //calendarTuesday.Appointments = appointments2;
        }

        private void UpdateData()
        {
            AptCalendar.MonthAppointments = data;
        }

        private void LoadAppointment()
        {
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                Appointment apt = new Appointment()
                {
                    AppointmentID = i,
                    StartTime = new DateTime(DateTime.Now.Year,
                                         12,
                                         rand.Next(1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))),
                    Subject = "13-40 Суханов Евгений"
                };
                data.Add(apt);
            }
        }

        private List<Visitor> GetVisitors()
        {
            List<Visitor> visitors = new List<Visitor>();
            Visitor visitor = new Visitor()
            {
                FullName = "Александров Александр",
            };
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            return visitors;
        }

        private void btnNewVisitor_Click(object sender, RoutedEventArgs e)
        {
            new AddClientWindow().Show();
        }
    }
}
