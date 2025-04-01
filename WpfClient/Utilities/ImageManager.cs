using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace WpfClient.Utilities
{
    public static class ImageManager
    {
        public static ImageSource ByteArrayToImageSource(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            try
            {
                using (var ms = new MemoryStream(imageData))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    image.Freeze();
                    return image;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
