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
        
        
        public string CurrentPageKey { get; set; }
        private Stack<String> PageKeyHistorique { get; set; } = new Stack<string>();
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
            
            bool canGoBack = PageKeyHistorique.Count() > 0;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = (canGoBack) ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
        

        private void SetRootFrame()
        {
            if (RootFrame == null)
            {
                CurrentPageKey = "Login";
                RootFrame = (Window.Current.Content as Frame);
            }
        }
        private void SetMainPage(string pageKey)
        {
            int oldRootType = (PageKeyHistorique.Count() > 0) ? this.ConfigurationRootLevel[PageKeyHistorique.Peek()] : -1;
            
            if (oldRootType != 2 && oldRootType != 3)
            {
                if (MainPage == null)
                    MainPage = new MainPage();
                RootFrame.Navigate(MainPage.GetType());

                CurrentFrame = ((Window.Current.Content as Frame).Content as MainPage).AppFrame;
            }

        }
        public void Configure(string key, Type type, int rootLevel)
        {
            Configuration.Add(key, type);
            ConfigurationRootLevel.Add(key, rootLevel);
        }

        public void GoBack()
        {
            if (PageKeyHistorique.Count() > 0)
            {
                string pageKeyOld = PageKeyHistorique.Pop();
                Frame frameToGoBack = GetFrame(CurrentPageKey);
                CurrentPageKey = pageKeyOld;

                if (frameToGoBack.CanGoBack)
                {
                    frameToGoBack.GoBack();
                }
                SetAppBarBackButtonVisibility();

            }
        
        }

        public void NavigateTo(string pageKey)
        {
            this.NavigateTo(pageKey,null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            SetRootFrame();

            if (this.Configuration.ContainsKey(pageKey))
            {
                Type pageType = this.Configuration[pageKey];
                
                InitNavigation(pageKey);
                Frame frametoNavigate = GetFrame(pageKey);


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

        private Frame GetFrame(string pageKey)
        {
            
            switch (this.ConfigurationRootLevel[pageKey])
            {
                case 2:
                case 3:
                    return CurrentFrame;
                default:
                    return RootFrame;
                    
            }
        }
        private void InitNavigation(string pageKey)
        {
            if (CurrentPageKey != null)
            {
                PageKeyHistorique.Push(CurrentPageKey);
            }
            CurrentPageKey = pageKey;
            switch (this.ConfigurationRootLevel[pageKey])
            {
                case 0:
                    PageKeyHistorique.Clear();
                    break;
                
                case 2:
                    PageKeyHistorique.Clear();
                    SetMainPage(pageKey);
                    break;
                case 3:
                    SetMainPage(pageKey);
                    break;
            }
        }
    }
}
