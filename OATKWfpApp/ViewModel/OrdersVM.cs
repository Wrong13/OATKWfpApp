using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OATKWfpApp.ViewModel
{
    public class OrdersVM : INotifyPropertyChanged
    {
        CFModels.OatkContext db;

        class FiltersCmb
        {
            public string Name { get; set; }
        }

        IEnumerable<CFModels.Order> orders;
        IEnumerable<CFModels.Client> clients;


        public IEnumerable<CFModels.Order> Orders
        {
            get { return orders; }
            set { orders = value;
                OnPropertyChanged("Orders");
            }           
        }
        public IEnumerable<CFModels.Client> Clients
        {
            get { return clients; }
            set { clients = value;
                OnPropertyChanged("Clients");
                }
        }

        private RelayCommand paidStatusOrder;
        private RelayCommand delOrder;
        
        public RelayCommand PaidStatusOrder
        {
            get
            {
                return paidStatusOrder ??
                    (paidStatusOrder = new RelayCommand((selectedItem) =>
                    {
                        //if (selectedItem == null)
                        //    return;
                        CFModels.Order order = selectedItem as CFModels.Order;
                        int orderID = order.Id;
                        order = db.Orders.Find(orderID);
                        order.IsBuy = true;
                        db.Entry(order).State = EntityState.Modified;
                        db.SaveChanges();
                    }));
            }
        }
        
        


        public OrdersVM()
        {


            //var ThisUser = db.Users
            //    .Where(x => x.UserID == ThisUserId)
            //    .FirstOrDefault();

            //if (ThisUser.UserRole.Name == "kass")
            //    Orders = db.Orders.Local.Where(x => x.IsActual == true).ToList();
            //else

            

            LoadOrders();
        }
        public void LoadOrders(string Filter = null)
        {

            db = new CFModels.OatkContext();
            db.Clients.Load();
            db.Orders.Load();
            db.UserRoles.Load();
            Clients = db.Clients.Local.ToBindingList();
            
            if (Filter == null)
                Orders = db.Orders.Local.ToBindingList();
            else
            {
                orders = null;

                if (Filter.Contains("Цене"))
                {
                    orders = db.Orders.OrderBy(x => x.Price).ToList();
                    OnPropertyChanged("Orders");
                }
                else if (Filter.Contains("Имени"))
                {
                    orders = db.Orders.OrderBy(x => x.NameProduct).ToList();
                    OnPropertyChanged("Orders");
                }
            }
            db.Orders.Load();
        }

        public RelayCommand DelOrder
        {
            get
            {
                return delOrder ?? (delOrder = new RelayCommand((selectedItem) =>
                {
                    if (selectedItem == null)
                       return;

                    var order = selectedItem as CFModels.Order;
                    
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }));
            }
        }

        private string selectedUnit = "";
        public string SelectedUnit
        {
            get => selectedUnit;
            set
            {
                selectedUnit = value;

                LoadOrders(selectedUnit);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = " ")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
