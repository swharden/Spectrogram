using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Spectrogram
{
    public static class Image
    {
        public static Bitmap GetBitmap(List<double[]> ffts, Colormap cmap, double intensity = 1,
            bool dB = false, double dBScale = 1, bool roll = false, int rollOffset = 0, bool rotate = false)
        {

            ImageMaker maker = new()
            {
                Colormap = cmap,
                Intensity = intensity,
                IsDecibel = dB,
                DecibelScaleFactor = dBScale,
                IsRoll = roll,
                RollOffset = rollOffset,
                IsRotated = rotate,
            };

            return maker.GetBitmap(ffts);
        }
    }
}
