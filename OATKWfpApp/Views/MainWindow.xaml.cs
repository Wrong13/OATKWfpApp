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
using OATKWfpApp.CFModels;

namespace OATKWfpApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AllOrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrm.Navigate(new Uri("/Pages/PageAllOrders.xaml",UriKind.Relative));
        }
    }
}
