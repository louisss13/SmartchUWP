using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccess;

namespace smartchUWP.ViewModel
{
    public class ClubsViewModel : ViewModelBase
    {
        private ObservableCollection<Club> _clubs = null;
        public ClubsViewModel()
        {
            if (IsInDesignMode)
            {
               
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
