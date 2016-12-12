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
    class ControlService : IControl<Service>
    {
        private static List<Service> listService;
        public void Add(Service service)
        {
            var adapter = new ServiceTableAdapter();
            adapter.InsertQuery(service.IdDepartment, service.IdGroup, service.Name, service.Cost);
            UpdateTable();
        }
        public List<Service> GetAll()
        {
            if (listService == null) UpdateTable();
            return listService;
        }

        public void Remove(Service service)
        {
            var adapter = new ServiceTableAdapter();
            adapter.DeleteQuery(service.Id);
            UpdateTable();
        }

        private void UpdateTable()
        {
            var tableServices = new ServiceTableAdapter().GetData();
            listService = ConvertTo(tableServices);
        }
        private List<Service> ConvertTo(DataTable datatable)
        {
            return datatable.AsEnumerable().Select(m => new Service(
                id: m.Field<int>("Id"),
                idDepartment: m.Field<int>("IdDepartment"),
                department: m.Field<string>("Department").Trim(),
                idGroup: m.Field<int>("IdGroup"),
                group: m.Field<string>("Group").Trim(),
                name: m.Field<string>("Name").Trim(),
                cost: m.Field<decimal>("Cost")
                )
            ).ToList();
        }
    }
}
