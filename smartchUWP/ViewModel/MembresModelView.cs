using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class MembresModelView : MainPageViewModel
    {
        public RelayCommand CmdNavigateAddMembre { get; private set; }

        
        private ObservableCollection<User> _users = null;
        public MembresModelView(INavigationService navigationService) : base(navigationService)
        {
            
            CmdNavigateAddMembre = new RelayCommand(NavigateToAddMembre);
            if (IsInDesignMode)
            {
                _users = new ObservableCollection<User> { new User() { Name = "Nom1" }, new User() { Name = "Nm2" } };
            }
            else
            {
                InitializeAsync();
            }
        }
        private void NavigateToAddMembre()
        {
            _navigationService.NavigateTo("AddMembre");
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
            var response = await service.GetUsers();
            if (response.Success)
            {
                List<User> users = ((List<Object>)response.Content).Cast<User>().ToList();
                Users = new ObservableCollection<User>(users);
            }
        }
    }
}
