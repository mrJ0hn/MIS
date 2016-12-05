namespace MIS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Appointment
    {
        public int AppointmentID { get; set; }
        public string Subject { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime RecreatedDate { get; set; }
    }
}