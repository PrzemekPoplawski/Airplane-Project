using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Data_Entry_Project
{
    public static class Extensions
    {
        public static byte[] ToByteArray(this BitmapSource bitmapSource)
        {

            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                return stream.ToArray();
            }
        }

        public static BitmapSource ToBitmapSource(this byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                var decoder = new JpegBitmapDecoder(stream, BitmapCreateOptions.None, BitmapCacheOption.Default);
                return decoder.Frames.First();
            }
        }

        public static BitmapSource GetImageStream(Image myImage)
        {
            var bitmap = new Bitmap(myImage);
            IntPtr bmpPt = bitmap.GetHbitmap();
            BitmapSource bitmapSource =
             System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmpPt,IntPtr.Zero,Int32Rect.Empty,BitmapSizeOptions.FromEmptyOptions());

            bitmapSource.Freeze();

            return bitmapSource;
        }

    }
}
