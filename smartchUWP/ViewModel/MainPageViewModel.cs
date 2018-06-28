using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using smartchUWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace smartchUWP.ViewModel
{
    public class MainPageViewModel : SmartchViewModelBase, IAfficheErrorGeneral
    {
        private Boolean _isChargement = false;
        private Boolean _isGeneralError = false;
        private Boolean _isGeneralErrorVisible = false;
        private String _errorDescription = "";

        public Boolean IsChargement
        {
            get
            {
                return _isChargement;
            }
            set
            {
                _isChargement = value;
                RaisePropertyChanged("IsChargement");
                RaisePropertyChanged("IsNotChargement");
            }
        }
        public Boolean IsNotChargement
        {
            get
            {
                return !IsChargement;
            }
            set
            {
                IsChargement = value;
            }
        }
        public Boolean IsGeneralErrorVisible
        {
            get
            {
                return _isGeneralErrorVisible;
            }
            set
            {
                _isGeneralErrorVisible = value;
                RaisePropertyChanged("IsGeneralErrorVisible");
            }
        }
        public Boolean IsGeneralError
        {
            get
            {
                return _isGeneralError;
            }
            set
            {
                _isGeneralError = value;
                RaisePropertyChanged("IsGeneralError");
            }
        }

        public String ErrorDescription
        {
            get
            {
                return _errorDescription;
            }
            set
            {
                _errorDescription = value ;
                RaisePropertyChanged("ErrorDescription");
            }
        }

        public RelayCommand<Object> CommandSelectChangeMenu { get; private set; }
        public RelayCommand<Object> CommandInvokedMenu { get; private set; }
        

        public MainPageViewModel(INavigationService navigationService):base(navigationService)
        {
            
            CommandSelectChangeMenu = new RelayCommand<Object>(SelectionChanged );
            CommandInvokedMenu = new RelayCommand<Object>(InvokedItemMenu);
        }
        
        
        public void SelectionChanged(Object args)
        {
            NavigationViewSelectionChangedEventArgs navigationItem = (NavigationViewSelectionChangedEventArgs)args;
             if (navigationItem.IsSettingsSelected)
            {
               _navigationService.NavigateTo("Login");
            }
            else
            {

                NavigationViewItem item = navigationItem.SelectedItem as NavigationViewItem;
                SwitchItemNavigation(item);
                
            }
        }
        

        private void InvokedItemMenu(Object args)
        {
            NavigationViewItemInvokedEventArgs navigationItem = (NavigationViewItemInvokedEventArgs)args;
            if (navigationItem.IsSettingsInvoked)
            {
                _navigationService.NavigateTo("Login");
            }
            else
            {
                NavigationViewItem item = navigationItem.InvokedItem as NavigationViewItem;
                SwitchItemNavigation(item);
            }
        }
        private void SwitchItemNavigation(NavigationViewItem item) {
            if(item != null)
            switch (item.Tag)
            {
                case "home":
                    _navigationService.NavigateTo("Login");
                    break;

                case "members":
                    _navigationService.NavigateTo("Membres");
                    break;

                case "clubs":
                    _navigationService.NavigateTo("Clubs");
                    break;

                case "spectators":
                    _navigationService.NavigateTo("Login");
                    break;

                case "tournaments":
                    _navigationService.NavigateTo("Tournaments");
                    break;
            }
        }

       
    }
}
