using smartchUWP.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace smartchUWP.View.Pages
{
    public class BindablePage : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (this.DataContext is INavigable navigableViewModel)
                navigableViewModel.NavigatedTo(e.Parameter);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (this.DataContext is INavigable navigableViewModel)
                navigableViewModel.NavigatedFrom(e.Parameter);
        }
    }
}
