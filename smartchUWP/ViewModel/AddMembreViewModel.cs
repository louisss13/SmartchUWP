using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using smartchUWP.Observable;
using smartchUWP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class AddMembreViewModel : MainPageViewModel
    {
        public RelayCommand CommandAddMember { get; private set; }

        private INavigationService _navigationService;
        private ObservableUserInfo _user = new ObservableUserInfo();

        public ObservableUserInfo User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                RaisePropertyChanged("User");
            }
        }

        public AddMembreViewModel(INavigationService navigationService) : base(navigationService)
        {
            CommandAddMember = new RelayCommand(AddMembre);
            if (IsInDesignMode)
            {
                
            }
            else
            {
                InitializeAsync();
            }
        }
        
        public async Task InitializeAsync()
        {
            

        }
        private async void AddMembre()
        {
            UsersServices usersServices = new UsersServices();
            ResponseObject response = await usersServices.AddUser(User);
            if (response.Success)
            {
                _navigationService.NavigateTo("Membres");
                MessengerInstance.Send(new NotificationMessage(NotificationMessageType.ListUser));
            }
            else
            {

            }
        }
    }
}
