using System;
using System.Collections;
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

namespace MIS
{
    /// <summary>
    /// Логика взаимодействия для CustomCalendar.xaml
    /// </summary>
    public partial class CustomCalendar : UserControl
    {
        private DateTime beginningWeek;
        private IEnumerable itemsSource;
        public IEnumerable ItemsSource
        {
            get { return itemsSource; }
            set {
                itemsSource = value;
                UpdateUI(value);
            }
        }

        private void UpdateUI(IEnumerable value)
        {
            gridMondey.ItemsSource = value;
        }

        public CustomCalendar()
        {
            InitializeComponent();
            beginningWeek = DateTime.Now.Date;
            UpdateTitleDataGrid();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToHorizontalOffset(scv.HorizontalOffset - e.Delta);
            e.Handled = true;
        }

        private void UpdateTitleDataGrid()
        {
            var curDay = FindMondey(beginningWeek);
            txtMondey.Content = "Пн " + curDay.Date.ToShortDateString();
            curDay = curDay.AddDays(1);
            txtTuesday.Content = "Вт " + curDay.Date.ToShortDateString();
            curDay = curDay.AddDays(1);
            txtWenesday.Content = "Ср " + curDay.Date.ToShortDateString();
            curDay = curDay.AddDays(1);
            txtThursday.Content = "Чт " + curDay.Date.ToShortDateString();
            curDay = curDay.AddDays(1);
            txtFriday.Content = "Пт " + curDay.Date.ToShortDateString();
            curDay = curDay.AddDays(1);
            txtSaturday.Content = "Сб " + curDay.Date.ToShortDateString();
        }

        private DateTime FindMondey(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }
            return date;
        }
    }
}
