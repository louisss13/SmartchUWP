using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using Model.ModelException;
using smartchUWP.Interfaces;
using smartchUWP.Observable;
using smartchUWP.Services;
using smartchUWP.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class AddMembreViewModel : SmartchViewModelBase, IAddressForm, INavigable
    {
        public RelayCommand CommandAddMember { get; private set; }

        private User _user = new User();
        private bool _isAddressError = false;
        private bool _isAddressRequiredCity = false;
        private bool _isAddressRequiredNumber = false;
        private bool _isAddressRequiredStreet = false;
        private bool _isAddressRequiredZipCode = false;

        private bool _isErrorUser = false;
        private bool _isEmailRequired = false;
        private bool _isFirstNameRequired = false;
        private bool _isNameRequired = false;
        private bool _isEmailFormatError = false;
        private bool _isBirtdayRequired = false;

        public User User
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

        public Address Address { get { return User.Adresse; } set { User.Adresse = value; RaisePropertyChanged("Address"); } }
        public bool IsErrorAdresse
        {
            get
            {
                return _isAddressError;
            }
            set
            {
                _isAddressError = value;
                RaisePropertyChanged("IsErrorAdresse");
            }
        }
        public bool IsAddressRequiredCity
        {
            get
            {
                return _isAddressRequiredCity;
            }
            set
            {
                _isAddressRequiredCity = value;
                RaisePropertyChanged("IsAddressRequiredCity");
                IsErrorAdresse = true;
            }
        }
        public bool IsAddressRequiredNumber
        {
            get
            {
                return _isAddressRequiredNumber;
            }
            set
            {
                _isAddressRequiredNumber = value;
                RaisePropertyChanged("IsAddressRequiredNumber");
                IsErrorAdresse = true;
            }
        }
        public bool IsAddressRequiredStreet
        {
            get
            {
                return _isAddressRequiredStreet;
            }
            set
            {
                _isAddressRequiredStreet = value;
                RaisePropertyChanged("IsAddressRequiredStreet");
                IsErrorAdresse = true;
            }
        }
        public bool IsAddressRequiredZipCode
        {
            get
            {
                return _isAddressRequiredZipCode;
            }
            set
            {
                _isAddressRequiredZipCode = value;
                RaisePropertyChanged("IsAddressRequiredZipCode");
                IsErrorAdresse = true;
            }
        }

        public bool IsErrorUser
        {
            get
            {
                return _isErrorUser;
            }
            set
            {
                _isErrorUser = value;
                RaisePropertyChanged("IsErrorUser");
            }
        }
        public bool IsEmailRequired
        {
            get
            {
                return _isEmailRequired;
            }
            set
            {
                _isEmailRequired = value;
                RaisePropertyChanged("IsEmailRequired");
                IsErrorUser = true;
            }
        }
        public bool IsFirstNameRequired
        {
            get
            {
                return _isFirstNameRequired;
            }
            set
            {
                _isFirstNameRequired = value;
                RaisePropertyChanged("IsFirstNameRequired");
                IsErrorUser = true;
            }
        }
        public bool IsNameRequired
        {
            get
            {
                return _isNameRequired;
            }
            set
            {
                _isNameRequired = value;
                RaisePropertyChanged("IsNameRequired");
                IsErrorUser = true;
            }
        }
        public bool IsEmailFormatError
        {
            get
            {
                return _isEmailFormatError;
            }
            set
            {
                _isEmailFormatError = value;
                RaisePropertyChanged("IsEmailFormatError");
                IsErrorUser = true;
            }
        }
        public bool IsBirtdayRequired
        {
            get
            {
                return _isBirtdayRequired;
            }
            set
            {
                _isBirtdayRequired = value;
                RaisePropertyChanged("IsBirtdayRequired");
                IsErrorUser = true;
            }
        }



        public AddMembreViewModel(INavigationService navigationService) : base(navigationService)
        {
            CommandAddMember = new RelayCommand(AddMembre);
        }
        
        
        private async void AddMembre()
        {
            InitError();
            UsersServices usersServices = new UsersServices();
            try
            {
                bool response = await usersServices.AddUser(User);
                if (response)
                {
                    _navigationService.NavigateTo("Membres");
                }
            }
            catch(BadRequestException e)
            {
                List<Error> errors = e.Errors.ToList();
                foreach (Error error in errors)
                {
                    SwitchError(error);
                }
            }
            catch(Exception e)
            {
                SetGeneralErrorMessage(e);
            }
        }
        public void SwitchError(Error error)
        {
            
            switch (error.Code)
            {
                case "ErrorAdresse":
                    IsErrorAdresse = true;
                    break;
                case "AddressRequiredCity":
                    IsAddressRequiredCity = true;
                    break;
                case "AddressRequiredNumber":
                    IsAddressRequiredNumber = true;
                    break;
                case "AddressRequiredStreet":
                    IsAddressRequiredStreet = true;
                    break;
                case "AddressRequiredZipCode":
                    IsAddressRequiredZipCode = true;
                    break;

                case "EmailFormatError":
                    IsEmailFormatError = true;
                    break;
                case "BirtdayRequired":
                    IsBirtdayRequired = true;
                    break;
                case "EmailRequired":
                    IsEmailRequired = true;
                    break;
                case "NameRequired":
                    IsNameRequired = true;
                    break;
                case "FirstNameRequired":
                    IsFirstNameRequired = true;
                    break;
            }
        }
        private void InitError()
        {
            IsAddressRequiredCity = false;
            IsAddressRequiredNumber = false;
            IsAddressRequiredStreet = false;
            IsAddressRequiredZipCode = false;

            IsErrorAdresse = false;


            IsEmailRequired = false;
            IsEmailFormatError = false;
            IsNameRequired = false;
            IsFirstNameRequired = false;
            IsBirtdayRequired = false;
            IsErrorUser = false;
        }

        public void NavigatedTo(object parameter)
        {
            InitError();
            User = new User();
        }

        public void NavigatedFrom(object parameter)
        {
            
        }
    }
}
