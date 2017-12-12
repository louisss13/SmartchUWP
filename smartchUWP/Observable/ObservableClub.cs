using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.Observable
{
    public class ObservableClub : Club, INotifyPropertyChanged
    {
        public ObservableClub() : base()
        { }
        public ObservableClub(Club club) : base()
        {
            base.ClubId = club.ClubId;
            base.Admins = club.Admins;
            base.Email = club.Email;
            base.Membres = club.Membres;
            base.Name = club.Name;
            base.Phone = club.Phone;

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
