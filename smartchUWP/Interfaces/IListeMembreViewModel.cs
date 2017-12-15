using GalaSoft.MvvmLight.Command;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.Interfaces
{
    public interface IListeMembreViewModel
    {
        ObservableCollection<User> AllMembers { get; }
        ObservableCollection<User> MembersEntity { get; }

        RelayCommand CommandAddMember { get;  }
        RelayCommand CommandDelMember { get;  }
        ObservableCollection<User> SelectedMembersEntity{get;set;}
        ObservableCollection<User> SelectedAllMembers{get;set;}

       
    }
}
