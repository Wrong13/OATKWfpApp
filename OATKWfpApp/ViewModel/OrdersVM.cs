using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OATKWfpApp.ViewModel
{
    public class OrdersVM : INotifyPropertyChanged
    {
        CFModels.OatkContext db;

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
        public RelayCommand PaidStatusOrder
        {
            get
            {
                return paidStatusOrder ??
                    (paidStatusOrder = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null)
                            return;
                        CFModels.Order order = selectedItem as CFModels.Order;
                        int orderID = order.Id;
                        order = db.Orders.Find(orderID);
                        order.IsBuy = true;
                        db.Entry(order).State = EntityState.Modified;
                        db.SaveChanges();
                    }));
            }
        }
        private RelayCommand delOrder;
        public RelayCommand DelOrder
        {
            get
            {
                return delOrder ?? (delOrder = new RelayCommand((selectedItem) =>
                {
                    
                    CFModels.Order order = selectedItem as CFModels.Order;
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }));
            }
        }


        public OrdersVM(int ThisUserId)
        {
            db = new CFModels.OatkContext();
            db.Clients.Load();
            db.Orders.Load();
            db.UserRoles.Load();

            var ThisUser = db.Users
                .Where(x => x.UserID == ThisUserId)
                .FirstOrDefault();

            if (ThisUser.UserRole.Name == "kass")
                Orders = db.Orders.Local.Where(x => x.IsActual == true).ToList();
            else
                Orders = db.Orders.Local.ToBindingList();

            Clients = db.Clients.Local.ToBindingList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = " ")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
