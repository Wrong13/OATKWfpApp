using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OATKWfpApp.CFModels
{
    public class Order
    {
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public int ClientId { get; set; }
        public bool IsBuy { get; set; }
        public DateTime Created { get; set; }
        public bool IsActual { get; set; }
        public decimal Price { get; set; }
        public Client Client { get; set; }
        public List<HistoryOrder> HistoryOrders  { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
