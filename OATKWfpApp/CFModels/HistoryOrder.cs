using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OATKWfpApp.CFModels
{
    public class HistoryOrder
    {
        public int Id { get; set; }
        public DateTime Finnaly { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
