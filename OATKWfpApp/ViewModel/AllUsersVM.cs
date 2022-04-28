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

        private RelayCommand addUser;
        private RelayCommand removeUser;
        private RelayCommand editUser;

        public RelayCommand AddUser
        {
           get
            {
                return addUser ?? (addUser = new RelayCommand((o) =>
                {
                    Views.AddUserWindow userWindow = new Views.AddUserWindow(new CFModels.User());
                    if (userWindow.ShowDialog() == true)
                    {
                        CFModels.User user = userWindow.User;
                        
                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                }));
            }
        }

        public RelayCommand RemoveUser
        {
            get
            {
                return removeUser ?? (removeUser = new RelayCommand((selectedUser) =>
                {
                    if (selectedUser == null)
                        return;
                    CFModels.User removeUser = selectedUser as CFModels.User;
                    db.Users.Remove(removeUser);
                    db.SaveChanges();
                }));
            }
        }

        public RelayCommand EditUser    
        {
            get
            {
                return editUser ?? (editUser = new RelayCommand((selectedUser) =>
                {
                    if (selectedUser == null)
                        return;
                    CFModels.User editUser = selectedUser as CFModels.User;
                    Views.AddUserWindow editUserWindow = new Views.AddUserWindow(editUser);
                    if (editUserWindow.ShowDialog() == true)
                    {
                        editUser = editUserWindow.User;
                        db.Entry(editUser).State = EntityState.Modified;
                        db.SaveChanges();
                        
                    }
                }));
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
