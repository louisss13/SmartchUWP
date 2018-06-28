using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.Observable
{
    public class ObservableAddress : Address, INotifyPropertyChanged
    {
        public ObservableAddress() : base()
        { }
            public ObservableAddress(Address address) : base()
        {
            base.Zipcode = address.Zipcode;
            base.Street = address.Street;
            base.Number = address.Number;
            base.Country = address.Country;
            base.City = address.City;
            base.Box = address.Box;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;


    }
}
