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
        private bool _isChargement = false;
        private bool _isErrorConnection = false;

        private String _email = "louisss13@gmail.com";
        private String _password = "Coucou-123";

        public bool IsNotChargement
        {
            get
            {
                return !IsChargement;
            }
            set
            {
                IsChargement = !value;
                RaisePropertyChanged("IsNotChargement");
                RaisePropertyChanged("IsChargement");
            }
        }
        public bool IsChargement {
            get
            {
                return _isChargement;
            }
            set
            {
                _isChargement = value;
                RaisePropertyChanged("IsNotChargement");
                RaisePropertyChanged("IsChargement");
            }
        }
        public bool IsErrorConnection
        {
            get
            {
                return _isErrorConnection;
            }
            set
            {
                _isErrorConnection = value;
                RaisePropertyChanged("IsErrorConnection");
            }
        }

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
                IsErrorConnection = false;
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
                IsErrorConnection = false;
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
            IsChargement = true;
            _navigationService.NavigateTo("Register");
            IsChargement = false; 
        }
        private async void Login()
        {
            IsChargement = true;
            AccountsServices accountsServices = new AccountsServices();

            ResponseObject response = await accountsServices.LogIn(Email, Password);

            if (response.Success)
            {
                _navigationService.NavigateTo("Home"); 
            }
            else
            {
                IsErrorConnection = true;
            }
            IsChargement = false; 
        }
    }
}
