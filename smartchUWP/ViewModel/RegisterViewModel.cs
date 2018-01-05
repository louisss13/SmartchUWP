using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class RegisterViewModel: ViewModelBase
    {
        private INavigationService _navigationService;
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


        public RegisterViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
                ResponseObject AddedUser = await accountsServices.AddUser(newUser);
                if (AddedUser.Success)
                {
                    _navigationService.NavigateTo("Login");
                }
                else if(AddedUser.Content is IEnumerable<Error>)
                {
                    foreach(Error error in (AddedUser.Content as IEnumerable<Error>))
                    {
                        ErrorSwitch(error.Code);
                    }
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
