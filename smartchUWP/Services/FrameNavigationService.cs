using GalaSoft.MvvmLight.Views;
using smartchUWP.View;
using smartchUWP.View.Clubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace smartchUWP.Services
{
    public class FrameNavigationService : INavigationService
    {
        
        private readonly string  rootString = "---ROOT---";
        public string CurrentPageKey { get; set; }
        private bool IsRootFrame { get; set; } = false;
        public Dictionary<string, Type> Configuration { get; set; } = new Dictionary<string, Type>();
        public Dictionary<string, int> ConfigurationRootLevel { get; set; } = new Dictionary<string, int>();
        public Frame CurrentFrame { get; set; }
        private Frame RootFrame { get; set; }
        private MainPage MainPage { get; set; }

        public FrameNavigationService() {
            
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            GoBack();
            
        }
        private void SetAppBarBackButtonVisibility()
        {
            bool canGoBack = false;
            if (this.ConfigurationRootLevel.ContainsKey(CurrentPageKey))
            {
                switch (this.ConfigurationRootLevel[CurrentPageKey]) {
                    case 0:
                        canGoBack = RootFrame.CanGoBack;
                        break;
                    case 1:
                        canGoBack = false;
                        break;
                    case 2:
                        canGoBack = CurrentFrame.CanGoBack;
                        break;
                }
            }
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = (canGoBack) ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
        

        private void SetRootFrame()
        {
            if(MainPage == null )
                MainPage = new MainPage();
           
            RootFrame = (Window.Current.Content as Frame);
            RootFrame.Navigate(MainPage.GetType());

            CurrentFrame = ((Window.Current.Content as Frame).Content as MainPage).AppFrame;

            IsRootFrame = true;
        }

        public void Configure(string key, Type type, int rootLevel)
        {
            Configuration.Add(key, type);
            ConfigurationRootLevel.Add(key, rootLevel);
        }

        public void GoBack()
        {
            if (this.ConfigurationRootLevel.ContainsKey(CurrentPageKey))
            {
               
                switch (this.ConfigurationRootLevel[CurrentPageKey])
                {
                    case 0:
                        if (RootFrame.CanGoBack)
                        {
                            RootFrame.GoBack();
                            SetAppBarBackButtonVisibility();
                        }
                        break;


                    case 2:
                        if (CurrentFrame.CanGoBack)
                        {
                            CurrentFrame.GoBack();
                            SetAppBarBackButtonVisibility();
                        }

                        break;
                }
            }
            CurrentPageKey = "Login";


        }

        public void NavigateTo(string pageKey)
        {
            this.NavigateTo(pageKey,null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            CurrentPageKey = pageKey;
            if (!IsRootFrame)
            {
                SetRootFrame();
            }

            
            if (this.Configuration.ContainsKey(pageKey))
            {
                Type pageType = this.Configuration[pageKey];
                Frame frametoNavigate;
                switch (this.ConfigurationRootLevel[pageKey])
                {
                    case 0:
                        frametoNavigate = RootFrame;
                    break;
                    case 1:
                        frametoNavigate = CurrentFrame;
                        break;
                    default:
                        frametoNavigate = RootFrame;
                        break;
                }
                
                if (parameter == null)
                {
                    frametoNavigate.Navigate(pageType);
                }
                else
                    frametoNavigate.Navigate(pageType, parameter);
            }
            else
                Console.WriteLine("Key "+ pageKey + " unknow");

            SetAppBarBackButtonVisibility();
        }
    }
}
