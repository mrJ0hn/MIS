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
    class ControlDepartment:IControl<Department>
    {
        private static List<Department> listDepartment;
        public List<Department> GetAll()
        {
            if (listDepartment == null) UpdateTable();
            return listDepartment;
        }
        public void Add(Department department)
        {
            var adapter = new DepartmentTableAdapter();
            adapter.InsertQuery(department.Name);
            UpdateTable();
        }
        private List<Department> ConvertTo(DataTable datatable)
        {
            return datatable.AsEnumerable().Select(m => new Department(
                id: m.Field<int>("Id"),
                name: m.Field<string>("Name").Trim()
                )
            ).ToList();
        }
        private void UpdateTable()
        {
            var tableDepartment = new DepartmentTableAdapter().GetData();
            listDepartment = ConvertTo(tableDepartment);
        }
        public void Remove(Department department)
        {
            var adapter = new DepartmentTableAdapter();
            adapter.DeleteQuery(department.Id);
            UpdateTable();
        }
    }
}
