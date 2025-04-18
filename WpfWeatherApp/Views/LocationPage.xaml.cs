using System.Windows.Controls;
using System.Windows.Input;

namespace MyWeatherAppp.Views
{
    /// <summary>
    /// Логика взаимодействия для LocationPage.xaml
    /// </summary>
    public partial class LocationPage: Page
    {
        public LocationPage()
        {
            InitializeComponent();
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }
    }
}
