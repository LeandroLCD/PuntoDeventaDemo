using Lottie.Forms;
using PuntoDeventa.UI.Auth.Models;
using Syncfusion.XForms.TextInputLayout;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace PuntoDeventa.IU.Auth.Screen
{
    public class LoginScreen : ContentView
    {
        public LoginScreen(AuthDataUser dataUser, Command<bool> isRemembermeCommand, Command LoginCommand, Command RegisterCommand)
        {
            Grid mainGrid = new Grid
            {
                Padding = new Thickness(50),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                RowDefinitions = new RowDefinitionCollection
                {
                new RowDefinition { Height = new GridLength(40) },
                new RowDefinition { Height = new GridLength(40) },
                new RowDefinition { Height = new GridLength(600) }
                }
            };





            mainGrid.Children.Add(PancakeContent(dataUser, isRemembermeCommand, LoginCommand, RegisterCommand), 0, 1, 1, 3);

            Image iconImage = new Image
            {
                Source = "icon.png",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            mainGrid.Children.Add(iconImage, 0, 1, 0, 2);

            Content = mainGrid;
        }
        private PancakeView PancakeContent(AuthDataUser dataUser, Command<bool> isRemembermeCommand, Command loginCommand, Command recoveryCommand)
        {
            BindingContext = dataUser;
            Grid gridLayout = new Grid
            {
                HorizontalOptions = LayoutOptions.Center,
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = new GridLength(80) },
                    new RowDefinition { Height = new GridLength(80) },
                    new RowDefinition { Height = new GridLength(40) },
                    new RowDefinition { Height = new GridLength(50) },
                    new RowDefinition { Height = new GridLength(40) }
                }
            };

            gridLayout.Children.Add(EntryInput(dataUser), 0, 0);

            gridLayout.Children.Add(PasswordInput(dataUser), 0, 1);

            gridLayout.Children.Add(Rememberme(isRemembermeCommand), 0, 2);

            gridLayout.Children.Add(LoginButton(loginCommand), 0, 3);

            var remembermeButton = new Button()
            {
                Text = "Recuperar Contraseña",
                TextColor = Color.BlueViolet,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 15,
                Command = recoveryCommand,
                BackgroundColor = Color.Transparent,
                CornerRadius = 12,
            };

            gridLayout.Children.Add(remembermeButton, 0, 4);

            return new PancakeView
            {
                Padding = new Thickness(20),
                CornerRadius = 10,
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Content = gridLayout
            };
        }
        private Button LoginButton(Command login)
        {
            return new Button()
            {
                Text = "Login",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 15,
                Command = login,
                BorderColor = Color.Black,
                BackgroundColor = Color.Red,
                CornerRadius = 12,
            };
        }

        private StackLayout Rememberme(Command<bool> IsRemembermeCommand)
        {
            var rememberme = new CheckBox();
            rememberme.CheckedChanged += (s, e) => {
                IsRemembermeCommand.Execute(e.Value);
            };

            return new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {

                    rememberme,
                    new Label()
                    {
                        Text = " Recordar Usuario", VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 15,
                        TextColor = Color.Black,
                        HorizontalOptions= LayoutOptions.StartAndExpand,
                    }

                }
            };
        }

        private SfTextInputLayout EntryInput(AuthDataUser authData)
        {
            Entry entry = new Entry();

            entry.SetBinding(Entry.TextProperty, new Binding(nameof(authData.Email)));

            SfTextInputLayout sfTextInput = new SfTextInputLayout
            {
                ContainerType = ContainerType.Outlined,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Hint = "Email",
                InputView = entry,
                ErrorColor = Color.Red
            };
            sfTextInput.SetBinding(SfTextInputLayout.ErrorTextProperty, new Binding(nameof(authData.PasswordErrorText)));
            sfTextInput.SetBinding(SfTextInputLayout.HasErrorProperty, new Binding(nameof(authData.HasEmail)));
            return sfTextInput;
        }
        private Grid PasswordInput(AuthDataUser authData)
        {
            Entry entry = new Entry()
            {
                IsPassword = true,
            };
            entry.SetBinding(Entry.TextProperty, new Binding(nameof(authData.Password)));
            SfTextInputLayout sfTextInput = new SfTextInputLayout
            {
                ContainerType = ContainerType.Outlined,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Hint = "Password",
                InputView = entry,
                ErrorColor = Color.Red
            };
            sfTextInput.SetBinding(SfTextInputLayout.ErrorTextProperty, new Binding(nameof(authData.PasswordErrorText)));
            sfTextInput.SetBinding(SfTextInputLayout.HasErrorProperty, new Binding(nameof(authData.HasPassword)));

            Stream json = new MemoryStream(Properties.Resources.eye);

            AnimationView animationView = new AnimationView()
            {
                Margin = new Thickness(0, 0, 5, 1),
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 25,
                HeightRequest = 25,
                RepeatMode = RepeatMode.Infinite,
                AutoPlay = true,
                Speed = 0.5f,
                Animation = json,
                AnimationSource = AnimationSource.Stream,

            };
            animationView.Command = new Command(() =>
            {
                animationView.PlayAnimation();
                entry.IsPassword = !entry.IsPassword;
            });


            return new Grid()
            {
                Children = {
                    sfTextInput,
                    animationView

                }
            };
        }

    }

}
