using Pharmacy.UWP.ViewModels.BasketViewModel;
using Pharmacy.UWP.ViewModels.ChequesViewModel;
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
    public sealed partial class ManageChequesPage : Page
    {
        public BasketsViewModel ChequesViewModel { get; set; }
        public BasketViewModel BasketViewModel { get; set; }

        object data;

        Domain.Models.Cheques selectedItem;
        Domain.Models.Users authorisedUser;

        public ManageChequesPage()
        {
            this.InitializeComponent();
            ChequesViewModel = new BasketsViewModel();
            BasketViewModel = new BasketViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            data = e.Parameter;
            authorisedUser = (Domain.Models.Users)data;

            await Task.Delay(50);
            await BasketViewModel.LoadBasketAsync(authorisedUser.Id);

            if (authorisedUser.IsAdmin == false)
            {
                foreach (var basket in BasketViewModel.Baskets)
                {
                    var temp = (Domain.Models.Baskets)basket;

                    await ChequesViewModel.LoadChequeAsync(temp.Id);
                }
            }
            else
            {
                await ChequesViewModel.LoadAllAsync();
            }
        }

        private async void DeleteConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await ChequesViewModel.DeleteChequeAsync();
                Frame.Navigate(this.GetType(), data);
            }
            catch
            {

            }
        }

        private void ChequestListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = (Domain.Models.Cheques)ChequesListView.SelectedItem;

            ChequesViewModel.Id = selectedItem.Id;
            ChequesViewModel.BasketsID = selectedItem.BasketsID;
            ChequesViewModel.StoresID = selectedItem.StoresID;
            ChequesViewModel.Date = selectedItem.Date;
            ChequesViewModel.Time = selectedItem.Time;
            ChequesViewModel.TotalPrice = selectedItem.TotalPrice;
        }
    }
}
