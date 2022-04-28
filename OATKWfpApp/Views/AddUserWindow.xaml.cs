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

            User = user;
            this.DataContext = User;
            
        }

        private void AddNewUserBtn_Click(object sender, RoutedEventArgs e)
        {
            Object selectedItem = UserRolesCmbBox.SelectedItem;
            User.UserRoleID = db.UserRoles.Where(x => x.Name == selectedItem.ToString()).Select(x=>x.ID).FirstOrDefault();
            
            this.DialogResult = true;
        }
    }
}
