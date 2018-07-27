using GalaSoft.MvvmLight.Messaging;
using smartchUWP.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace smartchUWP.View.Tournaments
{
    public sealed partial class Tournaments : BindablePage
    {
        public Tournaments()
        {
            InitializeComponent();
            //Messenger.Default.Register<void>(this, AjouterTournament);
        }

       

        



    }
}
