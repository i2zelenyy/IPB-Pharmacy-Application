using Pharmacy.UWP.ViewModels.UsersViewModel;
using Pharmacy.UWP.Views.Menu;
using Pharmacy.UWP.Views.Registration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace Pharmacy.UWP.Views.SignIn
{

    public sealed partial class SignInPage : Page
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public UsersViewModel UsersViewModel { get; set; }

        public SignInPage()
        {
            this.InitializeComponent();
            RememberMeCheck();

            UsersViewModel = new UsersViewModel();

            ApplicationView.PreferredLaunchViewSize = new Size(800, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        public void RememberMeCheck()
        {
            if (Convert.ToBoolean(localSettings.Values["RememberMe"]) == true)
            {
                CheckBox.IsChecked = true;
                UsernameBox.Text = localSettings.Values["Username"].ToString();
                PasswordBox.Password = localSettings.Values["Password"].ToString();
            }
                
            else
            {
                CheckBox.IsChecked = false;
                localSettings.Values["Username"] = "";
                localSettings.Values["Password"] = "";
            }               
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            localSettings.Values["RememberMe"] = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            localSettings.Values["RememberMe"] = false;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBox.IsChecked == true)
            {
                localSettings.Values["Username"] = UsernameBox.Text;
                localSettings.Values["Password"] = PasswordBox.Password;
            }

            await UsersViewModel.GetAuthorisedUserAsync(UsernameBox.Text, PasswordBox.Password);

            if (UsersViewModel.Users.Count == 1)
            {
                this.Frame.Navigate(typeof(MenuPage), UsersViewModel.Users[0]);
            }
            else
                ErrorText.Visibility = Visibility.Visible;
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegistrationPage));
        }
    }
}
