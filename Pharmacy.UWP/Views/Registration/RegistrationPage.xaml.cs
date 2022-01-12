using Pharmacy.UWP.ViewModels.UsersViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        Domain.Models.Users selectedUser;
        private bool _error = false;

        public RegistrationPage()
        {
            this.InitializeComponent();

            UsersViewModel = new UsersViewModel();          
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            List<object> data = (List<object>)e.Parameter;

            try
            {
                selectedUser = (Domain.Models.Users)data[0];

                if (data[1] as string == "Edit")
                {
                    SaveButton.Visibility = Visibility.Visible;
                    CreateButton.Visibility = Visibility.Collapsed;

                    FirstNameTextBox.Text = selectedUser.FirstName;
                    LastNameTextBox.Text = selectedUser.LastName;
                    DateOfBirthTextBox.Text = selectedUser.DateOfBirth.ToString("dd/MM/yyyy");
                    UsernameTextBox.Text = selectedUser.Username;
                    PasswordBox_1.Password = selectedUser.Password;
                    TelephoneTextBox.Text = selectedUser.Telephone;
                    StreetTextBox.Text = selectedUser.Street;
                }
                else
                {
                    SaveButton.Visibility = Visibility.Collapsed;
                    CreateButton.Visibility = Visibility.Visible;
                }
            }
            catch
            {
                SaveButton.Visibility = Visibility.Collapsed;
                CreateButton.Visibility = Visibility.Visible;
            }            
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
                    UsersViewModel.DateOfBirth = DateTime.ParseExact(DateOfBirthTextBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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
                        UsersViewModel.DateOfBirth = DateTime.ParseExact(DateOfBirthTextBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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
                        UsersViewModel.DateOfBirth = DateTime.ParseExact(DateOfBirthTextBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser != null)
                UsersViewModel.Id = selectedUser.Id;

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
                    UsersViewModel.DateOfBirth = DateTime.ParseExact(DateOfBirthTextBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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
                SaveMessageBox.Text = "Do you want to save the changes?";
                SaveConfirmationButton.Visibility = Visibility.Visible;

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
                        UsersViewModel.DateOfBirth = DateTime.ParseExact(DateOfBirthTextBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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
                SaveMessageBox.Text = "Incorrect Data!";
                SaveConfirmationButton.Visibility = Visibility.Collapsed;
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
                        UsersViewModel.DateOfBirth = DateTime.ParseExact(DateOfBirthTextBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        DateOfBirthTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    }
                }
            }
        }

        private async void SaveConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            if (SaveMessageBox.Text == "Do you want to save the changes?")
            {
                await UsersViewModel.UpdateUserAsync();
                this.Frame.GoBack();
            }
        }
    }
}
