using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.Observable
{
    public class ObservableTournament : Tournament, INotifyPropertyChanged
    {
        public ObservableTournament() : base()
        { }
        public ObservableTournament(Tournament tournament) : base()
        {
            Adresse = new ObservableAddress(tournament.Address);
            base.Address = tournament.Address;
            base.Address = Adresse;
            Admins = new ObservableCollection<Account>(tournament.Admins);
            base.Admins = tournament.Admins;
            base.BeginDate = (tournament.BeginDate == null)?new DateTime(): tournament.BeginDate;
            base.Club = tournament.Club;
            base.EndDate = (tournament.EndDate == null) ? new DateTime() : tournament.EndDate; ;
            base.Etat = tournament.Etat;
            base.Id = tournament.Id;
            base.Name = tournament.Name;
            Participants = new ObservableCollection<User>(tournament.Participants);
            base.Participants = tournament.Participants;
            
        }
        private ObservableAddress _address = new ObservableAddress() { Street = "coucou"};
        private ObservableCollection<User> _participants;

        public new ObservableCollection<Account> Admins { get; set; }
        public new ObservableCollection<User> Participants
        {
            get
            {
                return _participants;
            }
            set
            {
                _participants = value;
                base.Participants = _participants;
                RaisePropertyChanged("Participants");
            }
        }
        public ObservableAddress Adresse
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                Address = _address;
                RaisePropertyChanged("Address");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
