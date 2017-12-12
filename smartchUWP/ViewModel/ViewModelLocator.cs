using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using smartchUWP.Services;
using smartchUWP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace smartchUWP.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<ClubsViewModel>();
            SimpleIoc.Default.Register<AddMembreViewModel>();
            SimpleIoc.Default.Register<MembresModelView>();
            SimpleIoc.Default.Register<TournamentViewModel>();
            SimpleIoc.Default.Register<AddTournamentViewModel>();
            SimpleIoc.Default.Register<AddClubViewModel>();



            FrameNavigationService navigationPages = new FrameNavigationService();


            navigationPages.Configure("Home", typeof(View.Clubs.Clubs));
            navigationPages.Configure("Clubs",  typeof(View.Clubs.Clubs));
            navigationPages.Configure("AddClub", typeof(View.Clubs.AddClub));
            navigationPages.Configure("Membres", typeof(View.Membres.Membres));
            navigationPages.Configure("AddMembre", typeof(View.Membres.AddMembre));
            navigationPages.Configure("Tournaments", typeof(View.Tournaments.Tournaments));
            navigationPages.Configure("AddTournament", typeof(View.Tournaments.AddTournament));
            navigationPages.Configure("Login", typeof(View.Login));
            navigationPages.Configure("Register", typeof(View.Register));
            SimpleIoc.Default.Register<INavigationService>(() => navigationPages);


        }
        public MainPageViewModel MainPage { get { return ServiceLocator.Current.GetInstance<MainPageViewModel>(); } }
        public ClubsViewModel Clubs { get { return ServiceLocator.Current.GetInstance<ClubsViewModel>(); } }
        public AddMembreViewModel AddMembre { get { return ServiceLocator.Current.GetInstance<AddMembreViewModel>(); } }
        public MembresModelView Membres { get { return ServiceLocator.Current.GetInstance<MembresModelView>(); } }
        public TournamentViewModel Tournaments { get { return ServiceLocator.Current.GetInstance<TournamentViewModel>(); } }
        public AddTournamentViewModel AddTournament { get { return ServiceLocator.Current.GetInstance<AddTournamentViewModel>(); } }
        public AddClubViewModel AddClub { get { return ServiceLocator.Current.GetInstance<AddClubViewModel>(); } }
        public LoginViewModel Login { get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); } }

    }
}
