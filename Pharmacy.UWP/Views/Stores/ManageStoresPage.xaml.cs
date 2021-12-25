using Pharmacy.UWP.ViewModels.StoresViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Pharmacy.UWP.Views.Stores
{
    public sealed partial class ManageStoresPage : Page
    {
        public StoresViewModel StoresViewModel { get; set; }
        private bool _error = false;

        public ManageStoresPage()
        {
            this.InitializeComponent();
            StoresViewModel = new StoresViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter as string == "Edit")
            {
                AddButton.Visibility = Visibility.Collapsed;
                SaveButton.Visibility = Visibility.Visible;
                HeaderTextBlock.Text = "Manage (Editing)";
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
                StoresViewModel.Name = NameTextBox.Text;
            else
                _error = true;

            if (CountryTextBox.Text != "")
                StoresViewModel.Country = CountryTextBox.Text;
            else
                _error = true;

            if (CityTextBox.Text != "")
                StoresViewModel.City = CityTextBox.Text;
            else
                _error = true;

            if (StreetTextBox.Text != "")
                StoresViewModel.Street = StreetTextBox.Text;
            else
                _error = true;
            
            StoresViewModel.Telephone = TelephoneTextBox.Text;
            StoresViewModel.OpeningHours = OpeningHoursTextBox.Text;

            if (_error == false)
            {
                AddMessageBox.Text = "Do you want to add the store?";
                AddConfirmationButton.Visibility = Visibility.Visible;

                if (NameTextBox.Text != "")
                    NameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (CountryTextBox.Text != "")
                    CountryTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (CityTextBox.Text != "")
                    CityTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (StreetTextBox.Text != "")
                    StreetTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
            }
            else
            {
                AddMessageBox.Text = "Incorrect Data!";
                AddConfirmationButton.Visibility = Visibility.Collapsed;
                _error = false;

                if (NameTextBox.Text == "")
                    NameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);  

                if (CountryTextBox.Text == "")
                    CountryTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                if (CityTextBox.Text == "")
                    CityTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                if (StreetTextBox.Text == "")
                    StreetTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
                
        }

        private async void AddConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddMessageBox.Text == "Do you want to add the store?")
            {
                await StoresViewModel.CreateStoreAsync();
                this.Frame.GoBack();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveConfirmationButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
