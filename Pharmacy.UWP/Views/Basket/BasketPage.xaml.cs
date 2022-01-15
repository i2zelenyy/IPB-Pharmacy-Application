using Pharmacy.UWP.ViewModels.BasketViewModel;
using Pharmacy.UWP.ViewModels.MedicineViewModel;
using Pharmacy.UWP.ViewModels.ChequesViewModel;
using Pharmacy.UWP.Views.Cheques;
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
        public ChequesViewModel ChequesViewModel { get; set; }

        object data;

        Domain.Models.Baskets selectedItem;
        Domain.Models.Users authorisedUser;

        public BasketPage()
        {
            this.InitializeComponent();
            BasketViewModel = new BasketViewModel();
            MedicineViewModel = new MedicineViewModel();
            ChequesViewModel = new ChequesViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            data = e.Parameter;
            authorisedUser = (Domain.Models.Users)data;

            await Task.Delay(50);
            await BasketViewModel.LoadBasketAsync(authorisedUser.Id);
            await ChequesViewModel.LoadAllAsync();

            TotalTextBlock.Text = TotalCount() + " €";


            foreach (var cheque in ChequesViewModel.Cheques)
            {
                var temp = (Domain.Models.Cheques)cheque;

                foreach (var basket in BasketViewModel.Baskets)
                {
                    if (temp.BasketsID == basket.Id)
                    {
                        DeleteButton.IsEnabled = false;
                    }
                }
            }
        }

        public string TotalCount()
        {
            ToPriceConverter toPriceConverter = new ToPriceConverter();
            float result = 0;

            foreach ( var basket in BasketViewModel.Baskets)
            {
                var temp = (Domain.Models.Baskets) basket;

                int quantity = temp.Quantity;
                float price = (float) toPriceConverter.ConvertToPrice(temp.MedicineID);
                result += quantity * price;
            }

            return result.ToString();
        }

        private void BasketListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = (Domain.Models.Baskets)BasketListView.SelectedItem;

            BasketViewModel.Id = selectedItem.Id;
            BasketViewModel.UserID = authorisedUser.Id;
            BasketViewModel.MedicineID = selectedItem.MedicineID;

            BasketViewModel.Quantity = selectedItem.Quantity;
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
            List<object> data = new List<object>();
            data.Add(authorisedUser);
            data.Add("");

            Frame.Navigate(typeof(ChequesPage), data);
        }

        private async void AddQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItem != null)
            {
                BasketViewModel.Quantity += 1;
                await BasketViewModel.UpdateBasketAsync();
                Frame.Navigate(this.GetType(), data);
            }
        }

        private async void SubtractQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BasketViewModel.Quantity > 1)
                {
                    BasketViewModel.Quantity -= 1;
                    await BasketViewModel.UpdateBasketAsync();
                    Frame.Navigate(this.GetType(), data);
                }
            }
            catch
            {

            }

        }
    }
}
