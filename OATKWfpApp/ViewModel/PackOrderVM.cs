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
    public class PackOrderVM : INotifyPropertyChanged
    {
        IEnumerable<CFModels.Order> orders;
        IEnumerable<CFModels.PackOrder> packOrders;
        CFModels.OatkContext db;

        public IEnumerable<CFModels.PackOrder> PackOrders
        {
            get { return packOrders; }
            set
            {
                packOrders = value;
                OnPropertyChanged("PackOrders");
            }
        }

        public IEnumerable<CFModels.Order> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                OnPropertyChanged("Orders");
            }
        }

        public PackOrderVM()
        {
            db = new CFModels.OatkContext();
            db.Orders.Load();
            db.PackOrders.Load();

            Orders = db.Orders.Local.ToBindingList();
            PackOrders = db.PackOrders.Where(x=>x.IsPack == false).ToList();
        }

        private void UpdatePackOrders()
        {
            PackOrders = db.PackOrders.Where(x => x.IsPack == false).ToList();
            OnPropertyChanged("PackOrders");
        }

        private RelayCommand issueOrder;
        private RelayCommand cancelOrder;
        private string selectedUnit;
        private RelayCommand findUnit;

        public RelayCommand IssueOrder
        {
            get
            {
                return issueOrder ?? (issueOrder = new RelayCommand((selectedOrder) =>
                {
                    if (selectedOrder == null)
                        return;
                    var issOrder = selectedOrder as CFModels.PackOrder;

                    issOrder.IsPack = true;
                    db.Entry(issOrder).State = EntityState.Modified;
                    db.SaveChanges();
                    UpdatePackOrders();
                }));
            }
        }

        public RelayCommand CancelOrder
        {
            get
            {
                return cancelOrder ?? (cancelOrder = new RelayCommand((selectedOrder) =>
                {
                    if (selectedOrder == null)
                        return;
                    var canOrderPack = selectedOrder as CFModels.PackOrder;
                    var canOrder = db.Orders.Where(x => x.Id == canOrderPack.OrderId).FirstOrDefault();
                    db.Orders.Remove(canOrder);
                    db.SaveChanges();
                    UpdatePackOrders();
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

                if (selectedUnit.Contains("Имени"))
                {
                    packOrders = PackOrders.OrderBy(x => x.Order.NameProduct).ToList();
                    OnPropertyChanged("Orders");
                }
                else if (selectedUnit.Contains("Порядку"))
                {
                    packOrders = PackOrders.OrderBy(x => x.Id).ToList();
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
