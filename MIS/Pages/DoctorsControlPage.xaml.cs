using MIS.DatabaseDataSetTableAdapters;
using MIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIS.Pages
{
    /// <summary>
    /// Логика взаимодействия для DoctorsControlPage.xaml
    /// </summary>
    public partial class DoctorsControlPage : Page
    {
        private List<Visitor> listSpecializations = new List<Visitor>();
        public DoctorsControlPage()
        {
            InitializeComponent();
            listSpecializations = GetSpecialization();
            comboBoxSpecialization.ItemsSource = listSpecializations;
            gridSpecialization.ItemsSource = new SpecializationTableAdapter().GetData();
        }
        private List<Visitor> GetSpecialization()
        {
            List<Visitor> visitors = new List<Visitor>();
            Visitor visitor = new Visitor()
            {
                FullName = "Александров Александр",
            };
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            visitors.Add(visitor);
            return visitors;
        }
    }
}
