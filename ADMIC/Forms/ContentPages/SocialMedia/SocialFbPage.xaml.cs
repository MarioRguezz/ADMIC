using System;
using System.Collections.Generic;
using ADMIC.Data.Models.Facebook;
using ADMIC.Providers;
using Xamarin.Forms;

namespace ADMIC.Forms.ContentPages.SocialMedia
{
    public partial class SocialFbPage : ContentPage
    {
        public SocialFbPage()
        {
            InitializeComponent();

            //var p = new Post();

            fbList.ItemTapped += (sender, e) => {
                if (e.Item == null) return;
                if (e.Item is Post)
                {
                    Post item = e.Item as Post;

                    Device.OpenUri(new Uri(item.permalink_url));
                }
                fbList.SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GetPosts();

        }

        private async void GetPosts()
        {
            var requester = DependencyService.Get<IFaceBookRequester>();

            var posts = await requester.GetPosts();
            fbList.ItemsSource = posts.data;
        }
    }
}