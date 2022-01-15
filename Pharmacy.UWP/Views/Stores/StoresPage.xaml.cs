using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Pharmacy.UWP.ViewModels.StoresViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pharmacy.UWP.Views.Cheques;
using Pharmacy.UWP.ViewModels.ChequesViewModel;

namespace Pharmacy.UWP.Views.Stores
{
    public sealed partial class StoresPage : Page
    {
        List<object> data;
        public StoresViewModel StoresViewModel { get; set; }
        public ChequesViewModel ChequesViewModel { get; set; }

        Domain.Models.Users authorisedUser;
        Domain.Models.Stores selectedStore;
        bool _chequesMode = false;

        public StoresPage()
        {
            this.InitializeComponent();
            StoresViewModel = new StoresViewModel();
            ChequesViewModel = new ChequesViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            data = (List<object>) e.Parameter;
            authorisedUser = (Domain.Models.Users)data[0];

            await Task.Delay(50);
            await StoresViewModel.LoadAllAsync();
            await ChequesViewModel.LoadAllAsync();

            if (authorisedUser.IsAdmin == false)
            {
                AddButton.IsEnabled = false;
                EditButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
            else
            {
                foreach (var cheque in ChequesViewModel.Cheques)
                {
                    var temp = (Domain.Models.Cheques)cheque;

                    foreach (var store in StoresViewModel.Stores)
                    {
                        if (temp.StoresID == store.Id)
                        {
                            DeleteButton.IsEnabled = false;
                        }
                    }
                }
            }

            if (data[1] == "Cheques")
            {
                _chequesMode = true;

                AddButton.IsEnabled = true;
                EditButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> data = new List<object>();

            if (_chequesMode == true)
            {
                if (selectedStore != null)
                {                  
                    data.Add(authorisedUser);
                    data.Add(selectedStore);

                    this.Frame.Navigate(typeof(ChequesPage), data);
                }
            }
            else
            {
                data = new List<object>();
                data.Add(selectedStore);
                data.Add(AddButtonText.Text);

                this.Frame.Navigate(typeof(ManageStoresPage), data);
            }
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

        private async void DeleteConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await StoresViewModel.DeleteStoreAsync();

                List<object> data = new List<object>();
                data.Add(authorisedUser);
                data.Add("parameter1");

                Frame.Navigate(this.GetType(), data);
            }
            catch
            {

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

    }
}
