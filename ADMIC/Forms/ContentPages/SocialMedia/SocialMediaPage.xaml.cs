using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ADMIC.Forms.ContentPages.Menu;
using Xamarin.Forms;

namespace ADMIC.Forms.ContentPages.SocialMedia
{

    public partial class SocialMediaPage : TabbedPage
    {
        HomeDrawerPage RootPage;
     
        public SocialMediaPage(HomeDrawerPage _rootPage)
        {
            InitializeComponent();
            RootPage = _rootPage;
            // showPage();
            NavigationPage.SetHasNavigationBar(this, false);
            var navigationPage = new NavigationPage(new SocialytPage());
            navigationPage.Icon = "pgj_ic_event.png";
            navigationPage.Title = "Schedule";
            Children.Add(navigationPage);
        }

      /*  async void CloseClicked(object sender, System.EventArgs e)
        {
            var image = sender as Image;
            image.Opacity = 0.6;
            image.FadeTo(1);
            //await Navigation.PopAsync();
            //MessagingCenter.Send<ProfilePage>(this, "show_home");
            RootPage.IsPresented = true;
        }*/

      
    }
}
