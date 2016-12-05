using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace OutlookCalendar.Model
{
    public class Appointments : ObservableCollection<Appointment>
    {
        public Appointments(DateTime curDate)
        {
            Add(new Appointment() { Subject = "Dummy Appointment #1",
                StartTime = new DateTime(
                    curDate.Year, 
                    curDate.Month,
                    curDate.Day, 
                    12, 00, 00),
                EndTime = new DateTime(
                    curDate.Year,
                    curDate.Month,
                    curDate.Day, 
                    14, 00, 00) });
        }
    }
}
