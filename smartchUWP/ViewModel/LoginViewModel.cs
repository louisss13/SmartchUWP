using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        private String _email = "louisss13@gmail.com";
        private String _password = "Coucou-123";

        public RelayCommand CommandLogin { get; private set; }
        public RelayCommand CommandNavigateRegister { get; private set; }

        public String Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
            }
        }
        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            CommandLogin = new RelayCommand(Login);
            CommandNavigateRegister = new RelayCommand(NavigateToRegister);
        }
        private void NavigateToRegister()
        {
            _navigationService.NavigateTo("Register");
        }
        private async void Login()
        {
            AccountsServices accountsServices = new AccountsServices();

            ResponseObject response = await accountsServices.LogIn(Email, Password);

            if (response.Success)
            {
                _navigationService.NavigateTo("Home"); 
            }
            
        }
    }
}
