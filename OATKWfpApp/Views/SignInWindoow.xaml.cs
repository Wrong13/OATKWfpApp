using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using OATKWfpApp.CFModels;

namespace OATKWfpApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public OatkContext db;
        public MainWindow()
        {
            InitializeComponent();
            db = new OatkContext();
        }

        private void Close_Window(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private async void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            var ThisUser = await db.Users
                .Where(x => x.Login == LoginBox.Text)
                .Where(x => x.Password == PassBox.Password)
                .FirstOrDefaultAsync();
            if (ThisUser != null)
            {
                Views.MainWindow main = new Views.MainWindow(ThisUser.UserID);
                this.Close();
                main.Show();
            }
            else
                MessageBox.Show("Не удалось найти запись");
        }
    }
}
