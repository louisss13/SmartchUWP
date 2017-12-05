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
    public class AddMembreViewModel : ViewModelBase
    {
        private ObservableCollection<Club> _clubs = null;
        public AddMembreViewModel()
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

        }
    }
}
