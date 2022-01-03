using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Pharmacy.UWP.ViewModels.StoresViewModel;
using System.Collections.Generic;

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
            this.Frame.Navigate(typeof(ManageStoresPage), EditButton.Label);          
        }

        private async void StoresListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Domain.Models.Stores selectedStore = (Domain.Models.Stores)StoresListView.SelectedItem;
            
            StoresViewModel.Name = selectedStore.Name;
            StoresViewModel.Country = selectedStore.Country;
            StoresViewModel.City = selectedStore.City;
            StoresViewModel.Street = selectedStore.Street;
            StoresViewModel.Telephone = selectedStore.Telephone;
            StoresViewModel.OpeningHours = selectedStore.OpeningHours;

            await StoresViewModel.DeleteStoreAsync();
        }
      
    }
}
