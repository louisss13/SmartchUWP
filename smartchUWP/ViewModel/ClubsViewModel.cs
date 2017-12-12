using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccess;

using smartchUWP.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Windows.UI.Notifications;

namespace smartchUWP.ViewModel
{
    public class ClubsViewModel : ViewModelBase
    {
        public RelayCommand CmdNavigateAddClub { get; private set; }

        private ObservableCollection<Club> _clubs = null;

        INavigationService _navigationService;

        public ClubsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            CmdNavigateAddClub = new RelayCommand(NavigateToAddClub);

            MessengerInstance.Register<NotificationMessage>(this, MessageReceiver);
            if (IsInDesignMode)
            {
                _clubs =   new ObservableCollection<Club>{ new Club() { Name = "Club1" }, new Club() { Name = "club2" } };
            }
            else
            {
               InitializeAsync();
            }
        }
        public ObservableCollection<Club> Clubs
        {
            get
            {
                return _clubs;
            }
            set
            {
                if (_clubs == value)
                {
                    return;
                }
                _clubs = value;
                RaisePropertyChanged("Clubs");
            }
        }
        public async Task InitializeAsync()
        {
            SetClubs();
            
        }
        private void NavigateToAddClub()
        {
            _navigationService.NavigateTo("AddClub");
        }
        private async void SetClubs()
        {
            var service = new ClubsServices();
            var clubs = await service.GetClubs();
            Clubs = new ObservableCollection<Club>(clubs);
        }
        private void MessageReceiver(NotificationMessage message)
        {
            switch (message.VariableType)
            {
                case NotificationMessageType.ListClub:
                    SetClubs();
                    //ShowToastNotification("testc", "coucou");
                    break;
            }
        }
        //TODO Maybe later ! mauvaise place pour utiliser ce code
       /* private void ShowToastNotification(string title, string stringContent)
        {
            ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(title));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(stringContent));
            Windows.Data.Xml.Dom.IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            ToastNotification toast = new ToastNotification(toastXml);
            toast.ExpirationTime = DateTime.Now.AddSeconds(4);
            ToastNotifier.Show(toast);
        }
        */
    }
}
