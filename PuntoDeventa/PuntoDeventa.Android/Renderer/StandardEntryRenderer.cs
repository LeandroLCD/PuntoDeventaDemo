using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views.InputMethods;
using PuntoDeventa.Droid.Renderer;
using PuntoDeventa.UI.Controls;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(StandardEntry), typeof(StandardEntryRenderer))]
namespace PuntoDeventa.Droid.Renderer
{
    public class StandardEntryRenderer : EntryRenderer
    {
        private readonly Context _context;
        public StandardEntryRenderer(Context context) : base(context)
        {
            _context = context;
        }

        public StandardEntry ElementV2 => Element as StandardEntry;
        protected override FormsEditText CreateNativeControl()
        {

            var control = base.CreateNativeControl();
            UpdateBackground(control);
            control.Focusable = false;
            control.FocusableInTouchMode = false;
            return control;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == StandardEntry.CornerRadiusProperty.PropertyName)
            {
                UpdateBackground(Control);
            }
            else if (e.PropertyName == StandardEntry.BorderThicknessProperty.PropertyName)
            {
                UpdateBackground(Control);
            }
            else if (e.PropertyName == StandardEntry.BorderColorProperty.PropertyName)
            {
                UpdateBackground(Control);
            }
            else if (e.PropertyName == StandardEntry.IsVisibleKeyboardProperty.PropertyName)
            {
                UpdateKeyboard(Control);

            }

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void UpdateBackgroundColor()
        {
            UpdateBackground(Control);


        }
        protected void UpdateBackground(FormsEditText control)
        {
            if (control == null) return;

            var gd = new GradientDrawable();
            gd.SetColor(Element.BackgroundColor.ToAndroid());
            gd.SetCornerRadius(Context.ToPixels(ElementV2.CornerRadius));
            gd.SetStroke((int)Context.ToPixels(ElementV2.BorderThickness), control.IsFocused ? ElementV2.BorderColorFocus.ToAndroid() : ElementV2.BorderColor.ToAndroid());
            control.SetBackground(gd);
            var padTop = (int)Context.ToPixels(ElementV2.Padding.Top);
            var padBottom = (int)Context.ToPixels(ElementV2.Padding.Bottom);
            var padLeft = (int)Context.ToPixels(ElementV2.Padding.Left);
            var padRight = (int)Context.ToPixels(ElementV2.Padding.Right);

            control.SetPadding(padLeft, padTop, padRight, padBottom);
        }
        protected void UpdateKeyboard(FormsEditText control)
        {


            if (control == null) return;

            var inputMethodManager = (InputMethodManager)_context.GetSystemService(Context.InputMethodService);
            if (inputMethodManager != null && _context is Activity)
            {

                if (!ElementV2.IsVisibleKeyboard)
                {
                    var activity = _context as Activity;

                    var token = activity.CurrentFocus?.WindowToken;
                    inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                    this.EditText.InputType = Android.Text.InputTypes.Null;
                }
                else
                {

                    this.EditText.InputType = ElementV2.Keyboard.ToInputType();
                }
            }
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            this.EditText.TextChanged += EditText_TextChanged;
            this.EditText.FocusChange += EditText_FocusChange;
            //this.EditText.Touch += EditText_Touch;
            //this.EditText.Click += EditText_Click;



        }

        private void EditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            UpdateKeyboard(Control);

        }

        private void EditText_Click(object sender, System.EventArgs e)
        {
            UpdateKeyboard(Control);
        }

        private void EditText_Touch(object sender, TouchEventArgs e)
        {
            UpdateKeyboard(Control);
        }



        private void EditText_FocusChange(object sender, System.EventArgs e)
        {
            UpdateKeyboard(Control);
            UpdateBackground(Control);
        }



    }


}