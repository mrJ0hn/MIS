using MahApps.Metro.Controls;
using MIS.Controllers;
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
using System.Windows.Shapes;

namespace MIS
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClientWindow : MetroWindow
    {
        private ControlEmployees controlEmployees;
        public AddClientWindow()
        {
            InitializeComponent();
            controlEmployees = new ControlEmployees();
            comboBoxEmployee.ItemsSource = controlEmployees.GetAllEmployees();
        }
    }
}
