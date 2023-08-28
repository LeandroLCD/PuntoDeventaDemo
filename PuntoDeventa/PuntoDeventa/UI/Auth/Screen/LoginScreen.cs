using Lottie.Forms;
using PuntoDeventa.UI.Auth.Models;
using Syncfusion.XForms.TextInputLayout;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace PuntoDeVenta.IU.Auth.Screen
{
    public class LoginScreen : ContentView
    {
        bool isPassword;
        private AuthDataUser _dataUser;

        public LoginScreen(AuthDataUser dataUser, bool IsRememberme, Command LoginCommand, Command RecoveryCommand)
        {
            _dataUser = dataUser;
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





            mainGrid.Children.Add(PancakeContent(dataUser, IsRememberme, LoginCommand, RecoveryCommand), 0, 1, 1, 3);

            Image iconImage = new Image
            {
                Source = "icon.png",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            mainGrid.Children.Add(iconImage, 0, 1, 0, 2);

            Content = mainGrid;
        }
        private PancakeView PancakeContent(AuthDataUser dataUser, bool isRememberme, Command loginCommand, Command recoveryCommand)
        {


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

            gridLayout.Children.Add(EntryInput(), 0, 0);

            gridLayout.Children.Add(PasswordInput(isPassword), 0, 1);

            gridLayout.Children.Add(Rememberme(isRememberme), 0, 2);

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
            var button = new Button()
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

            button.SetBinding(Button.CommandParameterProperty, new Binding(nameof(_dataUser)));

            return button;
        }

        private StackLayout Rememberme(bool IsRememberme)
        {
            var rememberme = new CheckBox();
            rememberme.SetBinding(CheckBox.IsCheckedProperty, nameof(IsRememberme));

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

        private SfTextInputLayout EntryInput(bool isPassword = false)
        {
            Entry entry = new Entry()
            {
                IsPassword = isPassword,
            };

            entry.SetBinding(Entry.TextProperty, new Binding(nameof(_dataUser.Email)));

            SfTextInputLayout sfTextInput = new SfTextInputLayout
            {
                ContainerType = ContainerType.Outlined,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Hint = nameof(_dataUser.Email),
                InputView = entry,
                ErrorColor = Color.Red
            };
            sfTextInput.SetBinding(SfTextInputLayout.ErrorTextProperty, new Binding(nameof(_dataUser.EmailErrorText)));
            sfTextInput.SetBinding(SfTextInputLayout.HasErrorProperty, new Binding(nameof(_dataUser.HasEmail)));
            return sfTextInput;
        }
        private Grid PasswordInput( bool isPassword = false)
        {
            Entry entry = new Entry()
            {
                IsPassword = isPassword,
            };
            entry.SetBinding(Entry.TextProperty, new Binding(nameof(_dataUser.Password)));
            SfTextInputLayout sfTextInput = new SfTextInputLayout
            {
                ContainerType = ContainerType.Outlined,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Hint = nameof(_dataUser.Password),
                InputView = entry,
                ErrorColor = Color.Red
            };
            sfTextInput.SetBinding(SfTextInputLayout.ErrorTextProperty, new Binding(nameof(_dataUser.PasswordErrorText)));
            sfTextInput.SetBinding(SfTextInputLayout.HasErrorProperty, new Binding(nameof(_dataUser.HasPassword)));

            Stream json = new MemoryStream(PuntoDeventa.Properties.Resources.animation_eye);

            AnimationView animationView = new AnimationView()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 30,
                HeightRequest = 30,
                RepeatMode = RepeatMode.Restart,
                AutoPlay = false,
                Speed = 0.5f,
                Animation = json,
                AnimationSource = AnimationSource.Stream

            };
            animationView.Command = new Command(() =>
            {
                animationView.PlayAnimation();
                isPassword = !isPassword;
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
