using System;
using Xamarin.Forms;
using UIKit;

using BigTed;
using System.IO;
using ADMIC.iOS.Providers;
using ADMIC.Providers;
using System.Threading.Tasks;
using ADMIC.Data.Models.Facebook;
using Facebook.CoreKit;
using Foundation;

[assembly: Dependency(typeof(FaceBookRequester))]
namespace ADMIC.iOS.Providers
{
    public class FaceBookRequester : IFaceBookRequester
    {
        TaskCompletionSource<FacebookResponse> _task;


        public Task<FacebookResponse> GetPosts()
        {
            _task = new TaskCompletionSource<FacebookResponse>();



            //"fields", "picture"
            var request = new GraphRequest("/admicleon/posts?fields=from,full_picture,permalink_url,message,story,created_time&locale=es", new NSDictionary(), "203591490120675|a909222218ad39a1de0767ea41a679b1", null, "GET");

            var requestConnection = new GraphRequestConnection();
            requestConnection.AddRequest(request, (connection, result, error) =>
            {
                try
                {
                    var jsonData = NSJsonSerialization.Serialize(result, 0, out error);

                    if (error == null)
                    {
                        var jsonString = (string)NSString.FromData(jsonData, NSStringEncoding.UTF8);

                        var response = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookResponse>(jsonString);

                        _task.SetResult(response);
                    }
                    else
                    {
                        _task.SetResult(null);
                    }

                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    _task.SetResult(null);
                }

            });
            requestConnection.Start();

            return _task.Task;
        }

        public Task<FacebookResponse> GetPosts(string padding)
        {

            _task = new TaskCompletionSource<FacebookResponse>();

            _task.SetResult(null);

            return _task.Task;
        }
    }
}