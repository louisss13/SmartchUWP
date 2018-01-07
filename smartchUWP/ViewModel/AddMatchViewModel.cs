using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using smartchUWP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class AddMatchViewModel : MainPageViewModel
    {
        private Match _match;
        private User _selectedJoueur1;
        private User _selectedJoueur2;
        private User _selectedArbitre;
        private TimeSpan _heurePrevue = new TimeSpan(12, 0, 0);
        private String _lieuMatch;
        private ObservableCollection<User> _allUsers = new ObservableCollection<User>();
        private ObservableCollection<User> _joueur1Liste = new ObservableCollection<User>();
        private ObservableCollection<User> _joueur2Liste = new ObservableCollection<User>();
        private ObservableCollection<User> _listeArbitre = new ObservableCollection<User>();

        public RelayCommand CommandAjouterMatch { get; private set; }
        public RelayCommand CommandAddPoint { get; private set; }
        public RelayCommand CommandDelPoint { get; private set; }

        public Match Match
        {
            get
            {
                return _match;
            }
            set
            {
                _match = value;
                if(value != null)
                {
                    SelectedJoueur1 = _match.Player1;
                    SelectedJoueur2 = _match.Player2;
                    SelectedArbitre = _match.Arbitre;

                }
                RaisePropertyChanged("Match");
                CommandAddPoint.RaiseCanExecuteChanged();
            }
        }
        public User SelectedJoueur1
        {
            get
            {
                return _selectedJoueur1;
            }
            set
            {
                _selectedJoueur1 = value;
                RaisePropertyChanged("SelectedJoueur1");
                CommandAjouterMatch.RaiseCanExecuteChanged();
            }
        }
        public User SelectedJoueur2
        {
            get
            {
                return _selectedJoueur2;
            }
            set
            {
                _selectedJoueur2 = value;
                RaisePropertyChanged("SelectedJoueur2");
                CommandAjouterMatch.RaiseCanExecuteChanged();
            }
        }
        public User SelectedArbitre
        {
            get
            {
                return _selectedArbitre;
            }
            set
            {
                _selectedArbitre = value;
                RaisePropertyChanged("SelectedArbitre");
                CommandAjouterMatch.RaiseCanExecuteChanged();
            }
        }
        public TimeSpan HeurePrevue
        {
            get
            {
                return _heurePrevue;
            }
            set
            {
                _heurePrevue = value;
                RaisePropertyChanged("HeurePrevue");
            }
        }
        public String LieuMatch
        {
            get
            {
                return _lieuMatch;
            }
            set
            {
                _lieuMatch = value;
                RaisePropertyChanged("LieuMatch");
                CommandAjouterMatch.RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<User> AllUsers
        {
            get
            {
                return _allUsers;
            }
            set
            {
                _allUsers = value;
                ListeJoueur1 = value;
                ListeJoueur2 = value;
                RaisePropertyChanged("AllUsers");
            }
        }
        public ObservableCollection<User> ListeJoueur1
        {
            get
            {
                return _joueur1Liste;
            }
            set
            {
                _joueur1Liste = value;
                RaisePropertyChanged("ListeJoueur1");
                
            }
        }
        public ObservableCollection<User> ListeJoueur2
        {
            get
            {
                return _joueur2Liste;
            }
            set
            {
                _joueur2Liste = value;
                RaisePropertyChanged("ListeJoueur2");
            }
        }
        public ObservableCollection<User> AllArbitre
        {
            get
            {
                return _listeArbitre;
            }
            set
            {
                _listeArbitre = value;
                RaisePropertyChanged("AllArbitre");
            }
        }
        public Tournament Tournament { get; set; }
        public int? NumPhase { get; set; }
        

        public AddMatchViewModel(INavigationService navigationService) : base(navigationService)
        {
            CommandAjouterMatch = new RelayCommand(EnregistrerMatch, CanEnregistrerMatch);
            CommandAddPoint = new RelayCommand(AddPoint, CanAddPoint);
            CommandDelPoint = new RelayCommand(DelPoint, CanAddPoint);
            InitializeAsync();
            
        }
        private async void AddPoint()
        {
            TournamentsServices tournamentsServices = new TournamentsServices();
            Point point = new Point()
            {
                Joueur = EJoueurs.Joueur1
            };
            await tournamentsServices.AddPointMatch(Match.Id, point);
        }
        private async void DelPoint()
        {
            TournamentsServices tournamentsServices = new TournamentsServices();
            Point point = new Point()
            {
                Joueur = EJoueurs.Joueur1
            };
            await tournamentsServices.DelPointMatch(Match.Id, point);
        }
        private Boolean CanAddPoint()
        {
            if(Match != null && Match.Id > 0)
            {
                return true;
            }
            return false;
        }
        private async void EnregistrerMatch()
        {
            TournamentsServices tournamentsServices = new TournamentsServices();
            Match.Arbitre = SelectedArbitre;
            Match.Emplacement = LieuMatch;
            Match.Player1 = SelectedJoueur1;
            Match.Player2 = SelectedJoueur2;
            Match.Time = HeurePrevue;
            
            
            if(Match.Id > 0)
            {
                bool isUpdate = await tournamentsServices.UpdateMatch(Tournament, Match, NumPhase.GetValueOrDefault());
            }
            else
            {
                ResponseObject response = await tournamentsServices.AddMatch(Tournament, Match, NumPhase.GetValueOrDefault());
                if (response.Success)
                {
                    _navigationService.NavigateTo("Tournaments");
                }
            }
            
        }
        private Boolean CanEnregistrerMatch()
        {
            if( SelectedArbitre == null || SelectedJoueur1 == null ||
                SelectedJoueur2 == null || LieuMatch == null || LieuMatch.Length <=0)
            {
                return false;
            }
            return true;
        }
        private void MessageReceiver(NotificationMessage message)
        {
            switch (message.VariableType)
            {
                case NotificationMessageType.AddTournament:
                    if(message.Variable is List<Object>)
                    {
                        List<Object> variables = message.Variable as List<Object>;
                        Tournament = variables[0] as Tournament;
                        NumPhase = variables[1] as int?;
                        if (variables.Count() > 2)
                            Match = variables[2] as Match;
                        else
                            Match = new Match();
                    }
                    else
                        Tournament = message.Variable as Tournament;
                    ChargeVar();
                break;
            }
        }

        public void InitializeAsync()
        {
            AllArbitre = new ObservableCollection<User>(new List<User>());
            AllUsers = new ObservableCollection<User>(new List<User>());
            
            MessengerInstance.Register<NotificationMessage>(this, MessageReceiver);
        }
        public async void  ChargeVar()
        {
            UsersServices usersServices = new UsersServices();
            TournamentsServices tournamentsServices = new TournamentsServices();
            long idTounrnament = Tournament.Id;
            var responseUser = await tournamentsServices.GetParticipants(idTounrnament);
            var responseArbitre = await usersServices.GetUsersWithAccount();
            bool allRequestSuccess = true;
            if (responseUser.Success)
            {
                List<User> users = ((List<Object>)responseUser.Content).Cast<User>().ToList();
                AllUsers = new ObservableCollection<User>(users);
            }
            else
                allRequestSuccess = false;
            if (responseArbitre.Success)
            {
                List<User> arbitres = ((List<object>)responseArbitre.Content).Cast<User>().ToList();
                AllArbitre = new ObservableCollection<User>(arbitres);
            }
            else
                allRequestSuccess = false;



            if (allRequestSuccess) { 
                //if(Match.Arbitre != null)
                //    SelectedArbitre = AllArbitre.Where(a => a.Id == Match.Arbitre.Id);
                if (Match.Player1 != null)
                {
                    SelectedJoueur1 = AllUsers.Where(u => u.Id == Match.Player1.Id).First();
                }
                if (Match.Player2 != null)
                {
                    SelectedJoueur2 = AllUsers.Where(u => u.Id == Match.Player2.Id).First();
                }
                HeurePrevue = Match.Time;
                LieuMatch = Match.Emplacement;
            }
        }

    }
}
