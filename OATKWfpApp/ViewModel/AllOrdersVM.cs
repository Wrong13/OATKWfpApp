using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OATKWfpApp.ViewModel
{
    public class AllOrdersVM : INotifyPropertyChanged
    {
        CFModels.OatkContext db;

        IEnumerable<CFModels.Order> orders;
        IEnumerable<CFModels.Client> clients;


        public IEnumerable<CFModels.Order> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                OnPropertyChanged("Orders");
            }
        }
        public IEnumerable<CFModels.Client> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                OnPropertyChanged("Clients");
            }
        }

        public AllOrdersVM()
        {
            db = new CFModels.OatkContext();
            db.Clients.Load();
            db.Orders.Load();
            db.UserRoles.Load();

            Clients = db.Clients.Local.ToBindingList();
            Orders = db.Orders.Local.ToBindingList();
        }

        private RelayCommand paidStatusOrder;
        private RelayCommand delOrder;
        private RelayCommand goPackOrder;
        private string selectedUnit = "";
        private RelayCommand findUnit;

        public RelayCommand GoPackOrder
        {
            get
            {
                return goPackOrder ??
                    (goPackOrder = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null)
                            return;
                        CFModels.Order order = selectedItem as CFModels.Order;
                        CFModels.PackOrder packOrder = new CFModels.PackOrder();

                        packOrder.OrderId = order.Id;
                        if (db.PackOrders.Select(x => x.OrderId == packOrder.OrderId).FirstOrDefault() == true)
                        {
                            MessageBox.Show("Такая запись уже имеется");
                            return;
                        }

                        packOrder.IsPack = false;
                        order.IsActual = false;
                        db.PackOrders.Add(packOrder);
                        db.Entry(order).State = EntityState.Modified;
                        db.SaveChanges();
                        OnPropertyChanged("Orders");

                    }));
            }
        }

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

        public RelayCommand FindUnit
        {
            get
            {
                return findUnit ?? (findUnit = new RelayCommand((FindText) =>
                {
                    orders = Orders.Where(x => x.Id.ToString().Contains(FindText.ToString())).ToList();
                    OnPropertyChanged("Orders");
                }));
            }
        }

        public string SelectedUnit
        {
            get => selectedUnit;
            set
            {
                selectedUnit = value;


                if (selectedUnit.Contains("Цене"))
                {
                    orders = Orders.OrderBy(x => x.Price).ToList();
                    OnPropertyChanged("Orders");
                }
                else if (selectedUnit.Contains("Имени"))
                {
                    orders = Orders.OrderBy(x => x.NameProduct).ToList();
                    OnPropertyChanged("Orders");
                }
                else if (selectedUnit.Contains("Порядку"))
                {
                    orders = Orders.OrderBy(x => x.Id).ToList();
                    OnPropertyChanged("Orders");
                }
            }
        }

        public string SelectedUnitPay
        {
            get => selectedUnit;
            set
            {
                selectedUnit = value;


                if (selectedUnit.Contains("Все"))
                {
                    Orders = db.Orders.Local.ToBindingList();
                    OnPropertyChanged("Orders");
                }
                else if (selectedUnit.Contains("Оплаченные"))
                {
                    Orders = db.Orders.Local.ToBindingList();
                    orders = Orders.Where(x => x.IsBuy == true).ToList();
                    OnPropertyChanged("Orders");
                }
                else if (selectedUnit.Contains("Не оплаченные"))
                {
                    Orders = db.Orders.Local.ToBindingList();
                    orders = Orders.Where(x => x.IsBuy == false).ToList();
                    OnPropertyChanged("Orders");
                }
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
