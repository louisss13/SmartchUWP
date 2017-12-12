using smartchUWP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace smartchUWP.View
{
    public sealed partial class MainPage : Page, IRootFrame
    {
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        public Frame AppFrame { get { return this.ContentFrame; } }

        public Frame getRootFrame()
        {
            return AppFrame;
        }
    }
}
