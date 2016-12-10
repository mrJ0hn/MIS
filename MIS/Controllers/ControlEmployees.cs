using MIS.DatabaseDataSetTableAdapters;
using MIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Controllers
{
    class ControlEmployees
    {
        private static DatabaseDataSet.EmployeeDataTable tableEmployees;
        public DatabaseDataSet.EmployeeDataTable GetAllEmployees()
        {
            if (tableEmployees == null) UpdateTable();
            return tableEmployees;
        }

        private void UpdateTable()
        {
            tableEmployees = new EmployeeTableAdapter().GetData();
        }

        public void Add(Employee employee)
        {
            var adapter = new EmployeeTableAdapter();
            adapter.InsertQuery(employee.LastName, employee.FirstName, employee.MiddleName, 
                employee.IdSpecialization);
            UpdateTable();
        }
        public void Remove(Employee employee)
        {
            var adapter = new EmployeeTableAdapter();
            adapter.DeleteQuery(employee.Id);
            UpdateTable();
        }
    }
}
