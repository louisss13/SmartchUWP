using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using smartchUWP.Observable;
using smartchUWP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace smartchUWP.ViewModel
{
    public class AddClubViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public RelayCommand CommandAddClub { get; private set; }

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


        public AddClubViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            CommandAddClub = new RelayCommand(AddClub);
        }

        public async void AddClub()
        {
            ClubsServices clubsServices = new ClubsServices();
            ResponseObject response = await clubsServices.AddClub(Club);

            
            if (response.Success)
            {
                MessengerInstance.Send(new NotificationMessage(NotificationMessageType.ListClub));

            }
            else { }
               // TextErreur.Visibility = Visibility.Visible;

        }

    }
}
