using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Model
{
    class Specialization
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Specialization(string name)
        {
            Name = name;
        }
        public Specialization(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
