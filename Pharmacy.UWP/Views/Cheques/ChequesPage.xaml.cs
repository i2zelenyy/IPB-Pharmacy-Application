using Pharmacy.UWP.ViewModels.BasketViewModel;
using Pharmacy.UWP.ViewModels.ChequesViewModel;
using Pharmacy.UWP.ViewModels.MedicineViewModel;
using Pharmacy.UWP.Views.Stores;
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

namespace Pharmacy.UWP.Views.Cheques
{
    public sealed partial class ChequesPage : Page
    {
        List<object> data;
        public BasketViewModel BasketViewModel { get; set; }
        public MedicineViewModel MedicineViewModel { get; set; }
        public BasketsViewModel ChequesViewModel { get; set; }

        Domain.Models.Users authorisedUser;
        Domain.Models.Stores selectedStore;

        float totalSum;
        public ChequesPage()
        {
            this.InitializeComponent();
            BasketViewModel = new BasketViewModel();
            MedicineViewModel = new MedicineViewModel();
            ChequesViewModel = new BasketsViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            data = (List<object>)e.Parameter;
            authorisedUser = (Domain.Models.Users)data[0];

            if (data[1] != "")
            {
                selectedStore = (Domain.Models.Stores)data[1];
                StoreTextBlock.Text = selectedStore.Name;
            }
            

            await Task.Delay(50);
            await ChequesViewModel.LoadAllAsync();
            await BasketViewModel.LoadBasketAsync(authorisedUser.Id);

            totalSum = float.Parse(TotalCount());
            TotalTextBlock.Text = totalSum.ToString() + " €";
            
        }

        public string TotalCount()
        {
            ToPriceConverter toPriceConverter = new ToPriceConverter();
            float result = 0;

            foreach (var basket in BasketViewModel.Baskets)
            {
                var temp = (Domain.Models.Baskets)basket;

                int quantity = temp.Quantity;
                float price = (float)toPriceConverter.ConvertToPrice(temp.MedicineID);
                result += quantity * price;
            }

            return result.ToString();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void ChooseStoreButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> data = new List<object>();
            data.Add(authorisedUser);
            data.Add("Cheques");

            this.Frame.Navigate(typeof(StoresPage), data);
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (TotalTextBlock.Text != "0" + " €")
            {
                if (StoreTextBlock.Text != "")
                {
                    ChequesViewModel.StoresID = selectedStore.Id;
                    ChequesViewModel.TotalPrice = totalSum;
                    ChequesViewModel.Date = DateTime.Now.Date;
                    ChequesViewModel.Time = DateTime.Now.ToString("hh:mm:ss");

                    foreach (var basket in BasketViewModel.Baskets)
                    {
                        var temp = (Domain.Models.Baskets)basket;
                        ChequesViewModel.BasketsID = temp.Id;

                        await ChequesViewModel.CreateChequeAsync();
                    }

                    this.Frame.Navigate(typeof(ManageChequesPage), data[0]);

                    //foreach (var basket in BasketViewModel.Baskets)
                    //{
                    //    var temp = (Domain.Models.Baskets)basket;

                    //    BasketViewModel.Id = temp.Id;
                    //    BasketViewModel.MedicineID = temp.MedicineID;
                    //    BasketViewModel.UserID = temp.UserID;
                    //    BasketViewModel.Quantity = temp.Quantity;

                    //    await BasketViewModel.DeleteBasketAsync();
                    //}

                }
            }
        }
    }
}
