using System;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.Controls
{
    public sealed class StandardEntry : Entry
    {
        public static BindableProperty CornerRadiusProperty =
           BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(StandardEntry), 0);

        public static BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(StandardEntry), 0);

        public static BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(StandardEntry), new Thickness(5));

        public static BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(StandardEntry), Colors.Transparent);

        public static BindableProperty BorderColorFocusProperty =
            BindableProperty.Create(nameof(BorderColorFocus), typeof(Color), typeof(StandardEntry), Colors.Transparent);

        public static BindableProperty IsVisibleKeyboardProperty =
            BindableProperty.Create(nameof(IsVisibleKeyboard), typeof(Boolean), typeof(StandardEntry), false);


        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        public Color BorderColor //BorderColorFocus
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        public Color BorderColorFocus
        {
            get => (Color)GetValue(BorderColorFocusProperty);
            set => SetValue(BorderColorFocusProperty, value);
        }

        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public Boolean IsVisibleKeyboard
        {
            get => (Boolean)GetValue(IsVisibleKeyboardProperty);
            set => SetValue(IsVisibleKeyboardProperty, value);
        }
    }
}
