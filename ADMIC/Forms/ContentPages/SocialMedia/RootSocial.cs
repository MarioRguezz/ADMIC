using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ADMIC.Forms.ContentPages.Menu;
using Xamarin.Forms;

namespace ADMIC.Forms.ContentPages.SocialMedia
{
    public partial class RootSocial : TabbedPage
    {
        HomeDrawerPage RootPage;
       

        public RootSocial(HomeDrawerPage _rootPage)
        {
            RootPage = _rootPage;
            // showPage();
            NavigationPage.SetHasNavigationBar(this, true);
            var p1 = new SocialytPage();
            p1.Icon = "pgj_ic_event.png";
            p1.Title = "Youtube";
            var p2 = new SocialFbPage();
            p2.Icon = "pgj_ic_event.png";
            p2.Title = "Facebook";
            Children.Add(p1);
            Children.Add(p2);
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if(CurrentPage!=null){
                Title = CurrentPage.Title;
            }

        }
    }
}

