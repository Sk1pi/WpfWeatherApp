using System.Windows;
using WeatherProvider;
using MyWeatherAppp.Services;

namespace MyWeatherAppp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            WeatherService.WeatherApiProvider();
            WeatherService.MeasureConfiguration.WindSpeedUnit = SettingsService.Configuration.Wind;
            WeatherService.MeasureConfiguration.PressureUnit = SettingsService.Configuration.Pressure;
            WeatherService.MeasureConfiguration.PrecipitationUnit = SettingsService.Configuration.Lenght;
            WeatherService.MeasureConfiguration.TemperatureUnit = SettingsService.Configuration.Temperature;
            ServiceManager.InternetConnectionService.HostName = WeatherService.ProviderDomain;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            SettingsService.SaveChanges();
            base.OnExit(e);
        }
    }
}
