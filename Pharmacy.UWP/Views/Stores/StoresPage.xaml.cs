using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Pharmacy.UWP.ViewModels.StoresViewModel;

namespace Pharmacy.UWP.Views.Stores
{
    public sealed partial class StoresPage : Page
    {
        public StoresViewModel StoresViewModel { get; set; }

        public StoresPage()
        {
            this.InitializeComponent();
            StoresViewModel = new StoresViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            StoresViewModel.LoadAllAsync();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ManageStoresPage), AddButton.Label as string);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ManageStoresPage), EditButton.Label as string);
        }
    }
}
