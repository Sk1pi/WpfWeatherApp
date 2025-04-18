using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyWeatherAppp.Mvvm
{
    internal class ObservableObject: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}