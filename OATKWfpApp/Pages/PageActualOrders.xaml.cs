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

namespace OATKWfpApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAllOrders.xaml
    /// </summary>
    public partial class PageAllOrders : Page
    {
        public PageAllOrders(int UserId)
        {
            InitializeComponent();
            this.DataContext = new ViewModel.OrdersVM(UserId);
        }

        
    }
}
