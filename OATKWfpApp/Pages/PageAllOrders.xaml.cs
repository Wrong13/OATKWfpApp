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

namespace OATKWfpApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAllOrders.xaml
    /// </summary>
    public partial class PageAllOrders : Page
    {
        CFModels.OatkContext db;
        public PageAllOrders()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.AllOrdersVM(); 
            db = new CFModels.OatkContext();
            OrderListBox.ItemsSource = db.Orders.Join(db.Clients,
                o => o.ClientId, c => c.Id,
            (o, c) => new
            {
                OrderId = o.Id,
                ClientName = c.Name,
                NameProd = o.NameProduct,
                Price = o.Price
            }).ToList();
        }
    }
}
