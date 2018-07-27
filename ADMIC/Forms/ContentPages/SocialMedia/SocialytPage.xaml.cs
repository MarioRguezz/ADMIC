using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ADMIC.Data.Models;
using ADMIC.Forms.ContentPages.Menu;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ADMIC.Forms.ContentPages.SocialMedia
{
    public partial class SocialytPage : ContentPage
    {

        // Get your API Key @ https://console.developers.google.com/apis/api/youtube/
        const string ApiKey = "AIzaSyAC3a2iiHUyEMgzSEuaVsEuqXbxkvKtktg";
        const string ChannelId = "UCFv98pYn2iEPbRMuizGUpTg";
        //UCFv98pYn2iEPbRMuizGUpTg

        // Documentation @ https://developers.google.com/apis-explorer/#p/youtube/v3/youtube.videos.list 
        string channelUrl = $"https://www.googleapis.com/youtube/v3/search?part=id&maxResults=20&channelId={ChannelId}&key={ApiKey}";
        string detailsUrl = "https://www.googleapis.com/youtube/v3/videos?part=snippet,statistics&key=" + ApiKey + "&id={0}";

        public ObservableCollection<YouTubeItem> Items { get; set; } = new ObservableCollection<YouTubeItem>();

        public SocialytPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            youtubelist.ItemsSource = Items;
            youtubelist.ItemTapped += (sender, e) => {
                if (e.Item == null) return;
                if(e.Item is YouTubeItem){
                    var item = e.Item as YouTubeItem;
                    Device.OpenUri(new Uri("https://youtu.be/" + item.VideoId));
                }
                youtubelist.SelectedItem = null;
            };

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(Items.Count() == 0)
                LoadData();
        }

        public async void LoadData()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var videoIds = new List<string>();
                    var json = await httpClient.GetStringAsync(channelUrl);

                    // Deserialize our data, this is in a simple List format
                    var response = JsonConvert.DeserializeObject<YouTubeApiListRoot>(json);

                    // Add all the video id's we've found to our list.
                    System.Diagnostics.Debug.WriteLine(string.Join(",",response.items.Select(item => item.id.videoId)));
                    videoIds.AddRange(response.items.Select(item => item.id.videoId));

                    // Get the details for all our items
                    var data = await GetVideoDetailsAsync(videoIds);

                    System.Diagnostics.Debug.WriteLine(string.Join(",", data.Select(item => item.Description)));

                    foreach(var item in data)
                    {
                        Device.BeginInvokeOnMainThread(()=>{
                             Items.Add(item);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                var ms = ex;
            }
         
        }

        async Task<List<YouTubeItem>> GetVideoDetailsAsync(List<string> videoIds)
        {
            try
            {
                var videoIdString = string.Join(",", videoIds);
                var youtubeItems = new List<YouTubeItem>();

                using (var httpClient = new HttpClient())
                {
                    var json = await httpClient.GetStringAsync(string.Format(detailsUrl, videoIdString));
                    var response = JsonConvert.DeserializeObject<YouTubeApiDetailsRoot>(json);

                    foreach (var item in response.items)
                    {
                        var youTubeItem = new YouTubeItem()
                        {
                            Title = item.snippet.title,
                            Description = item.snippet.description,
                            ChannelTitle = item.snippet.channelTitle,
                            PublishedAt = item.snippet.publishedAt,
                            VideoId = item.id,
                            DefaultThumbnailUrl = item.snippet?.thumbnails?.@default?.url,
                            MediumThumbnailUrl = item.snippet?.thumbnails?.medium?.url,
                            HighThumbnailUrl = item.snippet?.thumbnails?.high?.url,
                            StandardThumbnailUrl = item.snippet?.thumbnails?.standard?.url,
                            MaxResThumbnailUrl = item.snippet?.thumbnails?.maxres?.url,
                            ViewCount = item.statistics?.viewCount,
                            LikeCount = item.statistics?.likeCount,
                            DislikeCount = item.statistics?.dislikeCount,
                            FavoriteCount = item.statistics?.favoriteCount,
                            CommentCount = item.statistics?.commentCount,
                            Tags = item.snippet?.tags
                        };

                        youtubeItems.Add(youTubeItem);
                    }
                }

                return youtubeItems;
            }
            catch (Exception ex)
            {
                var ms = ex;
                return new List<YouTubeItem>();
            }
        }
    }

}
