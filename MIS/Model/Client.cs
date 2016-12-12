using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Model
{
    class Client
    {
        public int Id { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        
        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2}", LastName, FirstName, MiddleName);
            }
        }
    }
}
