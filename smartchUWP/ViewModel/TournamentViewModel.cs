using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

using Model;
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
    public class TournamentViewModel : SmartchViewModelBase, INavigable
    {
        
        private ObservableCollection<Tournament> _tournaments = null;
        private Tournament _selectedTournament;

        public RelayCommand NavigateCommand { get; private set; }
        public RelayCommand CommandNavigateSelect { get; private set; }
        public RelayCommand CommandNavigateModification { get; private set; }

        public Tournament SelectedTournament
        {
            get
            {
                return _selectedTournament;
            }
            set
            {
                _selectedTournament = value;
                CommandNavigateSelect.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedTournament");
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

        public TournamentViewModel(INavigationService navigationService) : base(navigationService)
        {
            if (IsInDesignMode)
            {
                _tournaments = new ObservableCollection<Tournament> { new Tournament() { NameTournament = "Tournois1" }, new Tournament() { NameTournament = "Tounrnois2" } };
            }
            else
            {
                NavigateCommand = new RelayCommand(NavigateToAddTournament);
                CommandNavigateSelect = new RelayCommand(NavigateToSelectTournament, IsSelected);
                CommandNavigateModification = new RelayCommand(NavigateToSelectTournament);
                //SetTournaments();
            }
        }
        private bool IsSelected()
        {
            return SelectedTournament != null;
        }
        
        private void NavigateToSelectTournament()
        {
            _navigationService.NavigateTo("SelectTournament", SelectedTournament);
        }
        private void NavigateToAddTournament()
        {
            
            _navigationService.NavigateTo("AddTournament");

        }
       
        private async Task SetTournaments()
        {
            var service = new TournamentsServices();
            try
            {
                List<Tournament> tournament = await service.GetTournaments();
                Tournaments = new ObservableCollection<Tournament>(tournament);
            }
            catch(Exception e)
            {
                SetGeneralErrorMessage(e);
            }

        }

        public async void NavigatedTo(object parameter)
        {
            await SetTournaments();
        }

        public void NavigatedFrom(object parameter)
        {
            //nothing to do
        }
    }
}
