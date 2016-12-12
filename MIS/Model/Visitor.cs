using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Model
{
    class Visitor
    {
        public Client Client { get; private set; }
        public Employee Doctor { get; private set; }
        public List<Service> ListServices { get; private set; }
        public DateTime VisitDate { get; private set; }
        public decimal Discount { get; private set; }
        public string Remark { get; private set; }

    }
}
