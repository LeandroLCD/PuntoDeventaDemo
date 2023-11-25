﻿using Lottie.Forms;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.UI.Sales.Models;
using Syncfusion.SfNumericTextBox.XForms;
using Syncfusion.XForms.Editors;
using Syncfusion.XForms.TextInputLayout;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using static PuntoDeventa.UI.Sales.Models.PaymentType;

namespace PuntoDeventa.UI.Sales.Screen
{
    internal class PaymentListScreen : ContentView
    {
        private readonly ICommand _commandCash;
        private readonly ICommand _commandCredit;
        private Grid _gridParen;

        public PaymentListScreen(ICommand commandCash = null,
            ICommand commandCredit = null,
            ICommand changedPaymentCommand = null)
        {
            _commandCash = commandCash;
            _commandCredit = commandCredit;

            Content = LoadContent(changedPaymentCommand);
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

        private View LoadContent(ICommand changedPayment)
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
                        Command = paymentCash(changedPayment)

                    },
                    new Button()
                    {
                        //BackgroundColor = Color.FromRgb(39, 89, 148),
                        Text = "Crédito",
                        Style = styleButtons,
                        Command = paymentCredit(changedPayment)
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

        private ICommand paymentCredit(ICommand changedPayment)
        {
            return new Command(() =>
            {
                LoadPaymentList(_commandCredit, changedPayment);
            });
        }

        private ICommand paymentCash(ICommand changedPayment)
        {
            return new Command(() =>
            {
                LoadPaymentList(_commandCash, changedPayment);
            });
        }

        private void LoadPaymentList(ICommand commandCast, ICommand changedPayment)
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

            _gridParen.Children.Add(Payment(commandCast, changedPayment));
        }

        private View Payment(ICommand commandActions, ICommand changedPayment)
        {


            var stackLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20),
                Children =
                {
                    EntryPayment("Efectivo", Cash, changedPayment),
                    EntryPayment("Cheque", BankCheck,changedPayment),
                    EntryPayment("Transferencia",BankTransfer, changedPayment),
                    EntryPayment("Deposito", BankDeposit,changedPayment),
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

        private SfTextInputLayout EntryPayment(string title, PaymentType paymentType, ICommand changedPayment)
        {
            var numericTextBox = new SfNumericTextBox
            {
                FormatString = "c",
                ClearButtonVisibility = ClearButtonVisibilityMode.WhileEditing,
                EnableGroupSeparator = true,
                AllowDefaultDecimalDigits = true,
                SelectAllOnFocus = true

            };
            numericTextBox.ReturnCommand = changedPayment;
            numericTextBox.ReturnCommandParameter = new KeyValue<string, double>
            {
                Key = nameof(paymentType),
                Value = (double)numericTextBox.Value
            };

            return new SfTextInputLayout()
            {
                Hint = title,
                ContainerType = ContainerType.Outlined,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                InputView = numericTextBox
            };


        }

        private void ValueChange(ICommand changedPayment, PaymentType paymentType, double eValue)
        {
            changedPayment.Execute(new KeyValue<string, double>
            {
                Key = nameof(paymentType),
                Value = (double)eValue
            });
        }

        private SfTextInputLayout EntryPaymentTotal(IEnumerable<Payment> paymentTypes)
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
                Value = paymentTypes.Sum(p => p.Amount)
            };
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