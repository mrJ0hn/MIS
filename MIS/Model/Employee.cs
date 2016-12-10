using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Model
{
    class Employee
    {
        public int Id { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public int IdSpecialization { get; private set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2}", LastName, FirstName, MiddleName);
            }
        }

        public Employee(string lastName, string firstName, string middleName, int idSpecialization)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            IdSpecialization = idSpecialization;
        }
        public Employee(int id, string lastName, string firstName, string middleName, int idSpecialization)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            IdSpecialization = idSpecialization;
        }
    }
}
