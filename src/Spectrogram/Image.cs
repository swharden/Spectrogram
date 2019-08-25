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
        public static Bitmap BitmapFromFFTs(float[][] ffts, Settings.DisplaySettings displaySettings)
        {

            if (ffts == null || ffts.Length == 0)
                throw new ArgumentException("ffts must contain float arrays");

            Bitmap bmp = new Bitmap(ffts.Length, displaySettings.height, PixelFormat.Format8bppIndexed);
            ApplyColormap(bmp, displaySettings.colormap);

            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bitmapData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            byte[] pixels = new byte[bitmapData.Stride * bmp.Height];

            for (int col = 0; col < bmp.Width; col++)
            {
                if (col >= bmp.Width)
                    continue;

                if (col == displaySettings.highlightColumn)
                {
                    for (int row = 0; row < bmp.Height; row++)
                    {
                        int bytePosition = (bmp.Height - 1 - row) * bitmapData.Stride + col;
                        pixels[bytePosition] = 255;
                    }
                    continue;
                }

                if (ffts[col] == null)
                    continue;

                for (int row = 0; row < bmp.Height; row++)
                {
                    int bytePosition = (bmp.Height - 1 - row) * bitmapData.Stride + col;
                    float pixelValue;
                    pixelValue = ffts[col][row + displaySettings.pixelLower];
                    if (displaySettings.decibels)
                        pixelValue = (float)(Math.Log10(pixelValue) * 20);
                    pixelValue = (pixelValue * displaySettings.brightness);
                    pixelValue = Math.Max(0, pixelValue);
                    pixelValue = Math.Min(255, pixelValue);
                    pixels[bytePosition] = (byte)(pixelValue);
                }
            }

            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            bmp.UnlockBits(bitmapData);

            return bmp;
        }

        public static Bitmap Rotate(Bitmap bmpIn, float angle = 90)
        {
            // TODO: this could be faster with byte manipulation since it's 90 degrees

            if (bmpIn == null)
                return null;

            Bitmap bmp = new Bitmap(bmpIn);
            Bitmap bmpRotated = new Bitmap(bmp.Height, bmp.Width);

            Graphics gfx = Graphics.FromImage(bmpRotated);
            gfx.RotateTransform(angle);
            gfx.DrawImage(bmp, new Point(0, -bmp.Height));

            return bmpRotated;
        }

        public static void ApplyColormap(Bitmap bmp, Colormap colormap)
        {
            switch (colormap)
            {
                case Colormap.grayscale:
                    new Colormaps.Grayscale().Apply(bmp);
                    break;
                case Colormap.vdBlue:
                    new Colormaps.VdBlues().Apply(bmp);
                    break;
                case Colormap.vdGreen:
                    new Colormaps.VdGreens().Apply(bmp);
                    break;
                case Colormap.viridis:
                    new Colormaps.Viridis().Apply(bmp);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
