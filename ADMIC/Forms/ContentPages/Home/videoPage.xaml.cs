using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ADMIC.Forms.ContentPages.Home
{
    public partial class videoPage : BasePage
	{
		public videoPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
				

		}


		async void CloseClicked(object sender, EventArgs args)
		{
			await Navigation.PopAsync();
		}

	}




}
