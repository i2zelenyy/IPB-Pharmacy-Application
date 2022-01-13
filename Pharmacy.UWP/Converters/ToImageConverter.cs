﻿using Pharmacy.UWP.ViewModels.BasketViewModel;
using Pharmacy.UWP.ViewModels.MedicineViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Pharmacy.UWP
{
    public class ToImageConverter : IValueConverter
    {
        public BasketViewModel BasketViewModel { get; set; }
        public MedicineViewModel MedicineViewModel { get; set; }

        public async Task Converter()
        {
            BasketViewModel = new BasketViewModel();
            await BasketViewModel.LoadAllAsync();

            MedicineViewModel = new MedicineViewModel();
            await MedicineViewModel.LoadAllAsync();
        }


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Converter();

            foreach (var item in MedicineViewModel.Medicine)
            {
                if ((int)value == item.Id)
                {
                    ToBitmapConverter ToBitmapConverter = new ToBitmapConverter();
                    
                    return ToBitmapConverter.Converter(item.MedicineImage);
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
