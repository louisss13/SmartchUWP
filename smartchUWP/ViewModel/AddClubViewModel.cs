using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
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
    public class AddClubViewModel : MainPageViewModel, IListeMembreViewModel
    {
       
        private ObservableCollection<User> _allMembers;
        private ObservableCollection<User> _selectedAllMembers = new ObservableCollection<User>();
        private ObservableCollection<User> _selectedMembersEntity = new ObservableCollection<User>();

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

        public AddClubViewModel(INavigationService navigationService):base(navigationService)
        {
            
            CommandAddClub = new RelayCommand(AddClub);
            CommandAddMember = new RelayCommand(AddMembre, IsParameterAdd);
            CommandDelMember = new RelayCommand(DelMembre, IsParameterDel);
            
            SetMembers();
        }

        public async void AddClub()
        {
            ClubsServices clubsServices = new ClubsServices();
            ResponseObject response = await clubsServices.AddClub(Club);

            
            if (response.Success)
            {
                MessengerInstance.Send(new NotificationMessage(NotificationMessageType.ListClub));
                _navigationService.NavigateTo("Clubs");
            }
            else
            { }
              

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
            var response = await usersServices.GetUsers();
            if (response.Success) {
                List<User> users = ((List<Object>)response.Content).Cast<User>().ToList();
                AllMembers = new ObservableCollection<User>(users.Except(Club.Members));
                MembersEntity = new ObservableCollection<User>(Club.Members);
            }
        }

        
    }
}
