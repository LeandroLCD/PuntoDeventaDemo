using System;
using System.Globalization;
using Xamarin.Forms;

namespace PuntoDeventa.UI.Controls.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public Color TrueColor = Color.FromHex("F57E7E");

        public Color FalseColor = Color.FromHex("F5F5F5");
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
