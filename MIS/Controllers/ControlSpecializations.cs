using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MIS.DatabaseDataSetTableAdapters;
using MIS.Model;

namespace MIS.Controllers
{
    class ControlSpecializations
    {
        private static DatabaseDataSet.SpecializationDataTable tableSpecialization;

        public DatabaseDataSet.SpecializationDataTable GetAllSpecializations()
        {
            if (tableSpecialization == null) UpdateTable();
            return tableSpecialization;
        }

        public void Add(Specialization specialization)
        {
            var adapter = new SpecializationTableAdapter();
            adapter.InsertQuery(specialization.Name);
            UpdateTable();
        }

        private void UpdateTable()
        {
            tableSpecialization = new SpecializationTableAdapter().GetData();
        }

        internal void Remove(Specialization specialization)
        {
            var adapter = new SpecializationTableAdapter();
            adapter.DeleteQuery(specialization.Id);
            UpdateTable();
        }
    }
}
