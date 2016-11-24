﻿using System;
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
    /// Логика взаимодействия для ControlPage.xaml
    /// </summary>
    public partial class ControlPage : Page
    {
        public ControlPage()
        {
            InitializeComponent();
        }

        private void listTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox list = sender as ListBox;
            ListBoxItem selectedItem = (ListBoxItem)list.SelectedItem;
            if (selectedItem != null)
            {
                if (selectedItem.Name == itemSpecializationControl.Name)
                {
                    SetPageInFrame(new SpecializationControlPage());
                }
                else if (selectedItem.Name == itemDoctorsControl.Name)
                {
                    SetPageInFrame(new DoctorsControlPage());
                }
            }
        }
        private void SetPageInFrame(Page page)
        {
            frameControl.NavigationService.Navigate(page);
            frameControl.NavigationService.RemoveBackEntry();
        }
    }
}
