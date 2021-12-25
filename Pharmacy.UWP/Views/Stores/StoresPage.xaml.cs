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
using Pharmacy.UWP.ViewModels.StoresViewModel;
using Pharmacy.Infrastructure;
using Pharmacy.Domain;
using Pharmacy.Domain.Models;
using System.Threading.Tasks;

namespace Pharmacy.UWP.Views.Stores
{
    public sealed partial class StoresPage : Page
    {
        public StoresViewModel StoresViewModel { get; set; }

        public StoresPage()
        {           
            this.InitializeComponent();
            StoresViewModel = new StoresViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            StoresViewModel.LoadAllAsync();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
