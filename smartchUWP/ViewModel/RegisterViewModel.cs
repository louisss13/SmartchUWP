using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using Model.ModelException;
using Newtonsoft.Json.Linq;
using smartchUWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class RegisterViewModel: SmartchViewModelBase, IAfficheErrorGeneral
    {
        private bool _isErrorConnexion = false;
        private bool _isPasswordTooShort = false;
        private bool _isPasswordRequiresNonAlphanumeric = false;
        private bool _isPasswordRequiresDigit = false;
        private bool _isPasswordRequiresLower = false;
        private bool _isPasswordRequiresUpper = false;
        private bool _isPasswordRequiresUniqueChars = false;
        private bool _isPasswordConfirmEquals = false;
        private bool _isEmailNotValid = false;
        private bool _isDuplicateUserName = false;
        private bool _isGeneralError = false;
        private String _errorDescription = "";

        private String _email = "";
        private String _password = "";
        private String _passwordConfirm = "";


        public RelayCommand CommandSaveNewAccount { get; private set; }
        public RelayCommand CommandClear { get; private set; }

        public bool IsErrorConnexion
        {
            get
            {
                return _isErrorConnexion;
            }
            set
            {
                _isErrorConnexion = value;
                RaisePropertyChanged("IsErrorConnexion");
            }
        }
        public bool IsPasswordTooShort
        {
            get
            {
                return _isPasswordTooShort;
            }
            set
            {
                _isPasswordTooShort = value;
                RaisePropertyChanged("IsPasswordTooShort");
                IsErrorConnexion = true;
            }
        }
        public bool IsPasswordRequiresNonAlphanumeric
        {
            get
            {
                return _isPasswordRequiresNonAlphanumeric;
            }
            set
            {
                _isPasswordRequiresNonAlphanumeric = value;
                RaisePropertyChanged("IsPasswordRequiresNonAlphanumeric");
                IsErrorConnexion = true;
            }
        }
        public bool IsPasswordRequiresDigit
        {
            get
            {
                return _isPasswordRequiresDigit;
            }
            set
            {
                _isPasswordRequiresDigit = value;
                RaisePropertyChanged("IsPasswordRequiresDigit");
                IsErrorConnexion = true;
            }
        }
        public bool IsPasswordRequiresLower
        {
            get
            {
                return _isPasswordRequiresLower;
            }
            set
            {
                _isPasswordRequiresLower = value;
                RaisePropertyChanged("IsPasswordRequiresLower");
                IsErrorConnexion = true;
            }
        }
        public bool IsPasswordRequiresUpper
        {
            get
            {
                return _isPasswordRequiresUpper;
            }
            set
            {
                _isPasswordRequiresUpper = value;
                RaisePropertyChanged("IsPasswordRequiresUpper");
                IsErrorConnexion = true;
            }
        }
        public bool IsPasswordRequiresUniqueChars
        {
            get
            {
                return _isPasswordRequiresUniqueChars;
            }
            set
            {
                _isPasswordRequiresUniqueChars = value;
                RaisePropertyChanged("IsPasswordRequiresUniqueChars");
                IsErrorConnexion = true;
            }
        }
        public bool IsPasswordConfirmEquals
        {
            get
            {
                return _isPasswordConfirmEquals;
            }
            set
            {
                _isPasswordConfirmEquals = value;
                RaisePropertyChanged("IsPasswordConfirmEquals");
                IsErrorConnexion = true;
            }
        }
        public bool IsEmailNotValid
        {
            get
            {
                return _isEmailNotValid;
            }
            set
            {
                _isEmailNotValid = value;
                RaisePropertyChanged("IsEmailNotValid");
                IsErrorConnexion = true;
            }
        }
        public bool IsDuplicateUserName
        {
            get
            {
                return _isDuplicateUserName;
            }
            set
            {
                _isDuplicateUserName = value;
                RaisePropertyChanged("IsDuplicateUserName");
                IsErrorConnexion = true;
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
        public String Email
        {
            get
            {
                return _email;
            }
            set
            {
                ReinitError();
                _email = value;
                IsDuplicateUserName = false;
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
                ReinitError();
                _password = value;
                RaisePropertyChanged("Password");
            }
        }
        public String PasswordConfirm
        {
            get
            {
                return _passwordConfirm;
            }
            set
            {
                ReinitError();
                _passwordConfirm = value;
                RaisePropertyChanged("PasswordConfirm");
            }
        }


        public RegisterViewModel(INavigationService navigationService):base(navigationService)
        {
            
            CommandSaveNewAccount = new RelayCommand(SaveNewAccount);
            CommandClear = new RelayCommand(ClearField);
        }

        public async void SaveNewAccount()
        {
            ReinitError();
            if (!Password.Equals(PasswordConfirm))
            {
                IsPasswordConfirmEquals = true;
            }
            if (!IsValidMail(Email))
            {
                IsEmailNotValid = true;
            }
            IsCorrectPassword(Password);
            if (!IsErrorConnexion)
            {
                AccountsServices accountsServices = new AccountsServices();
                Account newUser = new Account() { Password = Password, Email = Email };
                try
                {
                    bool isAdded = await accountsServices.AddUser(newUser);
                    if (isAdded)
                    {
                        _navigationService.NavigateTo("Login");
                    }
                }
                catch (BadRequestException e) {
                    foreach (Error error in (e.Errors as IEnumerable<Error>))
                    {
                        ErrorSwitch(error.Code);
                    }
                }
                catch (Exception e) {
                    SetGeneralErrorMessage(e, this);
                }
                
                
            }
            
        }
        private void ErrorSwitch(String error)
        {
            switch (error)
            {
                case "PasswordTooShort":
                    IsPasswordTooShort = true;
                    break;

                case "PasswordRequiresNonAlphanumeric":
                    IsPasswordRequiresNonAlphanumeric = true;
                    break;
                case "PasswordRequiresDigit":
                    IsPasswordRequiresDigit = true;
                    break;
                case "PasswordRequiresLower":
                    IsPasswordRequiresLower = true;
                    break;
                case "PasswordRequiresUpper":
                    IsPasswordRequiresUpper = true;
                    break;
                case "PasswordRequiresUniqueChars":
                    IsPasswordRequiresUniqueChars = true;
                    break;
                case "DuplicateUserName":
                    IsDuplicateUserName = true;
                    break;
            }
        }
        public void ClearField()
        {
            Email = "";
            Password = "";
            PasswordConfirm = "";
        }
        private void ReinitError()
        {
            
            IsPasswordTooShort = false;
            IsPasswordRequiresNonAlphanumeric = false;
            IsPasswordRequiresDigit = false;
            IsPasswordRequiresLower = false;
            IsPasswordRequiresUpper = false;
            IsPasswordRequiresUniqueChars = false;
            IsPasswordConfirmEquals = false;
            IsEmailNotValid = false;
            IsErrorConnexion = false;

        }

        private Boolean IsCorrectPassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpper = new Regex(@"[A-Z]+");
            var hasMin = new Regex(@".{6,}");
            var hasLower = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");


            if (!hasNumber.IsMatch(password))
            {
                IsPasswordRequiresDigit = true;
            }
            if (!hasUpper.IsMatch(password))
            {
                IsPasswordRequiresUpper = true;
            }
            if (!hasMin.IsMatch(password))
            {
                IsPasswordTooShort = true;
            }
            if (!hasLower.IsMatch(password))
            {
                IsPasswordRequiresLower = true;
            }
            if (!hasSymbols.IsMatch(password))
            {
                IsPasswordRequiresNonAlphanumeric = true;
            }
            return !IsErrorConnexion;
        }

        private Boolean IsValidMail(String email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
