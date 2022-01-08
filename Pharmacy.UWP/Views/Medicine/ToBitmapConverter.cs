using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Pharmacy.UWP.Views.Medicine
{
    public class ToBitmapConverter : IValueConverter
    {
        public BitmapImage _image;

        public BitmapImage Converter(byte[] byte_image)
        {
            using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(byte_image);
                    writer.StoreAsync();
                }

                var image = new BitmapImage();
                image.SetSourceAsync(ms);
                return image;
            }
        }
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            byte[] byte_image = (byte[])value;

            using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(byte_image);
                    writer.StoreAsync();
                }

                var image = new BitmapImage();
                image.SetSourceAsync(ms);
                _image = image;

                return _image;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            DateTimeOffset resultTime = DateTime.Parse(value.ToString());
            return resultTime;
        }
    }
}
