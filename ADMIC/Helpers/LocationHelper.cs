using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace ADMIC.Helpers
{
    public class LocationHelper
    {

        public Position CurrentPosition = null;
        public IGeolocator Geolocator;

        static LocationHelper _instance;
        public static LocationHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LocationHelper();
                }

                return _instance;
            }
        }

        public LocationHelper()
        {
            SetupGeoLocator();
        }

        async void SetupGeoLocator()
        {
            if (Geolocator != null)
                return;
            Geolocator = CrossGeolocator.Current;
            Geolocator.DesiredAccuracy = 100;

            Geolocator.PositionChanged += (sender, e) =>
            {
                CurrentPosition = e.Position;
                System.Diagnostics.Debug.WriteLine("PositionChanged {0} {1}", e.Position.Latitude, e.Position.Longitude);
            };


            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    var results = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(30));
                    await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(10), 1, false, null);
                }
                else if (status != PermissionStatus.Unknown)
                {
                    //await Application.Current.MainPage.DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("" + ex.Message);
                //LabelGeolocation.Text = "Error: " + ex;
            }

        }


    }
}