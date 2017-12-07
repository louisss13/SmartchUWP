using DataAccess;
using GalaSoft.MvvmLight;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class AddTournamentViewModel : ViewModelBase
    {
        private ObservableCollection<Club> _clubs = null;
        private ObservableCollection<KeyValuePair<TournamentState, string>> _tournamentStates = null;
        
        public AddTournamentViewModel()
        {
            if (IsInDesignMode)
            {
                _clubs = new ObservableCollection<Club> { new Club() { Name = "Club1" }, new Club() { Name = "club2" } };
            }
            else
            {
                InitializeAsync();
            }
        }
        public ObservableCollection<KeyValuePair<TournamentState, string>> TournamentStates
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
        public async Task InitializeAsync()
        {
            var service = new ClubsServices();
            var clubs = await service.GetClubs();
            Clubs = new ObservableCollection<Club>(clubs);
            TournamentStates = getTournamentState();

        }
        private ObservableCollection<KeyValuePair<TournamentState, string>> getTournamentState()
        {
            var stateDictionary = new ObservableCollection<KeyValuePair<TournamentState, string>>() ;
            foreach (TournamentState etat in Enum.GetValues(typeof(TournamentState)))
            {
                KeyValuePair<TournamentState, string> cleValeur;
                
                switch (etat)
                {
                    case TournamentState.EnCours:
                        cleValeur = new KeyValuePair<TournamentState, string>(etat, "En cours");
                        break;
                    case TournamentState.EnPreparation:
                        cleValeur = new KeyValuePair<TournamentState, string>(etat, "En Préparation"); 
                        break;
                    case TournamentState.Fini:
                        cleValeur = new KeyValuePair<TournamentState, string>(etat, "Fini");
                        break;
                    default:
                        cleValeur = new KeyValuePair<TournamentState, string>(etat, "Valeur non traduite");
                        break;

                }
                stateDictionary.Add(cleValeur);
                
            }
            
            return stateDictionary;
        }
    }
}
   
