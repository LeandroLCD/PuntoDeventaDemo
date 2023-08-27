using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace PuntoDeVenta.IU.Controls
{
    public class CustomActivity : ContentView
    {
        #region Field
        private Grid _gridParen;
        private Image _imageIcon;
        private Image _activity;
        private Label _labelMessage;
        private PancakeView _pancakeView;
        #endregion

        #region Ctor
        public CustomActivity()
        {
            IsVisible = false;
            Content = ProtConten();
        }
        #endregion

        #region Properties
        public static readonly BindableProperty IsRunningProperty =
           BindableProperty.Create(nameof(IsRunning), typeof(bool),
               typeof(CustomActivity), false, BindingMode.TwoWay, null, propertyChanged: OnIsRunningChanged);
        public bool IsRunning
        {
            get => (bool)GetValue(IsRunningProperty);
            set => SetValue(IsRunningProperty, value);
        }
        private static void OnIsRunningChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null)
            {
                var customActivity = (CustomActivity)bindable;
                customActivity.IsVisible = (bool)newValue;
                // customActivity._activityIndicator.IsRunning = (bool)newValue;
                customActivity.AnimateText((bool)newValue);
            }
        }
        public static readonly BindableProperty IconProperty =
          BindableProperty.Create(nameof(Icon), typeof(ImageSource),
              typeof(CustomActivity), null, BindingMode.TwoWay, null, propertyChanged: OnIconChanged);
        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        private static void OnIconChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null)
            {
                var customActivity = (CustomActivity)bindable;
                customActivity._imageIcon.Source = (ImageSource)newValue;
            }
        }
        public static readonly BindableProperty SourceProperty =
          BindableProperty.Create(nameof(Source), typeof(ImageSource),
              typeof(CustomActivity), null, BindingMode.TwoWay, null, propertyChanged: OnSourceChanged);
        public ImageSource Source
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }
        private static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null)
            {
                var customActivity = (CustomActivity)bindable;
                customActivity._activity.Source = (ImageSource)newValue;
            }
        }

        public static readonly BindableProperty TextProperty =
          BindableProperty.Create(nameof(Text), typeof(string),
              typeof(CustomActivity), "Cargando...", BindingMode.TwoWay, null, propertyChanged: OnTextChanged);
        public string Text
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null)
            {
                var customActivity = (CustomActivity)bindable;
                customActivity._labelMessage.Text = (string)newValue;
            }
        }

        #endregion

        #region Methods
        private Grid ProtConten()
        {

            _gridParen = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent
            };

            _pancakeView = new PancakeView()
            {
                Border = new Border()
                {
                    Color = Color.Black,
                    Thickness = 2
                },
                BackgroundColor = Color.Black,
                CornerRadius = new CornerRadius(15),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 150,
                HeightRequest = 150,
                Opacity = 0.5
            };

            BoxView boxView = new BoxView()
            {
                BackgroundColor = Color.Black,
                Opacity = 0.6,
            };

            Grid grid = new Grid()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent
            };

            _labelMessage = new Label()
            {
                TextColor = Color.White,
                FontSize = 10,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.WordWrap,
                Margin = new Thickness(0, 120, 0, 0)
            };
            _labelMessage.SetBinding(Label.WidthRequestProperty, nameof(_pancakeView.WidthRequest));

            _activity = new Image()
            {
                WidthRequest = 70,

            };

            _imageIcon = new Image()
            {
                WidthRequest = 40,
                Margin = 10
            };
            grid.Children.Add(_activity);
            grid.Children.Add(_imageIcon);
            grid.Children.Add(_labelMessage);

            _gridParen.Children.Add(boxView);
            _gridParen.Children.Add(_pancakeView);
            _gridParen.Children.Add(_labelMessage);
            _gridParen.Children.Add(grid);

            return _gridParen;
        }

        private void AnimateText(bool state)
        {
            if (state)
            {
                var parentAnimation = new Animation();
                var scaleUpAnimation = new Animation(v => _activity.Scale = v, 0.8, 1, Easing.Linear);
                var rotateAnimation = new Animation(v => _activity.Rotation = v, 0, 360, Easing.Linear);
                var scaleDownAnimation = new Animation(v => _activity.Scale = v, 1, 0.8, Easing.Linear);
                parentAnimation.Add(0, 0.5, scaleUpAnimation);
                parentAnimation.Add(0, 1, rotateAnimation);
                parentAnimation.Add(0.5, 1, scaleDownAnimation);

                parentAnimation.Commit(this, "ChildAnimations", 16, 2000, Easing.Linear, null, () => true);
            }
            else
            {
                this.AbortAnimation("ChildAnimations");
                //this.AbortAnimation("TextTranslate");
            }

        }
        #endregion
    }
}
