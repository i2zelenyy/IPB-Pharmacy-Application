using Pharmacy.UWP.ViewModels.BasketViewModel;
using Pharmacy.UWP.ViewModels.ChequesViewModel;
using Pharmacy.UWP.ViewModels.MedicineViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Pharmacy.UWP.Views.Medicine
{
    public sealed partial class MedicinePage : Page
    {
        public MedicineViewModel MedicineViewModel { get; set; }
        public BasketViewModel BasketViewModel { get; set; }

        Domain.Models.Medicine selectedMedicine;
        Domain.Models.Users authorisedUser;
        List<object> data;
        bool _basketMode = false;

        public MedicinePage()
        {
            this.InitializeComponent();
            MedicineViewModel = new MedicineViewModel();
            BasketViewModel = new BasketViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            data = (List<object>)e.Parameter;

            authorisedUser = (Domain.Models.Users)data[0];

            await Task.Delay(50);
            await MedicineViewModel.LoadAllAsync();
            await BasketViewModel.LoadAllAsync();

            if (authorisedUser.IsAdmin == false)
            {
                AddButton.IsEnabled = false;
                EditButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
            else
            {
                foreach (var basket in BasketViewModel.Baskets)
                {
                    var temp = (Domain.Models.Baskets)basket;

                    foreach (var medicine in MedicineViewModel.Medicine)
                    {
                        if (temp.MedicineID == medicine.Id)
                        {
                            DeleteButton.IsEnabled = false;
                        }
                    }
                }
            }

            if (data[1] == "Basket")
            {
                _basketMode = true;

                AddButton.IsEnabled = true;
                EditButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
        }

        private async void DeleteConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await MedicineViewModel.DeleteMedicineAsync();
                Frame.Navigate(this.GetType(), data);
            }
            catch
            {

            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMedicine != null)
            {
                List<object> data = new List<object>();
                data.Add(selectedMedicine);
                data.Add(EditButtonText.Text);

                this.Frame.Navigate(typeof(ManageMedicinePage), data);
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (_basketMode == true)
            {
                if (selectedMedicine != null)
                {
                    BasketViewModel.MedicineID = selectedMedicine.Id;
                    BasketViewModel.UserID = authorisedUser.Id;
                    BasketViewModel.Quantity = 1;

                    await BasketViewModel.CreateBasketAsync();

                    this.Frame.GoBack();
                }
            }
            else
            {
                List<object> data = new List<object>();
                data.Add(selectedMedicine);
                data.Add(AddButtonText.Text);

                this.Frame.Navigate(typeof(ManageMedicinePage), data);
            }
        }

        private void MedicineListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedMedicine = (Domain.Models.Medicine)MedicineListView.SelectedItem;

            MedicineViewModel.Id = selectedMedicine.Id;
            MedicineViewModel.Name = selectedMedicine.Name;
            MedicineViewModel.Category = selectedMedicine.Category;
            MedicineViewModel.Brand = selectedMedicine.Brand;
            MedicineViewModel.Price = selectedMedicine.Price;
            MedicineViewModel.Description = selectedMedicine.Description;
            MedicineViewModel.Ingredients = selectedMedicine.Ingredients;
            MedicineViewModel.HowToUse = selectedMedicine.HowToUse;
        }



    }
}
