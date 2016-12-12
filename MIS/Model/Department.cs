using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Model
{
    class Department
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Department(string name)
        {
            Name = name;
        }
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
