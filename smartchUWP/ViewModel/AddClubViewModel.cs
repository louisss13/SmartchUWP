using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using Model.ModelException;
using smartchUWP.Interfaces;
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
    public class AddClubViewModel : SmartchViewModelBase, IListeMembreViewModel, IAddressForm
    {
       
        private ObservableCollection<User> _allMembers;
        private ObservableCollection<User> _selectedAllMembers = new ObservableCollection<User>();
        private ObservableCollection<User> _selectedMembersEntity = new ObservableCollection<User>();
        private bool _isAddressError = false;
        private bool _isAddressRequiredCity = false;
        private bool _isAddressRequiredNumber = false;
        private bool _isAddressRequiredStreet = false;
        private bool _isAddressRequiredZipCode = false;
        private bool _isAddClubError = false;
        private bool _isIncorrectMailAddress = false;
        private bool _isNullMailAddress = false;
        private bool _isNameRequired = false;
        private bool _isPhoneRequired = false;
        

        public RelayCommand CommandAddClub { get; private set; }
        public RelayCommand CommandAddMember { get; private set; }
        public RelayCommand CommandDelMember { get; private set; }
        

        public ObservableClub _club = new ObservableClub();
        public ObservableClub Club
        {
            get
            {
                return _club;
            }
            set
            {
                _club = value;
                RaisePropertyChanged("Club");
            }
        }
        public ObservableCollection<User> MembersEntity
        {
            get
            {
                return new ObservableCollection<User>(Club.Members);
            }
            set
            {
                Club.Members = value;
                RaisePropertyChanged("MembersEntity");
            }
        }
        public ObservableCollection<User> AllMembers
        {
            get
            {
                return _allMembers;
            }
            set
            {
                _allMembers = value;
                RaisePropertyChanged("AllMembers");
            }
        }
        public ObservableCollection<User> SelectedMembersEntity
        {
            get
            {
                return _selectedMembersEntity;
            }
            set
            {
                _selectedMembersEntity = value;
                CommandDelMember.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedMembersEntity");
            }
        }
        public ObservableCollection<User> SelectedAllMembers
        {
            get
            {
                return _selectedAllMembers;
            }
            set
            {
                _selectedAllMembers = value;
                CommandAddMember.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedAllMembers");
            }
        }

        public Address Address { get { return Club.Adresse; } set { Club.Adresse = value; RaisePropertyChanged("Address"); } }
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

        public bool IsAddClubError
        {
            get
            {
                return _isAddClubError;
            }
            set
            {
                _isAddClubError = value;
                RaisePropertyChanged("IsAddClubError");
            }
        }
        public bool IsIncorrectMailAddress
        {
            get
            {
                return _isIncorrectMailAddress;
            }
            set
            {
                _isIncorrectMailAddress = value;
                RaisePropertyChanged("IsIncorrectMailAddress");
                IsAddClubError = true;
            }
        }
        public bool IsNullMailAddress
        {
            get
            {
                return _isNullMailAddress;
            }
            set
            {
                _isNullMailAddress = value;
                RaisePropertyChanged("IsNullMailAddress");
                IsAddClubError = true;
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
                IsAddClubError = true;
            }
        }
        public bool IsPhoneRequired
        {
            get
            {
                return _isPhoneRequired;
            }
            set
            {
                _isPhoneRequired = value;
                RaisePropertyChanged("IsPhoneRequired");
                IsAddClubError = true;
            }
        }


        public AddClubViewModel(INavigationService navigationService):base(navigationService)
        {
            
            CommandAddClub = new RelayCommand(AddClub);
            CommandAddMember = new RelayCommand(AddMembre, IsParameterAdd);
            CommandDelMember = new RelayCommand(DelMembre, IsParameterDel);
            
            SetMembers();
        }

        public async void AddClub()
        {
            InitError();
            ClubsServices clubsServices = new ClubsServices();
            try
            {
                bool response = await clubsServices.AddClub(Club);
                if (response)
                {
                    MessengerInstance.Send(new NotificationMessage(NotificationMessageType.ListClub));
                    _navigationService.NavigateTo("Clubs");
                }
            }
            catch (BadRequestException e)
            {
                List<Error> errors = e.Errors.ToList();
                GereError(errors);
            }
            
            catch (Exception e)
            {
                SetGeneralErrorMessage(e);
            }

        }

        public bool IsParameterAdd()
        {
            return SelectedAllMembers.Count > 0;
        }
        public bool IsParameterDel()
        {
            return SelectedMembersEntity.Count > 0;
        }

        public void AddMembre()
        {
            
            MembersEntity = new ObservableCollection<User>(MembersEntity.Concat(SelectedAllMembers));
            AllMembers = new ObservableCollection<User>(AllMembers.Except(SelectedAllMembers));
            
        }
        public void DelMembre()
        {
            AllMembers = new ObservableCollection<User>(AllMembers.Concat(SelectedMembersEntity));
            MembersEntity = new ObservableCollection<User>(MembersEntity.Except(SelectedMembersEntity));
        }

        public async void SetMembers()
        {
            UsersServices usersServices = new UsersServices();
            try
            {
                List<User> users = await usersServices.GetUsers();
                AllMembers = new ObservableCollection<User>(users.Except(Club.Members));
                MembersEntity = new ObservableCollection<User>(Club.Members);
            }
            catch (Exception e)
            {
                SetGeneralErrorMessage(e);
            }
           
            
        }

        public void GereError(List<Error> errors)
        {
            foreach(Error error in errors)
            {
                SwitchError(error);
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

                case "IncorrectMailAddress":
                    IsIncorrectMailAddress = true;
                    break;
                case "NullMailAddress":
                    IsNullMailAddress = true;
                    break;
                case "NameRequired":
                    IsNameRequired = true;
                    break;
                case "PhoneRequired":
                    IsPhoneRequired = true;
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

            IsIncorrectMailAddress = false;
            IsNullMailAddress = false;
            IsNameRequired = false;
            IsPhoneRequired = false;
            IsAddClubError = false;
        }

        
    }
}
