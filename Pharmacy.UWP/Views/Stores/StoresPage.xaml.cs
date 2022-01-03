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
        Domain.Models.Stores selectedStore;

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
            List<object> data = new List<object>();
            data.Add(selectedStore);
            data.Add(AddButtonText.Text);

            this.Frame.Navigate(typeof(ManageStoresPage), data);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedStore != null)
            {
                List<object> data = new List<object>();
                data.Add(selectedStore);
                data.Add(EditButtonText.Text);

                this.Frame.Navigate(typeof(ManageStoresPage), data);
            }    
        }

        private void StoresListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedStore = (Domain.Models.Stores)StoresListView.SelectedItem;

            StoresViewModel.Id = selectedStore.Id;
            StoresViewModel.Name = selectedStore.Name;
            StoresViewModel.Country = selectedStore.Country;
            StoresViewModel.City = selectedStore.City;
            StoresViewModel.Street = selectedStore.Street;
            StoresViewModel.Telephone = selectedStore.Telephone;
            StoresViewModel.OpeningHours = selectedStore.OpeningHours;            
        }

        private async void DeleteConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await StoresViewModel.DeleteStoreAsync();
                Frame.Navigate(this.GetType());
            }
            catch
            {

            }
        }
    }
}
