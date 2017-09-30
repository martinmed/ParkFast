using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Map_Test
{
    public class MapTest : ContentPage
    {

        private Map mapa = new Map();

        private async void findMe()
        {
            var locator = CrossGeolocator.Current;
            Plugin.Geolocator.Abstractions.Position position = new Plugin.Geolocator.Abstractions.Position();

            position = await locator.GetPositionAsync();
            mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                            Distance.FromMiles(1)));
        }
        private async void requestGPSAsync()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }
                if (status == PermissionStatus.Granted)
                {
                    var results = await CrossGeolocator.Current.GetPositionAsync();
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                
            }
        }



        public MapTest()
        {
            mapa.IsShowingUser = true;
            requestGPSAsync();
            findMe();
            

            var position = new Position(-38.740355, -72.591244);
            var pin1 = new Pin
            {
                Type = PinType.Generic,
                
                Position = position,
                Label = "Estacionamiento Reservado",
                Address = "Calle Antonio Varas con Arturo Pratt"
            };

            mapa.Pins.Add(pin1);

            Content = new StackLayout
            {
                Children = {
                    mapa
                }
            };
        }
    }
}
