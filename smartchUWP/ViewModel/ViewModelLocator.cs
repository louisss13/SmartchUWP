using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<ClubsViewModel>();
            SimpleIoc.Default.Register<AddMembreViewModel>();
            SimpleIoc.Default.Register<MembresModelView>();
            SimpleIoc.Default.Register<TournamentViewModel>();


            NavigationService navigationPages = new NavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationPages);
            navigationPages.Configure("Clubs",  typeof(View.Clubs.Clubs));
            navigationPages.Configure("Membres", typeof(View.Membres.Membres));
            navigationPages.Configure("Tournaments", typeof(View.Tournaments.Tournaments));



        }
        public ClubsViewModel Clubs { get { return ServiceLocator.Current.GetInstance<ClubsViewModel>(); } }
        public AddMembreViewModel AddMembre { get { return ServiceLocator.Current.GetInstance<AddMembreViewModel>(); } }
        public MembresModelView Membres { get { return ServiceLocator.Current.GetInstance<MembresModelView>(); } }
        public TournamentViewModel Tournaments { get { return ServiceLocator.Current.GetInstance<TournamentViewModel>(); } }

    }
}
