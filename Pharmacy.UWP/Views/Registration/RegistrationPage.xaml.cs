using Pharmacy.UWP.ViewModels.UsersViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Pharmacy.UWP.Views.Registration
{
    public sealed partial class RegistrationPage : Page
    {
        public UsersViewModel UsersViewModel { get; set; }
        private bool _error = false;

        public RegistrationPage()
        {
            this.InitializeComponent();

            UsersViewModel = new UsersViewModel();          
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstNameTextBox.Text != "")
                UsersViewModel.FirstName = FirstNameTextBox.Text;
            else
                _error = true;

            if (LastNameTextBox.Text != "")
                UsersViewModel.LastName = LastNameTextBox.Text;
            else
                _error = true;

            if (UsernameTextBox.Text != "")
                UsersViewModel.Username = UsernameTextBox.Text;
            else
                _error = true;

            if (DateOfBirthTextBox.Text != "")
                try
                {
                    UsersViewModel.DateOfBirth = DateTime.Parse(DateOfBirthTextBox.Text);
                }
                catch
                {
                    _error = true;
                }
            else
                _error = true;

            if (PasswordBox_1.Password != "")
            {
                if (PasswordBox_1.Password == PasswordBox_2.Password)
                    UsersViewModel.Password = PasswordBox_1.Password;
                else
                    _error = true;
            }
            else
                _error = true;

            UsersViewModel.Telephone = TelephoneTextBox.Text;
            UsersViewModel.Street = StreetTextBox.Text;
            UsersViewModel.Email = EmailTextBox.Text;

            if (_error == false)
            {
                CreateMessageBox.Text = "Do you want to create the account?";
                CreateConfirmationButton.Visibility = Visibility.Visible;

                if (FirstNameTextBox.Text != "")
                    FirstNameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (LastNameTextBox.Text != "")
                    LastNameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (UsernameTextBox.Text != "")
                    UsernameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (DateOfBirthTextBox.Text != "")
                {
                    try
                    {
                        UsersViewModel.DateOfBirth = DateTime.Parse(DateOfBirthTextBox.Text);
                        DateOfBirthTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
                    }
                    catch
                    {
                        _error = true;
                    }
                }

                if (PasswordBox_1.Password != "")
                {
                    if (PasswordBox_1.Password == PasswordBox_2.Password)
                        PasswordBox_1.BorderBrush = new SolidColorBrush(Colors.Black);
                }
            }

            else
            {
                CreateMessageBox.Text = "Incorrect Data!";
                CreateConfirmationButton.Visibility = Visibility.Collapsed;
                _error = false;

                if (FirstNameTextBox.Text == "")
                    FirstNameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                if (LastNameTextBox.Text == "")
                    LastNameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                if (UsernameTextBox.Text == "")
                    UsernameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                if (DateOfBirthTextBox.Text == "")
                    DateOfBirthTextBox.BorderBrush = new SolidColorBrush(Colors.Red);

                if (PasswordBox_1.Password == "")
                    PasswordBox_1.BorderBrush = new SolidColorBrush(Colors.Red);

                if (FirstNameTextBox.Text != "")
                    FirstNameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (LastNameTextBox.Text != "")
                    LastNameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (UsernameTextBox.Text != "")
                    UsernameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);

                if (DateOfBirthTextBox.Text != "")
                {
                    try
                    {
                        UsersViewModel.DateOfBirth = DateTime.Parse(DateOfBirthTextBox.Text);
                    }
                    catch
                    {
                        DateOfBirthTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    }
                }
            }
        }

        private async void CreateConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            if (CreateMessageBox.Text == "Do you want to create the account?")
            {
                UsersViewModel.IsAdmin = false;
                await UsersViewModel.CreateUserAsync();
                this.Frame.GoBack();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
