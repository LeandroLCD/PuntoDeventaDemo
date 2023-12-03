using System;
using System.Globalization;
using Xamarin.Forms;

namespace PuntoDeventa.UI.Controls.Converters
{
    public class ResizeDp : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double dimensionValue)
            {
                var scaleFactor = double.Parse(parameter.ToString());
                return dimensionValue > 0 ? dimensionValue * scaleFactor : 0;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
