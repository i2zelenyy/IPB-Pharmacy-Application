using Pharmacy.UWP.ViewModels.MedicineViewModel;
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

namespace Pharmacy.UWP.Views.Medicine
{
    public sealed partial class MedicinePage : Page
    {
        public MedicineViewModel MedicineViewModel { get; set; }
        Domain.Models.Medicine selectedMedicine;

        public MedicinePage()
        {
            this.InitializeComponent();
            MedicineViewModel = new MedicineViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await Task.Delay(100);
            MedicineViewModel.LoadAllAsync();
        }

        private async void DeleteConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await MedicineViewModel.DeleteMedicineAsync();
                Frame.Navigate(this.GetType());
            }
            catch
            {

            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMedicine != null)
            {
                List<object> data = new List<object>();
                data.Add(selectedMedicine);
                data.Add(EditButtonText.Text);

                this.Frame.Navigate(typeof(ManageMedicinePage), data);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> data = new List<object>();
            data.Add(selectedMedicine);
            data.Add(AddButtonText.Text);

            this.Frame.Navigate(typeof(ManageMedicinePage), data);
        }

        private void MedicineListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedMedicine = (Domain.Models.Medicine)MedicineListView.SelectedItem;

            MedicineViewModel.Id = selectedMedicine.Id;
            MedicineViewModel.Name = selectedMedicine.Name;
            MedicineViewModel.Category = selectedMedicine.Category;
            MedicineViewModel.Brand = selectedMedicine.Brand;
            MedicineViewModel.Price = selectedMedicine.Price;
            MedicineViewModel.Description = selectedMedicine.Description;
            MedicineViewModel.Ingredients = selectedMedicine.Ingredients;
            MedicineViewModel.HowToUse = selectedMedicine.HowToUse;
        }
    }
}
