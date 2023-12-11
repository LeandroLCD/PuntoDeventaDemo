using Lottie.Forms;
using PuntoDeventa.Domain.Helpers;
using Syncfusion.SfNumericTextBox.XForms;
using Syncfusion.XForms.Editors;
using Syncfusion.XForms.TextInputLayout;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.Sales.Screen
{
    internal class PaymentListScreen : ContentView
    {
        private readonly ICommand _commandCash;
        private readonly ICommand _commandCredit;
        private readonly ICommand _castCommand;
        private readonly ICommand _checkCommand;
        private readonly ICommand _depositCommand;
        private readonly ICommand _transferCommand;
        private Grid _gridParen;

        public PaymentListScreen(ICommand commandCash = null,
                                 ICommand commandCredit = null,
                                 ICommand castCommand = null,
                                 ICommand checkCommand = null,
                                 ICommand depositCommand = null,
                                 ICommand transferCommand = null)
        {
            _commandCash = commandCash;
            _commandCredit = commandCredit;
            _castCommand = castCommand;
            _checkCommand = checkCommand;
            _depositCommand = depositCommand;
            _transferCommand = transferCommand;

            Content = LoadContent();
        }

        private AnimationView AnimationPay()
        {
            Stream json = new MemoryStream(Properties.Resources.paymethods);
            return new AnimationView
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                WidthRequest = 300,
                HeightRequest = 300,
                RepeatMode = RepeatMode.Infinite,
                AutoPlay = true,
                Animation = json,
                AnimationSource = AnimationSource.Stream

            };
        }

        private View LoadContent()
        {
            var styleButtons = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter
                    {
                        Property = Button.TextTransformProperty, Value = TextTransform.None
                    },
                    new Setter
                    {
                        Property = Button.FontAttributesProperty, Value = FontAttributes.Bold
                    },
                    new Setter
                    {
                        Property = Button.TextColorProperty, Value = Color.FromRgb(64, 56, 59)
                    },
                    new Setter
                    {
                        Property = Button.FontSizeProperty, Value = 20
                    }
                }
            };

            var pay = AnimationPay();

            var paymentMethods = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                Margin = 20,
                Children =
                {
                    new Button()
                    {
                        Text = "Efectivo",
                        Style = styleButtons,
                        Command = paymentCash()

                    },
                    new Button()
                    {
                        Text = "Crédito",
                        Style = styleButtons,
                        Command = paymentCredit()
                    }
                }
            };

            _gridParen = new Grid()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { pay, paymentMethods }
            };
            return _gridParen;
        }

        private ICommand paymentCredit()
        {
            return new Command(() =>
            {
                LoadPaymentList(_commandCredit);
            });
        }

        private ICommand paymentCash()
        {
            return new Command(() =>
            {
                LoadPaymentList(_commandCash);
            });
        }

        private void LoadPaymentList(ICommand command)
        {
            View aniView = _gridParen.Children.FirstOrDefault(p => p.GetType() == typeof(AnimationView));
            aniView?.Apply(() =>
                {
                    var scaleAnimation = new Animation(s =>
                    {
                        aniView.Scale = s;
                    }, 1, 0.5, Easing.SinInOut, (delegate { ((AnimationView)aniView).Animation = new MemoryStream(Properties.Resources.cash); }));

                    var translationAnimation = new Animation(t =>
                    {
                        aniView.TranslationY = t;
                    }, 0, -20);

                    var rotationAnimation = new Animation(r =>
                    {
                        aniView.Rotation = r;
                    }, 0, 90);


                    var combinedScaleTranslationAnimation = new Animation
                    {
                        { 0, 0.5, scaleAnimation },
                        { 0.5, 1, translationAnimation }
                    };

                    var finalAnimation = new Animation
                    {
                        { 0, 1, combinedScaleTranslationAnimation },
                        { 0, 1, rotationAnimation }
                    };

                    finalAnimation.Commit(aniView, "FinalAnimation", length: 2000, easing: Easing.Linear);

                });
            var buttonView = _gridParen.Children.FirstOrDefault(p => p.GetType() == typeof(StackLayout));
            buttonView?.Apply(() =>
            {
                _gridParen.Children.Remove(buttonView);
            });

            _gridParen.Children.Add(Payment(command));
        }

        private View Payment(ICommand commandActions)
        {


            var stackLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 50, 20, 10),
                Children =
                {
                    EntryPayment("Efectivo", _castCommand),
                    EntryPayment("Cheque", _checkCommand),
                    EntryPayment("Transferencia",_transferCommand),
                    EntryPayment("Deposito", _depositCommand),
                    new BoxView(){ VerticalOptions = LayoutOptions.FillAndExpand},
                    EntryPaymentTotal(),

                }
            };

            var buttonPay = new Button()
            {
                Margin = new Thickness(10),
                Text = "Pagar",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                Command = commandActions
            };
            var gridContainer = new Grid()
            {
                Children = { stackLayout, buttonPay }
            };


            return new ScrollView()
            {
                Content = gridContainer
            };
        }

        private SfTextInputLayout EntryPayment(string title, ICommand paymentCommand)
        {
            var numericTextBox = new SfNumericTextBox
            {
                FormatString = "c",
                ClearButtonVisibility = ClearButtonVisibilityMode.WhileEditing,
                EnableGroupSeparator = true,
                AllowDefaultDecimalDigits = true,
                SelectAllOnFocus = true,

            };
            numericTextBox.Unfocused += (sender, e) =>
            {
                var value = ((SfNumericTextBox)sender).Value.ToString();
                var parameter = int.TryParse(value, out var result);

                if (parameter && paymentCommand.CanExecute(result))
                {
                    paymentCommand.Execute(result);
                }
            };
            //numericTextBox.ReturnCommand = paymentCommand;
            //numericTextBox.SetBinding(SfNumericTextBox.ReturnCommandParameterProperty ,new Binding("Value", source: numericTextBox));

            return new SfTextInputLayout()
            {
                Hint = title,
                ContainerType = ContainerType.Outlined,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                InputView = numericTextBox
            };


        }



        private SfTextInputLayout EntryPaymentTotal()
        {
            var numericTextBox = new SfNumericTextBox
            {
                Margin = new Thickness(0, 20),
                FormatString = "c",
                ClearButtonVisibility = ClearButtonVisibilityMode.WhileEditing,
                EnableGroupSeparator = true,
                AllowDefaultDecimalDigits = true,
                SelectAllOnFocus = true,
                IsReadOnly = true,
            };

            numericTextBox.SetBinding(SfNumericTextBox.ValueProperty, new Binding("TotalPay"));

            return new SfTextInputLayout()
            {
                Hint = "Total",
                ContainerType = ContainerType.Outlined,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                InputView = numericTextBox
            };

        }


    }
}
