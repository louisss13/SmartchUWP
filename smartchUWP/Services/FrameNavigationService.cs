using GalaSoft.MvvmLight.Views;
using smartchUWP.View;
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
        public string CurrentPageKey => throw new NotImplementedException();
        private readonly string  rootString = "---ROOT---";
        private bool IsRootFrame { get; set; } = false;
        public Dictionary<string, Type> Configuration { get; set; } = new Dictionary<string, Type>();
        public Frame CurrentFrame { get; set; }
        private Frame RootFrame { get; set; }

        public FrameNavigationService() {
            
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }
        private void App_BackRequested(object sender,BackRequestedEventArgs e)
        {
            if (CurrentFrame.CanGoBack)
            {
                CurrentFrame.GoBack();
                SetAppBarBackButtonVisibility();
            }
        }
        private void SetAppBarBackButtonVisibility()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = (CurrentFrame.CanGoBack) ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
        

            private void SetRootFrame()
        {
            if (Window.Current.Content is IRootFrame)
            {
                RootFrame = ((IRootFrame)Window.Current.Content).getRootFrame();
                CurrentFrame = RootFrame;
            }
           
            else
                throw new ArgumentNullException("Root Frame is undefined");
            IsRootFrame = true;
        }

        public void Configure(string key, Type type)
        {
            Configuration.Add(key, type);
        }

        public void GoBack()
        {
            CurrentFrame.GoBack();
        }

        public void NavigateTo(string pageKey)
        {
            this.NavigateTo(pageKey,null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            if (!IsRootFrame)
            {
                SetRootFrame();
            }

            
            if (this.Configuration.ContainsKey(pageKey))
            {
                Type pageType = this.Configuration[pageKey];
                if (parameter == null)
                {
                    CurrentFrame.Navigate(pageType);
                }
                else
                    CurrentFrame.Navigate(pageType, parameter);
            }
            else
                Console.WriteLine("Key "+ pageKey + " unknow");

            SetAppBarBackButtonVisibility();
        }
    }
}
