using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.Controls.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public Color TrueColor = Color.FromArgb("F57E7E");

        public Color FalseColor = Color.FromArgb("F5F5F5");
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? TrueColor : FalseColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Color)value).Equals(TrueColor) ? true : false;
        }
    }
}
