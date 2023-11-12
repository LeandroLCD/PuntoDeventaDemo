using Syncfusion.SfPdfViewer.XForms;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace PuntoDeventa.UI.Sales.Screen
{
    internal class PdfViewScreen : ContentView
    {
        private readonly Stream _pdfStream;
        private readonly ICommand _commandActions;
        private readonly string _titleAction;

        public PdfViewScreen(Stream pdfStream, ICommand commandActions = null, string titleAction = "Aceptar")
        {
            _pdfStream = pdfStream;
            _commandActions = commandActions;
            _titleAction = titleAction;
            Content = LoadContent();
        }

        private View LoadContent()
        {
            var pdfViewer = new SfPdfViewer()
            {
                ShowPageNumber = false,
                ShowPageFlipNavigationArrows = false,
                IsPageFlipEnabled = false,
                IsToolbarVisible = false,
                InputFileStream = _pdfStream
            };

            var buttonBar = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Orientation = StackOrientation.Horizontal,
                HeightRequest = 60
            };

            return new Grid()
            {
                Children = { pdfViewer, new ImageButton()
                    {
                        Source = "ic_share.png",
                        BackgroundColor = Color.Blue,
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.End,
                        Scale = 1.2,
                        HeightRequest = 40,
                        WidthRequest = 40,
                        CornerRadius = 25,
                        Margin = new Thickness(10),

                    },
                    new Button()
                    {
                        Text = _titleAction,
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,

                        Margin = new Thickness(10),
                        Command = _commandActions
                    }

                }
            };


        }


    }
}
