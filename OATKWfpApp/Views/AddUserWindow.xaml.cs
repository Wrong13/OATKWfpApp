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

namespace OATKWfpApp.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        CFModels.OatkContext db;
        public CFModels.User User { get; private set; }
        public AddUserWindow(CFModels.User user)
        {
            db = new CFModels.OatkContext();
            InitializeComponent();
            
            UserRolesCmbBox.ItemsSource = db.UserRoles.Select(x => x.Name).ToList();
            UserRolesCmbBox.SelectedValue(db.UserRoles.Select(x => x.Name).ToString());
            

            this.DataContext = User;
            User = user;
        }

        private void AddNewUserBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(UserRolesCmbBox.SelectedValuePath.ToString());
            this.DialogResult = true;
        }

        private void UserRolesCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string role = UserRolesCmbBox.SelectedValuePath;
            MessageBox.Show(role);
        }
    }
}
