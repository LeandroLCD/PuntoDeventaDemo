using Lottie.Forms;
using PuntoDeventa.UI.Sales.Models;
using System.IO;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.Sales.Screen
{
    internal class DocumentTypeScreen : ContentView
    {
        public DocumentTypeScreen(ICommand actionCommand = null)
        {
            Content = LoadContent(actionCommand);
        }

        private View LoadContent(ICommand actionCommand)
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

            var animationView = AnimationPay();

            var documentType = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                Margin = 20,
                Children =
                {
                    new Button()
                    {
                        Text = "Factura",
                        Style = styleButtons,
                        Command = actionCommand,
                        CommandParameter = DocumentType.Factura

                    },
                    new Button()
                    {
                        Text = "Pedido",
                        Style = styleButtons,
                        Command = actionCommand,
                        CommandParameter = DocumentType.NotaDePedido
                    }
                }
            };
            return new Grid()
            {
                Children = { animationView, documentType }
            };
        }

        private AnimationView AnimationPay()
        {
            Stream json = new MemoryStream(Properties.Resources.document_type);
            return new AnimationView
            {
                Margin = new Thickness(30),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                WidthRequest = 220,
                HeightRequest = 200,
                RepeatMode = RepeatMode.Infinite,
                AutoPlay = true,
                Animation = json,
                AnimationSource = AnimationSource.Stream

            };
        }
    }
}
