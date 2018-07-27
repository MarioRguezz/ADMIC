using System;
using System.Collections.Generic;
using ADMIC.Forms.ContentPages.Home;
using ADMIC.Forms.ContentPages.Login;
using Xamarin.Forms;

namespace ADMIC.Forms.ContentPages.Login
{
	public partial class RootPage : ContentPage
	{
		public RootPage()
		{
			InitializeComponent();
			Background.BackgroundColor = Color.FromHex("#83074677");
			NavigationPage.SetHasNavigationBar(this, false);
        }

		async void Session(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new LoginPage());
		}

		async void SignUp(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new SignUpPage());
		}

		async void VideoClicked(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new videoPage());
		}

	}
}
