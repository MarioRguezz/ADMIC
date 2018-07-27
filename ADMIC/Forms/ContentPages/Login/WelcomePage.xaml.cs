using System;
using System.Collections.Generic;
using ADMIC.Data.Models;
using Xamarin.Forms;

namespace ADMIC.Forms.ContentPages.Login
{
	public partial class WelcomePage : BasePage
{
	public WelcomePage()
	{
		InitializeComponent();
		NavigationPage.SetHasNavigationBar(this, false);

	}

	async void Start(object sender, System.EventArgs e)
	{
		PropertiesManager.SaveFirstArrive();
		await Navigation.PushAsync(new RootPage());
	}
}
}
