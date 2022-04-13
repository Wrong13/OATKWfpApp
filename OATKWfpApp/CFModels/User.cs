using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OATKWfpApp.CFModels
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(20)]
        public string Login { get; set; }
        [MaxLength(15)]
        public string Password { get; set; }
        public int UserRoleID { get; set; }
        public UserRole UserRole { get; set; }
    }
}
