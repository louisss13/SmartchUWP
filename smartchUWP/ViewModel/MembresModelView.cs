using DataAccess;
using GalaSoft.MvvmLight;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class MembresModelView : ViewModelBase
    {
        private ObservableCollection<User> _users = null;
        public MembresModelView()
        {
            if (IsInDesignMode)
            {
                _users = new ObservableCollection<User> { new User() { Name = "Nom1" }, new User() { Name = "Nm2" } };
            }
            else
            {
                InitializeAsync();
            }
        }
        public ObservableCollection<User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                if (_users == value)
                {
                    return;
                }
                _users = value;
                RaisePropertyChanged("Users");
            }
        }
        public async Task InitializeAsync()
        {
            var service = new UsersServices();
            var users = await service.GetUsers();
            Users = new ObservableCollection<User>(users);

        }
    }
}
