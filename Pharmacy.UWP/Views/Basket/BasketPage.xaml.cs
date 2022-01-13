using Pharmacy.UWP.ViewModels.BasketViewModel;
using Pharmacy.UWP.ViewModels.MedicineViewModel;
using Pharmacy.UWP.Views.Medicine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Pharmacy.UWP.Views.Basket
{
    public sealed partial class BasketPage : Page
    {
        public BasketViewModel BasketViewModel { get; set; }
        public MedicineViewModel MedicineViewModel { get; set; }

        object data;

        Domain.Models.Baskets selectedMedicine;
        Domain.Models.Users authorisedUser;

        public BasketPage()
        {
            this.InitializeComponent();
            BasketViewModel = new BasketViewModel();
            MedicineViewModel = new MedicineViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            data = e.Parameter;
            authorisedUser = (Domain.Models.Users)data;

            await Task.Delay(50);
            await BasketViewModel.LoadAllAsync();             
        }

        private void BasketListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedMedicine = (Domain.Models.Baskets)BasketListView.SelectedItem;

            BasketViewModel.UserID = authorisedUser.Id;
            BasketViewModel.MedicineID = selectedMedicine.Id;

            BasketViewModel.Quantity = selectedMedicine.Quantity;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> data = new List<object>();
            data.Add(authorisedUser);
            data.Add("Basket");

            this.Frame.Navigate(typeof(MedicinePage), data);
        }

        private async void DeleteConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await BasketViewModel.DeleteBasketAsync();
                Frame.Navigate(this.GetType(), data);
            }
            catch
            {

            }
        }

        private void SummaryConfirmationButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
