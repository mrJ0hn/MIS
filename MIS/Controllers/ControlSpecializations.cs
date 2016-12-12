using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MIS.DatabaseDataSetTableAdapters;
using MIS.Model;
using System.Data;

namespace MIS.Controllers
{
    class ControlSpecializations: IControl<Specialization>
    {
        private static List<Specialization> listSpecialization;
        public List<Specialization> GetAll()
        {
            if (listSpecialization == null) UpdateTable();
            return listSpecialization;
        }
        public void Add(Specialization specialization)
        {
            var adapter = new SpecializationTableAdapter();
            adapter.InsertQuery(specialization.Name);
            UpdateTable();
        }
        private void UpdateTable()
        {
            var tableSpecialization = new SpecializationTableAdapter().GetData();
            listSpecialization = ConvertTo(tableSpecialization);
        }
        private List<Specialization> ConvertTo(DataTable datatable)
        {
            return datatable.AsEnumerable().Select(m => new Specialization(
                id: m.Field<int>("Id"),
                name: m.Field<string>("Name").Trim()
                )
            ).ToList();
        }
        public void Remove(Specialization specialization)
        {
            var adapter = new SpecializationTableAdapter();
            adapter.DeleteQuery(specialization.Id);
            UpdateTable();
        }
    }
}
