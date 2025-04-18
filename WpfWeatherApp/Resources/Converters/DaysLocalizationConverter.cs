using System;
using System.Globalization;
using System.Windows.Data;

namespace MyWeatherAppp.Resources.Converters
{
    public class DaysLocalizationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            if (value is DateTime date)
            {
                if (parameter == null) 
                    return App.Current.Resources[date.Date.DayOfWeek.ToString()];
                return App.Current.Resources[date.Date.DayOfWeek.ToString() + "Short"];
                 
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
