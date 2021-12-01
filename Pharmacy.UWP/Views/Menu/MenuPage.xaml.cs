using Pharmacy.UWP.Views.Basket;
using Pharmacy.UWP.Views.Medicine;
using Pharmacy.UWP.Views.Profile;
using Pharmacy.UWP.Views.Stores;
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


namespace Pharmacy.UWP.Views.Menu
{
    public sealed partial class MenuPage : Page
    {
        public MenuPage()
        {
            this.InitializeComponent();
        }

        private void ProfilePageItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            frame.Navigate(typeof(ProfilePage));
            Page_Header.Text = "Profile";
            
        }

        private void SignOutItem_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            Menu.SelectedItem = HomePageItem;
        }

        private void Menu_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem nvi = (NavigationViewItem)args.SelectedItemContainer;
            if (nvi != null)
            {
                switch (nvi.Tag)
                {
                    case "HomePage":
                        frame.Navigate(typeof(HomePage));
                        Page_Header.Text = "Home";
                        break;
                    case "MedicinePage":
                        frame.Navigate(typeof(MedicinePage));
                        Page_Header.Text = "Medicine";
                        break;

                    case "BasketPage":
                        frame.Navigate(typeof(BasketPage));
                        Page_Header.Text = "Basket";
                        break;

                    case "StoresPage":
                        frame.Navigate(typeof(StoresPage));
                        Page_Header.Text = "Stores";
                        break;
                }
            }
        }
    }
}
