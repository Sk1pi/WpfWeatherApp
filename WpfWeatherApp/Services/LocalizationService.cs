using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace MyWeatherAppp.Services
{
    internal class LocalizationService
    {
        private Dictionary<Cultures, ResourceDictionary> _dictionary = new Dictionary<Cultures, ResourceDictionary>()
        {
            { Cultures.EN, new ResourceDictionary() { Source = new Uri("pack://application:,,,/Resources/Localizations/Strings.en.xaml", UriKind.RelativeOrAbsolute) } },
            { Cultures.UA, new ResourceDictionary() { Source = new Uri("pack://application:,,,/Resources/Localizations/Strings.ua.xaml", UriKind.RelativeOrAbsolute) } }
        };
        public Cultures? CurrentCulture { get; }

        public void SetCulture(Cultures culture)
        {
            if (CurrentCulture == culture) return;
            if (CurrentCulture is not null)
                App.Current.Resources.MergedDictionaries.Remove(_dictionary[CurrentCulture.Value]);
            App.Current.Resources.MergedDictionaries.Add(_dictionary[culture]);
            CultureInfo.CurrentCulture = new CultureInfo(App.Current.Resources["lang"].ToString());
        }
    }
    public enum Cultures
    {
        EN,
        UA
    }
}
