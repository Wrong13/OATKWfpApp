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
    public class ActualOrdersVM : INotifyPropertyChanged
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

        public ActualOrdersVM()
        {
            db = new CFModels.OatkContext();
            db.Clients.Load();
            db.Orders.Load();
            

            Clients = db.Clients.Local.ToBindingList();
            Orders = db.Orders.Local.Where(x=> x.IsActual == true).ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = " ")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
