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
        
        private void UpdateUsers()
        {
            Users = db.Users.Local.ToBindingList();
            OnPropertyChanged("Users");
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
        private string selectedUnit;
        private RelayCommand findUnit;

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
                    UpdateUsers();
                }));
            }
        }

        public RelayCommand FindUnit
        {
            get
            {
                return findUnit ?? (findUnit = new RelayCommand((FindText) =>
                {
                    users = Users.Where(x => x.UserID.ToString().Contains(FindText.ToString())).ToList();
                    OnPropertyChanged("Users");
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
                    users = Users.OrderBy(x => x.UserName).ToList();
                    OnPropertyChanged("Users");
                }
                else if (selectedUnit.Contains("Порядку"))
                {
                    users = Users.OrderBy(x => x.UserID).ToList();
                    OnPropertyChanged("Users");
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
