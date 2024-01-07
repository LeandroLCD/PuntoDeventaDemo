using Lottie.Forms;
using System.IO;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

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
                BackgroundColor = Colors.White,
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