using Pharmacy.UWP.ViewModels.UsersViewModel;
using Pharmacy.UWP.Views.SignIn;
using Pharmacy.UWP.Views.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Pharmacy.UWP.Views.Profile
{
    public sealed partial class ProfilePage : Page
    {
        public UsersViewModel UsersViewModel { get; set; }

        public ProfilePage()
        {
            this.InitializeComponent();
            UsersViewModel = new UsersViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            object data = e.Parameter;
            Domain.Models.Users authorisedUser = (Domain.Models.Users) data;

            UsersViewModel.FirstName = authorisedUser.FirstName;
            UsersViewModel.LastName = authorisedUser.LastName;
            UsersViewModel.Telephone = authorisedUser.Telephone;
            UsersViewModel.Email = authorisedUser.Email;
            UsersViewModel.DateOfBirth = authorisedUser.DateOfBirth;
            UsersViewModel.Password = authorisedUser.Password;
            UsersViewModel.Street = authorisedUser.Street;
            UsersViewModel.Username = authorisedUser.Username;
            UsersViewModel.Id = authorisedUser.Id;
            UsersViewModel.IsAdmin = authorisedUser.IsAdmin;

            UsernameTextBlock.Text = authorisedUser.Username;

            if (authorisedUser.IsAdmin == false)
            {
                ManageUsersButton.IsEnabled = false;
            }
            else
            {
                BecomeAdminButton.IsEnabled = false;
            }
        }

        private async void ChangePasswordConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox_1.Password != "")
            {
                if (PasswordBox_1.Password == PasswordBox_2.Password)
                {
                    UsersViewModel.Password = PasswordBox_1.Password;
                    await UsersViewModel.UpdateUserAsync();
                }
            }        
        }

        private async void ChangeTelephoneConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            if (TelephoneTextBox.Text != "")
            {
                UsersViewModel.Telephone = TelephoneTextBox.Text;
                await UsersViewModel.UpdateUserAsync();
            }
        }

        private async void ChangeStreetConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            if (StreetTextBox.Text != "")
            {
                UsersViewModel.Telephone = StreetTextBox.Text;
                await UsersViewModel.UpdateUserAsync();
            }
        }

        private void ManageUsersButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ManageUsersPage));
        }

        private async void BecomeAdminConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            if (BecomeAdminTextBox.Text == "admin")
            {
                UsersViewModel.IsAdmin = true;
                await UsersViewModel.UpdateUserAsync();
                BecomeAdminTextBox.Header = "Granted. Restart the Application!";
            }
        }

        private async void DeleteAccountConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteAccountTextBox.Text == "DELETE")
            {
                await UsersViewModel.DeleteUserAsync();
                this.Frame.Navigate(typeof(SignInPage));
            }
        }

    }
}
