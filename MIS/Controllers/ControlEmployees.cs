using MIS.DatabaseDataSetTableAdapters;
using MIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Controllers
{
    class ControlEmployees
    {
        private static List<Employee> listEmployees;
        public List<Employee> GetAllEmployees()
        {
            if (listEmployees == null) UpdateTable();
            return listEmployees;
        }
        private void UpdateTable()
        {
            var tableEmployees = new EmployeeTableAdapter().GetData();
            listEmployees = ConvertTo(tableEmployees);
        }
        public List<Employee> ConvertTo(DataTable datatable)
        {
            return datatable.AsEnumerable().Select(m => new Employee(
                id: m.Field<int>("Id"),
                firstName: m.Field<string>("FirstName").Trim(),
                lastName: m.Field<string>("LastName").Trim(),
                middleName: m.Field<string>("MiddleName").Trim(),
                specialization: m.Field<string>("Specialization").Trim()
                )
            ).ToList();
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
