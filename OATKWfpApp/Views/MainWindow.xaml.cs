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
        OatkContext db;
        public int IdUser = 0;

        public MainWindow(int UserId)
        {
            InitializeComponent();
            db = new OatkContext();

            IdUser = UserId;

            var ThisUser = db.Users.Where(x => x.UserID == UserId).FirstOrDefault();
            string userRole = db.UserRoles.Where(x=> x.ID == ThisUser.UserRoleID).Select(x=>x.Name).FirstOrDefault();
            if (userRole == "kass")
            {
                AllUsersBtn.Visibility = Visibility.Collapsed;
                HistoryOrdersBtn.Visibility = Visibility.Collapsed;
            }
            else if (userRole == "superadmin")
            {

            }
        }

        private void AllOrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrm.Navigate(new Uri("/Pages/PageAllOrders.xaml",UriKind.Relative));
        }

        private void AllUsersBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrm.Navigate(new Uri("/Pages/AllUsersPage.xaml", UriKind.Relative));
        }
    }
}
