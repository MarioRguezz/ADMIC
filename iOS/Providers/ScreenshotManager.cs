using System;
using Xamarin.Forms;
using UIKit;

using BigTed;
using System.IO;
using ADMIC.iOS.Providers;
using ADMIC.Providers;

[assembly: Dependency(typeof(ScreenshotManager))]
namespace ADMIC.iOS.Providers
{
    public class ScreenshotManager : IScreenshotManager
    {

        public async System.Threading.Tasks.Task<byte[]> CaptureAsync()
        {
            var view = UIApplication.SharedApplication.KeyWindow.RootViewController.View;

            UIGraphics.BeginImageContext(view.Frame.Size);
            view.DrawViewHierarchy(view.Frame, true);
            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            using (var imageData = image.AsPNG())
            {
                var bytes = new byte[imageData.Length];
                System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, bytes, 0, Convert.ToInt32(imageData.Length));
                return bytes;
            }

        }

    }
}