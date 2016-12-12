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
using MahApps.Metro.Controls;

namespace MIS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSchedule_Click(object sender, RoutedEventArgs e)
        {
            SetPageInMainFrame(new Pages.SchedulePage());
        }

        private void btnControl_Click(object sender, RoutedEventArgs e)
        {
            SetPageInMainFrame(new Pages.ControlPage());
        }

        private void SetPageInMainFrame(Page page)
        {
            frameMain.NavigationService.Navigate(page);
            frameMain.NavigationService.RemoveBackEntry();
        }

        private void btnPatientControl_Click(object sender, RoutedEventArgs e)
        {
            SetPageInMainFrame(new Pages.PatientPageControl());
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            new СlientСardWindow().Show();
        }
    }
}
