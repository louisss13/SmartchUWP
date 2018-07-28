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
    public class AddTournamentViewModel : SmartchViewModelBase, IListeMembreViewModel, IAddressForm, INavigable
    {
        
        private ObservableCollection<Club> _clubs = null;
        private ObservableCollection<User> _allMembers;
        private ObservableCollection<User> _selectedAllMembers = new ObservableCollection<User>();
        private ObservableCollection<User> _selectedMembersEntity = new ObservableCollection<User>();
        private bool _isAddressError = false;
        private bool _isAddressRequiredCity = false;
        private bool _isAddressRequiredNumber = false;
        private bool _isAddressRequiredStreet = false;
        private bool _isAddressRequiredZipCode = false;
        private bool _isTournamentError = false;
        private bool _isClubRequired = false;
        private bool _isNameTournamentRequired = false;

        private Tournament _tournament;
        private ObservableCollection<TournamentState> _tournamentStates = null;

        public RelayCommand CommandAddTournament { get; private set; }
        public RelayCommand CommandAddMember { get; private set; }
        public RelayCommand CommandDelMember { get; private set; }

        public Address Address { get { return Tournament.Address; } set { Tournament.Address = value; RaisePropertyChanged("Address"); } }
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

        public bool IsTournamentError
        {
            get
            {
                return _isTournamentError;
            }
            set
            {
                _isTournamentError = value;
                RaisePropertyChanged("IsTournamentError");
            }
        }
        public bool IsClubRequired
        {
            get
            {
                return _isClubRequired;
            }
            set
            {
                _isClubRequired = value;
                RaisePropertyChanged("IsClubRequired");
                IsTournamentError = true;
            }
        }
        public bool IsNameTournamentRequired
        {
            get
            {
                return _isNameTournamentRequired;
            }
            set
            {
                _isNameTournamentRequired = value;
                RaisePropertyChanged("IsNameTournamentRequired");
                IsTournamentError = true;
            }
        }

        public ObservableCollection<TournamentState> TournamentStates
        {
            get
            {
                return _tournamentStates;
            }
            set
            {
                _tournamentStates = value;
                RaisePropertyChanged("TournamentStates");
            }

        }
        public ObservableCollection<Club> Clubs
        {
            get
            {
                return _clubs;
            }
            set
            {
                if (_clubs == value)
                {
                    return;
                }
                _clubs = value;
                RaisePropertyChanged("Clubs");
            }
        }
        public Tournament Tournament
        {
            get
            {
                return _tournament;
            }
            set
            {
                if (_tournament == value)
                {
                    return;
                }
                _tournament = value;
                RaisePropertyChanged("Tournament");
            }
        }

        public ObservableCollection<User> MembersEntity
        {
            get
            {
                return new ObservableCollection<User>(Tournament.Participants);
            }
            set
            {
                Tournament.Participants = value;
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

        public AddTournamentViewModel(INavigationService navigationService) : base(navigationService)
        {
            
            if (IsInDesignMode)
            {
                _clubs = new ObservableCollection<Club> { new Club() { Name = "Club1" }, new Club() { Name = "club2" } };
            }
            else
            {
                CommandAddTournament = new RelayCommand(AddTournament);
                CommandAddMember = new RelayCommand(AddMembre, IsParameterAdd);
                CommandDelMember = new RelayCommand(DelMembre, IsParameterDel);
                
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

        public async Task SetMembers()
        {
            UsersServices usersServices = new UsersServices();
            try
            {
                List<User> users = await usersServices.GetUsers();
                AllMembers = new ObservableCollection<User>(users.Except(Tournament.Participants));
                MembersEntity = new ObservableCollection<User>(Tournament.Participants);
            }
            catch (Exception e)
            {
                SetGeneralErrorMessage(e);
            }
        }


        public async void AddTournament()
        {
            InitError();
           
            Tournament.Admins = null;
            //tournament.Club = null;

            TournamentsServices tournamentsServices = new TournamentsServices();
            try
            {
                bool response = await tournamentsServices.AddTournamentAsync(Tournament);
                if (response)
                {
                    _navigationService.NavigateTo("Tournaments");
                }
            }
            catch (BadRequestException e)
            {
                foreach (Error error in e.Errors.ToList() as List<Error>)
                {
                    SwitchError(error);
                }
            }
            catch (Exception e)
            {
                SetGeneralErrorMessage(e);
            }
           
        }

        public async Task InitializeAsync()
        {
            await SetClubList();
            SetStates();
            await SetMembers();
        }
        private void SetStates()
        {
            List<TournamentState> allTournamentState = new List<TournamentState>();
            foreach(TournamentState s in Enum.GetValues(typeof(TournamentState)))
            {
                allTournamentState.Add(s);
            }
            TournamentStates = new ObservableCollection<TournamentState>(allTournamentState);
        }
       


        private async Task SetClubList()
        {
            var service = new ClubsServices();
            try
            {
                List<Club> clubs = await service.GetClubs();
                Clubs = new ObservableCollection<Club>(clubs);
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
                case "AddressRequired":
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

                case "ClubRequired":
                    IsClubRequired = true;
                    break;
                case "NameTournamentRequired":
                    IsNameTournamentRequired = true;
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

            IsClubRequired = false;
            IsNameTournamentRequired = false;

            IsTournamentError = false;
            
        }

        public async void NavigatedTo(object parameter)
        {
            Tournament = new Tournament();
            await InitializeAsync();
        }

        public void NavigatedFrom(object parameter)
        {
            //throw new NotImplementedException();
        }
    }
}
   
