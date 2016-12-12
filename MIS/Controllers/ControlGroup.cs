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
    class ControlGroup : IControl<Group>
    {
        private static List<Group> listGroup;
        public void Add(Group group)
        {
            var adapter = new GroupTableAdapter();
            adapter.InsertQuery(group.Name, group.IdDepartment);
            UpdateTable();
        }
        public List<Group> GetAll()
        {
            if (listGroup == null) UpdateTable();
            return listGroup;
        }
        private void UpdateTable()
        {
            var tableGroups = new GroupTableAdapter().GetData();
            listGroup = ConvertTo(tableGroups);
        }
        private List<Group> ConvertTo(DataTable datatable)
        {
            return datatable.AsEnumerable().Select(m => new Group(
                id: m.Field<int>("Id"),
                name: m.Field<string>("Name").Trim(),
                department: m.Field<string>("Department").Trim(),
                idDepartment: m.Field<int>("IdDepartment")
                )
            ).ToList();
        }
        public void Remove(Group group)
        {
            var adapter = new GroupTableAdapter();
            adapter.DeleteQuery(group.Id);
            UpdateTable();
        }
    }
}
