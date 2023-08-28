using Lottie.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PuntoDeventa.UI.Auth.Screen
{
    public class SuccessScreem: ContentView
    {
        public SuccessScreem()
        {
            Content = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                Children = {
                    Success()
                }
            };
        }

        private AnimationView Success()
        {
            Stream json = new MemoryStream(Properties.Resources.login_success);
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
