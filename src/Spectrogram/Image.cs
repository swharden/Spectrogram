using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Spectrogram
{
    class Image
    {
        public static Bitmap BitmapFromFFTs(List<float[]> ffts, int? fixedWidth = null)
        {

            if (ffts == null || ffts.Count == 0)
                throw new ArgumentException("ffts must contain float arrays");

            // use indexed colors to make it easy to convert from value to color
            int width = (fixedWidth == null) ? ffts.Count : (int)fixedWidth;
            Bitmap bmp = new Bitmap(width, ffts[0].Length, PixelFormat.Format8bppIndexed);
            Palette.ApplyLUT(bmp, Palette.LUT.viridis);

            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bitmapData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            byte[] pixels = new byte[bitmapData.Stride * bmp.Height];

            // TODO: smarter intensity scaling (adjustable gain?)
            float scaleMax = 100;
            /*
            float scaleMax = 0;
            foreach (float value in ffts[ffts.Count / 2])
                scaleMax = Math.Max(scaleMax, value);
            scaleMax *= (float).2;
            */

            for (int col = 0; col < ffts.Count; col++)
            {
                if (col < width)
                {
                    for (int row = 0; row < ffts[col].Length; row++)
                    {
                        int bytePosition = (ffts[col].Length - 1 - row) * bitmapData.Stride + col;
                        float pixelValue = ffts[col][row] / scaleMax * 255;
                        pixelValue = Math.Max(0, pixelValue);
                        pixelValue = Math.Min(255, pixelValue);
                        pixels[bytePosition] = (byte)(pixelValue);
                    }
                }
            }

            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            bmp.UnlockBits(bitmapData);

            return bmp;
        }
    }
}
