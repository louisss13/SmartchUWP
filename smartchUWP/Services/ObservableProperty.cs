using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.Services
{
    public class ObservableProperty<T> : INotifyPropertyChanged
    {
        public T Object;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableProperty(T @object)
        {
            Object = @object;
        }

       
    }
}
