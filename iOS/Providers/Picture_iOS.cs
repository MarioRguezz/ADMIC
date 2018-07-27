using System;
using Xamarin.Forms;
using UIKit;
using BigTed;
using Foundation;
using ADMIC.iOS.Providers;
using ADMIC.Providers;

[assembly: Xamarin.Forms.Dependency(typeof(Picture_iOS))]
namespace ADMIC.iOS.Providers
{
    public class Picture_iOS : IPicture
    {
        public void SavePictureToDisk(string filename, byte[] imageData)
        {
            var chartImage = new UIImage(NSData.FromArray(imageData));
            chartImage.SaveToPhotosAlbum((image, error) =>
            {
                //you can retrieve the saved UI Image as well if needed using
                //var i = image as UIImage;
                if (error != null)
                {
                    Console.WriteLine(error.ToString());
                }
            });
        }
    }
}