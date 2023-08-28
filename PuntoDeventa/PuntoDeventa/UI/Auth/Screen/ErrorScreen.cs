using Lottie.Forms;
using System;
using System.IO;

using Xamarin.Forms;

namespace PuntoDeventa.UI.Auth.Screen
{
    public class ErrorScreen : ContentView
    {
        public ErrorScreen(string error, Action action, double height = 0, double width = 0)
        {
            WidthRequest = width;
            HeightRequest = height;
            var content = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand, 
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = new RowDefinitionCollection()
                {
                    new RowDefinition(){ Height = 300},
                    new RowDefinition() { Height = height.Equals(0) ? 400 : height - 550},
                    new RowDefinition{ Height = 100 },
                },
                Children =
                {

                }
            };

            content.Children.Add(ErrorAnimation(), 0, 0);
            content.Children.Add(ErrorText(error), 0, 1);
            content.Children.Add(ErrorButton(action), 0, 2);    

            Content = content;
        }

        private AnimationView ErrorAnimation()
        {
            Stream json = new MemoryStream(Properties.Resources.icecreams);
            return new AnimationView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RepeatMode = RepeatMode.Infinite,
                AutoPlay = true,
                Speed = 0.5f,
                Animation = json,
                AnimationSource = AnimationSource.Stream

            };
        }

        private Label ErrorText(string error)
        {
            return new Label()
            {
                Text = error, 
                Margin = new Thickness(20),
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 20
            };
             
        }

        private Button ErrorButton(Action action)
        {
            return new Button()
            {
                Text = "Reintentar",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                FontSize = 15,
                Command = new Command(action),
                BorderColor = Color.Black,
                BackgroundColor = Color.Red,
                CornerRadius = 12,
            };
        }
    }
}