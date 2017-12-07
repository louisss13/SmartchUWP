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
    public class TournamentViewModel : ViewModelBase
    {
        private ObservableCollection<Tournament> _tournaments = null;
        public TournamentViewModel()
        {
            if (IsInDesignMode)
            {
                _tournaments = new ObservableCollection<Tournament> { new Tournament() { Name = "Tournois1" }, new Tournament() { Name = "Tounrnois2" } };
            }
            else
            {
                InitializeAsync();
            }
        }
        public ObservableCollection<Tournament> Tournaments
        {
            get
            {
                return _tournaments;
            }
            set
            {
                if (_tournaments == value)
                {
                    return;
                }
                _tournaments = value;
                RaisePropertyChanged("Tournaments");
            }
        }
        public async Task InitializeAsync()
        {
            var service = new TournamentsServices();
            var tournament = await service.GetTournaments();
            Tournaments = new ObservableCollection<Tournament>(tournament);

        }
    }
}
