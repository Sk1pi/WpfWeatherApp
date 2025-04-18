using System.Collections.Generic;
using System.Threading.Tasks;
using GeoCoder;
using GeoCoder.Models;
using MyWeatherAppp.Services;
using MyWeatherAppp.Mvvm;
using System.Collections.ObjectModel;

namespace MyWeatherAppp.ViewModels
{
    class LocationsViewModel: Base.BaseViewModel
    {
        public RelayCommand SelectLocationCommand { get; }
        public RelayCommand DeleteLocationCommand { get; }

        public LocationsViewModel()
        {
            Title = App.Current.Resources["LocationsPageTitle"].ToString();
            SelectLocationCommand = new RelayCommand((location) => SelectedLocation = location as GeoLocationsModels);
            DeleteLocationCommand = new RelayCommand((location) =>
            {
                if (FavouritLocations.Count > 1)
                    FavouritLocations.Remove(location as GeoLocationsModels);
            });
        }

        private string _locationSearch;

        public string LocationSearch
        {
            get => _locationSearch;
            set
            {
                Set(ref _locationSearch, value, nameof(LocationSearch));
                if (string.IsNullOrEmpty(value))
                {
                    SearchResults = null!;
                    return;
                }
                Task.Run(async () => {
                    string search = value;
                    await Task.Delay(700);
                    if (search != _locationSearch) return;
                    SearchResults = await GeoCoderService.GetPositionAsync(value);
                });
            }
        }

        private List<GeoLocationsModels> _searchResults;

        public List<GeoLocationsModels> SearchResults
        {
            get => _searchResults;
            set => Set(ref _searchResults, value, nameof(SearchResults));
        }

        public ObservableCollection<GeoLocationsModels> FavouritLocations => SettingsService.Configuration.FavouritLocations;

        public GeoLocationsModels SelectedLocation
        {
            get => SettingsService.Configuration.SelectedLocation;
            set
            {
                SearchResults = null!;
                if (value is not null)
                {
                    SettingsService.Configuration.SelectedLocation = value;
                    if (!FavouritLocations.Contains(value)) FavouritLocations.Add(value);
                }

                OnPropertyChanged(nameof(SelectedLocation));
            }
        }
    }
}
