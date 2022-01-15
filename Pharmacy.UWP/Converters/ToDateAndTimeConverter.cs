using Pharmacy.UWP.ViewModels.ChequesViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Pharmacy.UWP
{
    public class ToDateAndTimeConverter : IValueConverter
    {
        public ChequesViewModel ChequesViewModel { get; set; }

        public async Task Converter()
        {
            ChequesViewModel = new ChequesViewModel();
            await ChequesViewModel.LoadAllAsync();
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Converter();

            foreach (var item in ChequesViewModel.Cheques)
            {
                if ((int)value == item.Id)
                {
                    return item.Date.ToString("dd/MM/yyyy") + "   " + item.Time;
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
