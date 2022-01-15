using Pharmacy.UWP.ViewModels.BasketViewModel;
using Pharmacy.UWP.ViewModels.UsersViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Pharmacy.UWP
{
    public class FromBasketsID_Converter : IValueConverter
    {
        public BasketViewModel BasketViewModel { get; set; }
        public UsersViewModel UsersViewModel { get; set; }

        public async Task Converter()
        {
            BasketViewModel = new BasketViewModel();
            await BasketViewModel.LoadAllAsync();

            UsersViewModel = new UsersViewModel();
            await UsersViewModel.LoadAllAsync();
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Converter();

            foreach (var basket in BasketViewModel.Baskets)
            {
                if ((int)value == basket.Id)
                {
                    foreach (var user in UsersViewModel.Users)
                    {
                        if (basket.UserID == user.Id)
                        {
                            return (user.Username + " / " + user.FirstName).ToString();
                        }
                    }
                }
            }
            return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
