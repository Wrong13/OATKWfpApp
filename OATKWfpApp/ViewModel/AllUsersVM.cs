﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OATKWfpApp.ViewModel
{
    public class AllUsersVM : INotifyPropertyChanged
    {
        CFModels.OatkContext db;

        IEnumerable<CFModels.User> users;
        IEnumerable<CFModels.UserRole> userRoles;

        public IEnumerable<CFModels.User> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged("Users");
            }
        }
        public IEnumerable<CFModels.UserRole> UserRoles
        {
            get { return userRoles; }
            set
            {
                userRoles = value;
                OnPropertyChanged("UserRoles");
            }
        }
        public AllUsersVM()
        {
            db = new CFModels.OatkContext();
            db.Users.Load();
            db.UserRoles.Load();

            Users = db.Users.Local.ToBindingList();
            UserRoles = db.UserRoles.Local.ToBindingList();
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = " ")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}