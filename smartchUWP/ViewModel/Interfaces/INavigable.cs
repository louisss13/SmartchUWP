using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel.Interfaces
{
    interface INavigable
    {
        void NavigatedTo(object parameter);
        void NavigatedFrom(object parameter);
    }
}
