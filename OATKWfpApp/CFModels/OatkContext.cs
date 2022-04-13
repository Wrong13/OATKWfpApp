using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OATKWfpApp.CFModels
{
    public class OatkContext : DbContext
    {
        public OatkContext() : base("DefaultConnection") 
            {

            }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<HistoryOrder> HistoryOrders { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
