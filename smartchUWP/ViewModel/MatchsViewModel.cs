using DataAccess;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using smartchUWP.Services;
using smartchUWP.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class MatchsViewModel : SmartchViewModelBase, INavigable
    {
       
        
        private Tournament _selectedTournament = null;
        private MatchsPhase _selectedPhase;
        private Match _selectedMatch;
        private ObservableCollection<MatchsPhase> _matchPhases = new ObservableCollection<MatchsPhase>(new List<MatchsPhase>());


        public RelayCommand CommandNavigateAddMatch { get; private set; }
        public RelayCommand AddPivot { get; private set; }
        public RelayCommand CommandGenereMatch { get; private set; }
        public RelayCommand CommandEditMatch { get; private set; }
        public RelayCommand<Match> CommandDeleteMatch { get; private set; }
        public RelayCommand CommandEnregistrerTournament { get; private set; }

        public Tournament SelectedTournament
        {
            get
            {
                return _selectedTournament;
            }
            set
            {
                _selectedTournament = value;
                RaisePropertyChanged("SelectedTournament");
                RaisePropertyChanged("MatchsPhases");
            }
        }
        public MatchsPhase SelectedPhase
        {
            get
            {
                return _selectedPhase;
            }
            set
            {
                _selectedPhase = value;
                CommandGenereMatch.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedPhase");
            }
        }
        public Match SelectedMatch
        {
            get
            {
                return _selectedMatch;
            }
            set
            {
                _selectedMatch = value;
                CommandEditMatch.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedMatch");
            }
        }
        public ObservableCollection<MatchsPhase> MatchsPhases
        {
            get
            {
                if(_matchPhases.Count <= 0)
                {
                    if (SelectedTournament != null && SelectedTournament.Matches != null)
                        _matchPhases = new ObservableCollection<MatchsPhase>(SelectedTournament.Matches);
                    
                    
                }
                
                return _matchPhases;  
            }
            set
            {
                _matchPhases = value;
                SelectedTournament.Matches = value;
                RaisePropertyChanged("MatchsPhases");
            }
        }

        public MatchsViewModel(INavigationService navigationService) : base(navigationService)
        {
            CommandNavigateAddMatch = new RelayCommand(NavigateToAddMatch);
            CommandGenereMatch = new RelayCommand(GenereMatches, IsSelectedPhase);
            CommandEditMatch = new RelayCommand(NavigateToEditMatch, IsSelectedMatch);
            CommandEnregistrerTournament = new RelayCommand(RegisterTournamentAsync);
            CommandDeleteMatch = new RelayCommand<Match>(DeleteMatchAsync);
            AddPivot = new RelayCommand(AddPivotItem);
        }

        private void GenereMatches()
        {
            GenereFirstPhase();
        }
        private void GenereFirstPhase()
        {
            int selectedNumPhase = 1;
            if(SelectedPhase != null)
            {
                selectedNumPhase = SelectedPhase.NumPhase;
            }
            List<User> users = new List<User>(SelectedTournament.Participants);
            List<Match> matchs = new List<Match>();
            Random rand = new Random();
            while (users.Count > 1 ) {
                int toSkip = rand.Next(0, users.Count);
                User joueur1 = users.Skip(toSkip).Take(1).First();
                users.Remove(joueur1);
                toSkip = rand.Next(0, users.Count);
                User joueur2 = users.Skip(toSkip).Take(1).First();
                users.Remove(joueur2);
                Match match = new Match()
                {
                    Player1 = joueur1,
                    Player2 = joueur2,
                    State = EMatchState.EnPreparation
                };
                matchs.Add(match);
            }
            if (MatchsPhases.Count == 0)
            {
                MatchsPhases.Add(new MatchsPhase()
                {
                    NumPhase = selectedNumPhase,
                    Matchs = new List<Match>()
                });
            }

            MatchsPhase selectedPhase = SelectedTournament.Matches.Where(m => m.NumPhase == selectedNumPhase).DefaultIfEmpty(null).First();
            if(selectedPhase == null)
            {
                selectedPhase = new MatchsPhase() { NumPhase = selectedNumPhase };
            }
            selectedPhase.Matchs = matchs;
            SelectedPhase = selectedPhase;
            MatchsPhases = new ObservableCollection<MatchsPhase>(SelectedTournament.Matches);
            
           
            
        }
        private bool IsSelectedPhase()
        {
            return SelectedPhase != null;
        }
        private bool IsSelectedMatch()
        {
            return SelectedMatch != null;
        }

        private void NavigateToEditMatch()
        {
            _navigationService.NavigateTo("AddMatch", new List<Object>() { SelectedTournament, SelectedPhase.NumPhase, SelectedMatch });
        }
        private void NavigateToAddMatch()
        {
            _navigationService.NavigateTo("AddMatch", new List<Object>() { SelectedTournament, SelectedPhase.NumPhase });
        }

        public void AddPivotItem()
        {

                
            MatchsPhases.Add(new MatchsPhase() { NumPhase = SelectedTournament.Matches.Count+1 });
            RaisePropertyChanged("MatchsPhases");

        }

        private async void RegisterTournamentAsync()
        {
            TournamentsServices tournamentsServices = new TournamentsServices();
            try
            {
                if(await tournamentsServices.UpdateAsync(SelectedTournament))
                {
                    await InitTournament();
                }
            }
            catch(Exception e)
            {
                SetGeneralErrorMessage(e);
            }
        }
        private async void DeleteMatchAsync(Match m)
        {
            TournamentsServices tournamentsServices = new TournamentsServices();
            try
            {
                if(await tournamentsServices.DeletaMatchAsync(SelectedTournament.Id, m))
                {
                    await InitTournament();
                }
            }
            catch(Exception e)
            {
                SetGeneralErrorMessage(e);
            }
        }

       
        public async Task InitTournament()
        {
            
            TournamentsServices tournamentsServices = new TournamentsServices();
            try
            {
                SelectedTournament = await tournamentsServices.GetTournament(SelectedTournament.Id);
                if (SelectedTournament.Matches == null || SelectedTournament.Matches.Count() <= 0)
                {
                    SelectedTournament.Matches = new List<MatchsPhase>()
                            {
                                new MatchsPhase(){
                                    NumPhase = 1,
                                    Matchs = new List<Match>()
                                }
                            };

                }
                SelectedPhase = MatchsPhases.Where(m => m.NumPhase == 1).DefaultIfEmpty(null).First();
                MatchsPhases = new ObservableCollection<MatchsPhase>(SelectedTournament.Matches);
            }
            catch (Exception e)
            {
                SetGeneralErrorMessage(e);
            }
        }

        public async void  NavigatedTo(object parameter)
        {
            if (parameter is Tournament tournament)
            {
                SelectedTournament = tournament;
                await InitTournament();
            }
            else
            {
                SetGeneralErrorMessage(new Exception("Tournois non trouvé"));
            }

        }

        public void NavigatedFrom(object parameter)
        {
            //nothing to do
        }
    }
}
