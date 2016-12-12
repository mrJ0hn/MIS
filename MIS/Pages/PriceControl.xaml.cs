using MahApps.Metro.Controls.Dialogs;
using MIS.Controllers;
using MIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для PriceControl.xaml
    /// </summary>
    public partial class PriceControl : Page
    {
        private ControlDepartment controlDepartment;
        private ControlGroup controlGroup;
        private ControlService controlService;
        public PriceControl()
        {
            InitializeComponent();
            controlDepartment = new ControlDepartment();
            controlGroup = new ControlGroup();
            controlService = new ControlService();
            UpdateDepartments();
            UpdateGroups();
            UpdateServices();
        }

        private void btnAddDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (txtDepartment.Text == null || txtDepartment.Text.Trim().Length == 0)
            {
                var result = DialogService.ShowMessage(Properties.Resources.FillInAllTheFields, MessageDialogStyle.Affirmative);
                return;
            }
            controlDepartment.Add(
                new Department(txtDepartment.Text.Trim()));
            txtDepartment.Text = string.Empty;
            UpdateDepartments();
        }
        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            var selectedDepartment = (Department)comboBoxDepartment.SelectedItem;
            var selectedGroup = (Model.Group)comboBoxGroup.SelectedItem;
            decimal cost = 0;
            if (selectedDepartment == null || txtServiceName.Text == null || txtServiceName.Text.Trim().Length == 0
                || txtCost.Text == null || txtCost.Text.Trim().Length == 0 || !decimal.TryParse(txtCost.Text, out cost))
            {
                var result = DialogService.ShowMessage(Properties.Resources.FillInAllTheFields, MessageDialogStyle.Affirmative);
                return;
            }
            controlService.Add(new Service(
                   selectedDepartment.Id,
                   selectedGroup.Id,
                   txtServiceName.Text.Trim(),
                   cost
                   ));
            txtServiceName.Clear();
            txtCost.Clear();
            UpdateServices();
        }
        private void btnAddGroup_Click(object sender, RoutedEventArgs e)
        {
            var departemnt = (Department)comboBoxDepartmentInGroup.SelectedItem;
            if (txtNameGroup.Text == null || txtNameGroup.Text.Trim().Length == 0 ||
                departemnt == null)
            {
                var result = DialogService.ShowMessage(Properties.Resources.FillInAllTheFields, MessageDialogStyle.Affirmative);
                return;
            }
            controlGroup.Add(new Model.Group(departemnt.Id, txtNameGroup.Text));
            txtNameGroup.Text = string.Empty;
            UpdateGroups();
        }

        private void menuItemRemoveDepartment_Click(object sender, RoutedEventArgs e)
        {
            var department = (Department)gridDepartment.SelectedItem;
            if (department != null)
            {
                controlDepartment.Remove(department);
                UpdateDepartments();
            }
        }

        private void menuItemRemoveService_Click(object sender, RoutedEventArgs e)
        {
            var service = (Service)gridPrice.SelectedItem;
            if (service != null)
            {
                controlService.Remove(service);
                UpdateServices();
            }
        }

        private void menuItemRemoveGroup_Click(object sender, RoutedEventArgs e)
        {
            var group = (Model.Group)gridGroup.SelectedItem;
            if (group != null)
            {
                controlGroup.Remove(group);
                UpdateGroups();
            }
        }
        
        private void TextBoxService_KeyEnterUpdate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) btnAddService_Click(null, null);
        }
        private void TextBoxDepartment_KeyEnterUpdate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) btnAddDepartment_Click(null, null);
        }
        private void TextBoxGroup_KeyEnterUpdate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) btnAddGroup_Click(null, null);
        }

        private void UpdateGroups()
        {
            gridGroup.ItemsSource = comboBoxGroup.ItemsSource = controlGroup.GetAll();
        }
        private void UpdateDepartments()
        {
            gridDepartment.ItemsSource = comboBoxDepartment.ItemsSource = comboBoxDepartmentInGroup.ItemsSource
                = controlDepartment.GetAll();
        }
        private void UpdateServices()
        {
            gridPrice.ItemsSource = controlService.GetAll();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^-*[0-9\.]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
