using MahApps.Metro.Controls;
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
    /// Логика взаимодействия для СlientСard.xaml
    /// </summary>
    public partial class СlientСardWindow : MetroWindow
    {
        public СlientСardWindow()
        {
            InitializeComponent();
        }

        private void BtnPriceWindow_Click(object sender, RoutedEventArgs e)
        {
            new PriceWindow().Show();
        }
    }
}
