using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using Model.ModelException;
using smartchUWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class LoginViewModel : SmartchViewModelBase, IAfficheErrorGeneral
    {
        
        private bool _isChargement = false;
        private bool _isErrorConnection = false;
        private bool _isGeneralError = false;

        private String _email = "louisss13@gmail.com";
        private String _password = "Coucou-123";
        private String _errorDescription = "";

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
        public bool IsGeneralError
        {
            get
            {
                return _isGeneralError;
            }
            set
            {
                _isGeneralError = value;
                RaisePropertyChanged("IsGeneralError");
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
        public String ErrorDescription
        {
            get
            {
                return _errorDescription;
            }
            set
            {
                _errorDescription = value + "234567890";
                RaisePropertyChanged("ErrorDescription");
            }
        }


        public LoginViewModel(INavigationService navigationService):base(navigationService)
        {
            CommandLogin = new RelayCommand(Login);
            CommandNavigateRegister = new RelayCommand(NavigateToRegister);
        }
        public void SetMessageConnection()
        {
            IsGeneralError = true;
            ErrorDescription = "Un problème est survenu lors de l'envois d'une requète. Etes-vous connecté à internet?";
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
            try
            {
                bool response = await accountsServices.LogIn(Email, Password);
                if (response)
                {
                    _navigationService.NavigateTo("Home");
                }
            }
            catch (Exception e)
            {
                SetGeneralErrorMessage(e, this);
            }
            IsChargement = false; 
        }
    }
}
