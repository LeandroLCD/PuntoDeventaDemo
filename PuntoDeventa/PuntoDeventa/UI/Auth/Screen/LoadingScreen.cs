using Lottie.Forms;
using System.IO;
using Xamarin.Forms;

namespace PuntoDeventa.UI.Auth.Screen
{
    public class LoadingScreen : ContentView
    {
        public LoadingScreen()
        {
            Content = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                Children = {
                    Loading()
                }
            };
        }

        private AnimationView Loading()
        {
            Stream json = new MemoryStream(Properties.Resources.montacargas);
            return new AnimationView
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 400,
                HeightRequest = 400,
                RepeatMode = RepeatMode.Infinite,
                AutoPlay = true,
                Speed = 0.5f,
                Animation = json,
                AnimationSource = AnimationSource.Stream

            };
        }
    }
}