using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Model
{
    class Service
    {
        public int Id { get; private set; }
        public int IdDepartment { get; private set; }
        public int IdGroup { get; private set; }
        public string Department { get; private set; }
        public string Group { get; private set; }
        public string Name { get; private set; }
        public decimal Cost { get; private set; }

        public Service(int idDepartment, int idGroup, string name, decimal cost) 
            : this(-1, idDepartment, null, idGroup, null, name, cost) { }
        public Service(int id, int idDepartment, string department, int idGroup, string group,
            string name, decimal cost)
        {
            Id = id;
            IdDepartment = idDepartment;
            Department = department;
            IdGroup = idGroup;
            Group = group;
            Name = name;
            Cost = cost;
        }
    }
}
