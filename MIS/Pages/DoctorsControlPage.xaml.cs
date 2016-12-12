using MIS.Controllers;
using MIS.DatabaseDataSetTableAdapters;
using MIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace MIS.Pages
{
    /// <summary>
    /// Логика взаимодействия для DoctorsControlPage.xaml
    /// </summary>
    public partial class DoctorsControlPage : Page
    {
        private ControlSpecializations controlSpecializations;
        private ControlEmployees controlEmployees;

        public DoctorsControlPage()
        {
            InitializeComponent();
            controlSpecializations = new ControlSpecializations();
            controlEmployees = new ControlEmployees();
            comboBoxSpecialization.ItemsSource = controlSpecializations.GetAllSpecializations();
            gridSpecialization.ItemsSource = controlSpecializations.GetAllSpecializations();
            gridEmployee.ItemsSource = controlEmployees.GetAllEmployees();
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

        private void btnAddSpecialization_Click(object sender, RoutedEventArgs e)
        {
            if (txtSpecialization.Text == null || txtSpecialization.Text.Trim().Length == 0)
            {
                var result = DialogService.ShowMessage(Properties.Resources.FillInAllTheFields, MessageDialogStyle.Affirmative);
                return;
            }
            controlSpecializations.Add(new Specialization(txtSpecialization.Text.Trim()));
            txtSpecialization.Clear();
            gridSpecialization.ItemsSource = controlSpecializations.GetAllSpecializations();
            comboBoxSpecialization.ItemsSource = controlSpecializations.GetAllSpecializations();
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var a = (DataRowView)comboBoxSpecialization.SelectedItem;

            if (txtLastName.Text == null || txtFirstName.Text == null || txtMiddleName.Text == null
                || a == null)
            {
                var result = DialogService.ShowMessage(Properties.Resources.FillInAllTheFields, MessageDialogStyle.Affirmative);
                return;
            }
            controlEmployees.Add(new Employee(
                txtLastName.Text.Trim(),
                txtFirstName.Text.Trim(),
                txtMiddleName.Text.Trim(),
                (int)a.Row["Id"]));
            txtLastName.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            comboBoxSpecialization.SelectedItem = null;
            gridEmployee.ItemsSource = controlEmployees.GetAllEmployees();
        }

        private void menuItemRemoveSpecialization_Click(object sender, RoutedEventArgs e)
        {
            var a = (DataRowView)gridSpecialization.SelectedItem;
            if (a != null)
            {
                var specialization = new Specialization(
                    (int)a.Row["Id"],
                    a.Row["Name"] as string
                    );
                controlSpecializations.Remove(specialization);
                gridSpecialization.ItemsSource = controlSpecializations.GetAllSpecializations();
                comboBoxSpecialization.ItemsSource = controlSpecializations.GetAllSpecializations();
            }
        }
        private void menuItemRemoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            var a = (DataRowView)gridEmployee.SelectedItem;
            if (a != null)
            {
                var employee = new Employee(
                    (int)a.Row["Id"],
                    a.Row["LastName"] as string,
                    a.Row["FirstName"] as string,
                    a.Row["MiddleName"] as string,
                    -1
                    );
                controlEmployees.Remove(employee);
                gridEmployee.ItemsSource = controlEmployees.GetAllEmployees();
            }
        }

        private void TextBoxSpecialization_KeyEnterUpdate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) btnAddSpecialization_Click(null, null);
        }
        private void TextBoxEmployee_KeyEnterUpdate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) btnAddEmployee_Click(null, null);
        }
    }
}
