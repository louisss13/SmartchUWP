using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Model.ModelException;
using smartchUWP.Interfaces;
using smartchUWP.Services;
using smartchUWP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class SmartchViewModelBase : ViewModelBase
    {
        protected readonly INavigationService _navigationService;
        public SmartchViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
            
        }

        public void SetGeneralErrorMessage(Exception e, IAfficheErrorGeneral main = null)
        {
            if (main == null)
                main = GetMain();
            
            if (e is GetDataException)
            {
                main.IsGeneralError = true;
                main.ErrorDescription = "Un problème est survenu lors de l'envois d'une requète. Etes-vous connecté à internet?";
            }
            else
            {
                main.IsGeneralError = true;
                main.ErrorDescription = e.Message;
            }
            CacheError(main);
            
        }
        public async void CacheError(IAfficheErrorGeneral main)
        {
            await Task.Delay(5000);
            
                main.IsGeneralError = false;
                main.ErrorDescription = "";
            
        }
        private IAfficheErrorGeneral GetMain()
        {
            FrameNavigationService nav = ((FrameNavigationService)_navigationService);
            if (nav.MainPage is MainPage)
            {
                IAfficheErrorGeneral main = ((FrameNavigationService)_navigationService).MainPage.GetViewModel();
                return main;
            }
            return null;
        }
    }
}
