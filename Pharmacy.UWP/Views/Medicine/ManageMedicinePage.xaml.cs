using Pharmacy.UWP.ViewModels.MedicineViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
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
    public sealed partial class ManageMedicinePage : Page
    {
        public MedicineViewModel MedicineViewModel { get; set; }
        Domain.Models.Medicine selectedMedicine;
        private bool _error = false;        

        public ManageMedicinePage()
        {
            this.InitializeComponent();
            MedicineViewModel = new MedicineViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<object> data = (List<object>)e.Parameter;
            selectedMedicine = (Domain.Models.Medicine)data[0];

            if (data[1] as string == "Edit")
            {
                AddButton.Visibility = Visibility.Collapsed;
                SaveButton.Visibility = Visibility.Visible;
                HeaderTextBlock.Text = "Manage (Editing)";

                NameTextBox.Text = selectedMedicine.Name;
                CategoryTextBox.Text = selectedMedicine.Category;
                BrandTextBox.Text = selectedMedicine.Brand;
                PriceTextBox.Text = selectedMedicine.Price.ToString();
            }
            else
            {
                AddButton.Visibility = Visibility.Visible;
                SaveButton.Visibility = Visibility.Collapsed;
                HeaderTextBlock.Text = "Manage (Adding)";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            {
                if (NameTextBox.Text != "")
                    MedicineViewModel.Name = NameTextBox.Text;
                else
                    _error = true;

                if (CategoryTextBox.Text != "")
                    MedicineViewModel.Category = CategoryTextBox.Text;
                else
                    _error = true;

                if (BrandTextBox.Text != "")
                    MedicineViewModel.Brand = BrandTextBox.Text;
                else
                    _error = true;

                if (PriceTextBox.Text != "")
                    MedicineViewModel.Price = Convert.ToInt64(PriceTextBox.Text);
                else
                    _error = true;

                //MedicineViewModel.HowToUse = HowToUseTextBox.Text;
                //MedicineViewModel.Ingredients = IngredientsTextBox.Text;
                //MedicineViewModel.

                //if (_error == false)
                //{
                //    AddMessageBox.Text = "Do you want to add the store?";
                //    AddConfirmationButton.Visibility = Visibility.Visible;

                //    if (NameTextBox.Text != "")
                //        NameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                //    if (CountryTextBox.Text != "")
                //        CountryTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                //    if (CityTextBox.Text != "")
                //        CityTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                //    if (StreetTextBox.Text != "")
                //        StreetTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
                //}
                //else
                //{
                //    AddMessageBox.Text = "Incorrect Data!";
                //    AddConfirmationButton.Visibility = Visibility.Collapsed;
                //    _error = false;

                //    if (NameTextBox.Text == "")
                //        NameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                //    if (CountryTextBox.Text == "")
                //        CountryTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                //    if (CityTextBox.Text == "")
                //        CityTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                //    if (StreetTextBox.Text == "")
                //        StreetTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                //}

            }
        }

        private void AddConfirmationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveConfirmationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void AttachButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();

            BitmapImage image = new BitmapImage();
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                await image.SetSourceAsync(stream);
            }
            MedicineImage.Source = image;

            //MedicineViewModel.MedicineImage = 

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MedicineImage.Source = null;
        }

    }
}
