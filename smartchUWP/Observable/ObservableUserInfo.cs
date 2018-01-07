using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.Observable
{
    public class ObservableUserInfo : User, INotifyPropertyChanged
    {
        public ObservableUserInfo() : base()
        { }
        public ObservableUserInfo(User user) : base()
        {
            base.Id = user.Id;
            Adresse = user.Adresse;
            base.Birthday = user.Birthday;
            base.Email = user.Email;
            base.Name = user.Name;
            base.FirstName = user.FirstName;
            base.Phone = user.Phone;


        }
        


        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
