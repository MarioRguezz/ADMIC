using System;
using ADMIC.Data.Models;
using ADMIC.Forms.ContentPages.About;
using ADMIC.Forms.ContentPages.Call;
using ADMIC.Forms.ContentPages.Event;
using ADMIC.Forms.ContentPages.GuanajovenCode;
using ADMIC.Forms.ContentPages.Home;
using ADMIC.Forms.ContentPages.Login;
using ADMIC.Forms.ContentPages.Notifications;
using ADMIC.Forms.ContentPages.Profile;
using ADMIC.Forms.ContentPages.Promotion;
using ADMIC.Forms.ContentPages.Region;
using ADMIC.Forms.ContentPages.SocialMedia;
using Xamarin.Forms;

namespace ADMIC.Forms.ContentPages.Menu
{
    public class HomeDrawerPage : MasterDetailPage
    {

        public DrawerListPage _drawer;

        public HomeDrawerPage(DrawerPage page = DrawerPage.HomeView)
        {

            _drawer = new DrawerListPage(this);
            _drawer.PageSelected += (pageType) =>
            {
                bool presented = false;
                switch (pageType)
                {
                    case DrawerPage.HomeView:
                        Detail = new NavigationPage(new MasterHomePage(this));
                        break;
                    case DrawerPage.ProfileView:

                        Detail = new NavigationPage(new ProfilePage(this));
                        break;
                    case DrawerPage.CodigoView:
                        //Detail.Navigation.PushAsync(new GuanajovenCodePage());
                        Detail = new NavigationPage(new GuanajovenCodePage(this));
                        break;
                    case DrawerPage.Cerrar:
                        var user = PropertiesManager.GetUserInfo();
                        if (user != null)
                        {
                            FirebaseObject firebase = new FirebaseObject();
                            firebase.id_usuario = user.data.id + "";
                            var canceltoken = ClientGuanajoven.CancelarFirebase(firebase);
                            PropertiesManager.LogOut();
                            ////await new Navigation.PushModalAsync(new RootPage());
                            //Navigation.PushModalAsync(new NavigationPage(new RootPage()));
                            App.Current.MainPage = new NavigationPage(new RootPage());
                        }
                        break;
                    case DrawerPage.EventosView:
                        //Detail.Navigation.PushAsync(new EventsView());
                        Detail = new NavigationPage(new EventsView(this));
                        break;
                    case DrawerPage.ConvocatoriasView:
                        Detail = new NavigationPage(new CallPage(this));
                        break;
                    case DrawerPage.PromotionView:
                        //Detail.Navigation.PushAsync(new CalendarPage());

                        Detail = new NavigationPage(new EmpresaPage(this));
                        break;
                    case DrawerPage.NotificacionesView:
                        //Detail.Navigation.PushAsync(new NotificationPage());

                        Detail = new NavigationPage(new NotificationPage(this));
                        break;
                    case DrawerPage.RedesSocialesView:
                        //Detail.Navigation.PushAsync(new SocialMediaPage());

                        //Detail = new NavigationPage(new SocialMediaPage(this));
                        ShowSocialMediaPage();
                        break;
                    case DrawerPage.ChatView:
                        //Detail.Navigation.PushAsync(new ());

                        Detail = new NavigationPage(new ChatPage(this));
                        break;
                    case DrawerPage.RegionesView:
                        //Detail.Navigation.PushAsync(new ());

                        Detail = new NavigationPage(new RegionPage(this));
                        break;
                    case DrawerPage.AcercaView:
                        //Detail.Navigation.PushAsync(new ());

                        Detail = new NavigationPage(new AboutPage(this));
                        break;

                }

                IsPresented = presented;
            };
            Master = _drawer;

            if (page == DrawerPage.HomeView)
            {
                Detail = new NavigationPage(new MasterHomePage(this));
            }
            else
            {
                switch (page)
                {
                    case DrawerPage.ConvocatoriasView:
                        //Detail = new NavigationPage(new MapInfoDelivery(this));
                        break;
                }
            }

            MasterBehavior = MasterBehavior.Popover;

            IsPresentedChanged += (sender, e) =>
            {

                //_drawer.UpdateView(); 
            };

            SetListeners();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void ShowSocialMediaPage()
        {
            Detail = new NavigationPage(new RootSocial(this))
            {
                BarBackgroundColor = Color.FromHex("#003465"),
                BarTextColor = Color.White,
            };
        }

        void SetListeners()
        {
            MessagingCenter.Subscribe<ProfilePage>(this, "show_home", (sender) =>
            {
                Detail = new NavigationPage(new MasterHomePage(this));
            });
        }
    }
}