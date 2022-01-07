﻿using Pharmacy.UWP.ViewModels.MedicineViewModel;
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
using Windows.UI;
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
                DescriptionTextBox.Text = selectedMedicine.Description;
                IngredientsTextBox.Text = selectedMedicine.Ingredients;
                HowToUseTextBox.Text = selectedMedicine.HowToUse;
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
                try
                {
                    MedicineViewModel.Price = Convert.ToInt64(PriceTextBox.Text);
                }
                catch
                {
                    _error = true;
                } 
            else
                _error = true;

            MedicineViewModel.Description = DescriptionTextBox.Text;
            MedicineViewModel.Ingredients = IngredientsTextBox.Text;
            MedicineViewModel.HowToUse = HowToUseTextBox.Text;

            if (_error == false)
            {
                AddMessageBox.Text = "Do you want to add the medicine?";
                AddConfirmationButton.Visibility = Visibility.Visible;

                if (NameTextBox.Text != "")
                    NameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (CategoryTextBox.Text != "")
                    CategoryTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (BrandTextBox.Text != "")
                    BrandTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (PriceTextBox.Text != "")
                {
                    try
                    {
                        MedicineViewModel.Price = Convert.ToInt64(PriceTextBox.Text);
                        PriceTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
                    }
                    catch
                    {
                        _error = true;
                    }
                }
            }

            else
            {
                AddMessageBox.Text = "Incorrect Data!";
                AddConfirmationButton.Visibility = Visibility.Collapsed;
                _error = false;

                if (NameTextBox.Text == "")
                    NameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                if (CategoryTextBox.Text == "")
                    CategoryTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                if (BrandTextBox.Text == "")
                    BrandTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                if (PriceTextBox.Text == "")
                    PriceTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                if (PriceTextBox.Text != "")
                {
                    try
                    {
                        MedicineViewModel.Price = Convert.ToInt64(PriceTextBox.Text);
                    }
                    catch
                    {
                        PriceTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    }
                }

                if (NameTextBox.Text != "")
                    NameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (CategoryTextBox.Text != "")
                    CategoryTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (BrandTextBox.Text != "")
                    BrandTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
            }
        }

        private async void AddConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddMessageBox.Text == "Do you want to add the medicine?")
            {
                await MedicineViewModel.CreateMedicineAsync();
                this.Frame.GoBack();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMedicine != null)
            {
                MedicineViewModel.Id = selectedMedicine.Id;
            }

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
                try
                {
                    MedicineViewModel.Price = Convert.ToInt64(PriceTextBox.Text);
                }
                catch
                {
                    _error = true;
                }
            else
                _error = true;

            MedicineViewModel.Description = DescriptionTextBox.Text;
            MedicineViewModel.Ingredients = IngredientsTextBox.Text;
            MedicineViewModel.HowToUse = HowToUseTextBox.Text;

            if (_error == false)
            {
                SaveMessageBox.Text = "Do you want to save the changes?";
                SaveConfirmationButton.Visibility = Visibility.Visible;

                if (NameTextBox.Text != "")
                    NameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (CategoryTextBox.Text != "")
                    CategoryTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (BrandTextBox.Text != "")
                    BrandTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (PriceTextBox.Text != "")
                {
                    try
                    {
                        MedicineViewModel.Price = Convert.ToInt64(PriceTextBox.Text);
                        PriceTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
                    }
                    catch
                    {
                        _error = true;
                    }
                }
            }
            else
            {
                SaveMessageBox.Text = "Incorrect Data!";
                SaveConfirmationButton.Visibility = Visibility.Collapsed;
                _error = false;

                if (NameTextBox.Text == "")
                    NameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                if (CategoryTextBox.Text == "")
                    CategoryTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                if (BrandTextBox.Text == "")
                    BrandTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                if (PriceTextBox.Text == "")
                    PriceTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                if (PriceTextBox.Text != "")
                {
                    try
                    {
                        MedicineViewModel.Price = Convert.ToInt64(PriceTextBox.Text);
                    }
                    catch
                    {
                        PriceTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    }
                }

                if (NameTextBox.Text != "")
                    NameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (CategoryTextBox.Text != "")
                    CategoryTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (BrandTextBox.Text != "")
                    BrandTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
            }
        }

        private async void SaveConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            if (SaveMessageBox.Text == "Do you want to save the changes?")
            {
                await MedicineViewModel.UpdateMedicineAsync();
                this.Frame.GoBack();
            }
        }

        private async void AttachButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] fileBytes = null;

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();

            BitmapImage image = new BitmapImage();

            using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
            {
                fileBytes = new byte[stream.Size];
                using (DataReader reader = new DataReader(stream))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    reader.ReadBytes(fileBytes);
                    MedicineImage.Source = image;
                    MedicineViewModel.MedicineImage = fileBytes;
                }
            }

            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                await image.SetSourceAsync(stream);
            }

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MedicineViewModel.MedicineImage = null;
            MedicineImage.Source = null;
        }

    }
}
