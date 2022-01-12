using Pharmacy.UWP.ViewModels.UsersViewModel;
using Pharmacy.UWP.Views.Registration;
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

namespace Pharmacy.UWP.Views.Users
{
    public sealed partial class ManageUsersPage : Page
    {
        public UsersViewModel UsersViewModel { get; set; }
        Domain.Models.Users selectedUser;

        public ManageUsersPage()
        {
            this.InitializeComponent();
            UsersViewModel = new UsersViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await Task.Delay(50);
            await UsersViewModel.LoadAllAsync();            
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegistrationPage));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> data = new List<object>();
            data.Add(selectedUser);
            data.Add(EditButtonText.Text);

            Frame.Navigate(typeof(RegistrationPage), data);
        }

        private async void DeleteConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await UsersViewModel.DeleteUserAsync();
                Frame.Navigate(this.GetType());
            }
            catch
            {

            }
        }

        private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUser = (Domain.Models.Users)UsersListView.SelectedItem;

            UsersViewModel.Id = selectedUser.Id;
            UsersViewModel.FirstName = selectedUser.FirstName;
            UsersViewModel.LastName = selectedUser.LastName;
            UsersViewModel.DateOfBirth = selectedUser.DateOfBirth;
            UsersViewModel.Username = selectedUser.Username;
            UsersViewModel.Password = selectedUser.Password;
            UsersViewModel.Telephone = selectedUser.Telephone;
            UsersViewModel.Street = selectedUser.Street;
            UsersViewModel.IsAdmin = selectedUser.IsAdmin;
        }
    }
}
