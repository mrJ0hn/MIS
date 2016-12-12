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
            comboBoxSpecialization.ItemsSource = controlSpecializations.GetAll();
            gridSpecialization.ItemsSource = controlSpecializations.GetAll();
            gridEmployee.ItemsSource = controlEmployees.GetAll();
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
            gridSpecialization.ItemsSource = controlSpecializations.GetAll();
            comboBoxSpecialization.ItemsSource = controlSpecializations.GetAll();
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var specialization = (Specialization)comboBoxSpecialization.SelectedItem;

            if (txtLastName.Text == null || txtFirstName.Text == null || txtMiddleName.Text == null
                || specialization == null)
            {
                var result = DialogService.ShowMessage(Properties.Resources.FillInAllTheFields, MessageDialogStyle.Affirmative);
                return;
            }
            controlEmployees.Add(new Employee(
                txtLastName.Text.Trim(),
                txtFirstName.Text.Trim(),
                txtMiddleName.Text.Trim(),
                specialization.Id));
            txtLastName.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            comboBoxSpecialization.SelectedItem = null;
            gridEmployee.ItemsSource = controlEmployees.GetAll();
        }

        private void menuItemRemoveSpecialization_Click(object sender, RoutedEventArgs e)
        {
            var specialization = (Specialization)gridSpecialization.SelectedItem;
            if (specialization != null)
            {
                controlSpecializations.Remove(specialization);
                gridSpecialization.ItemsSource = controlSpecializations.GetAll();
                comboBoxSpecialization.ItemsSource = controlSpecializations.GetAll();
            }
        }
        private void menuItemRemoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            var employee = (Employee)gridEmployee.SelectedItem;
            if (employee != null)
            {
                controlEmployees.Remove(employee);
                gridEmployee.ItemsSource = controlEmployees.GetAll();
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
