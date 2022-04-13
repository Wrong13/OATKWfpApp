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
    public class AllOrdersVM : INotifyPropertyChanged
    {
        CFModels.OatkContext db;

        IEnumerable<CFModels.Order> orders;
        public IEnumerable<CFModels.Order> Orders
        {
            get { return orders; }
            set { orders = value;
                OnPropertyChanged("Orders");
            }           
        }
        public AllOrdersVM()
        {
            db = new CFModels.OatkContext();
            db.Orders.Load();
            Orders = db.Orders.Local.Where(x => x.IsActual == true).ToList();           
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = " ")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
