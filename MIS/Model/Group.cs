using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Model
{
    class Group
    {
        public int Id { get; private set; }
        public int IdDepartment { get; private set; }
        public string Department { get; private set; }
        public string Name { get; private set; }

        public Group(int idDepartment, string name)
        {
            IdDepartment = idDepartment;
            Name = name;
        }
        public Group(int id, string name, int idDepartment, string department)
        {
            Id = id;
            IdDepartment = idDepartment;
            Department = department;
            Name = name;
        }
    }
}
