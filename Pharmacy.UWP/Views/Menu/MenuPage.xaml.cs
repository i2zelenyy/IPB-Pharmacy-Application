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

        private void Menu_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            NavigationViewItem nvi = (NavigationViewItem)args.InvokedItemContainer;
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

                    case "ProfilePage":
                        frame.Navigate(typeof(ProfilePage));
                        Page_Header.Text = "Profile";
                        break;
                }
            }
        }

        private void ProfilePageItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            frame.Navigate(typeof(ProfilePage));
            Page_Header.Text = "Profile";
        }

        private void SignOutItem_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
