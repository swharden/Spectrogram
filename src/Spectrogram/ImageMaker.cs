using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using SkiaSharp;

namespace Spectrogram
{
    /// <summary>
    /// This class converts a collection of FFTs to a colormapped spectrogram image
    /// </summary>
    public class ImageMaker
    {
        /// <summary>
        /// Colormap used to translate intensity to pixel color
        /// </summary>
        public Colormap Colormap;

        /// <summary>
        /// Intensity is multiplied by this number before converting it to the pixel color according to the colormap
        /// </summary>
        public double Intensity = 1;

        /// <summary>
        /// If True, intensity will be log-scaled to represent Decibels
        /// </summary>
        public bool IsDecibel = false;

        /// <summary>
        /// If <see cref="IsDecibel"/> is enabled, intensity will be scaled by this value prior to log transformation
        /// </summary>
        public double DecibelScaleFactor = 1;

        /// <summary>
        /// If False, the spectrogram will proceed in time from left to right across the whole image.
        /// If True, the image will be broken and the newest FFTs will appear on the left and oldest on the right.
        /// </summary>
        public bool IsRoll = false;

        /// <summary>
        /// If <see cref="IsRoll"/> is enabled, this value indicates the pixel position of the break point.
        /// </summary>
        public int RollOffset = 0;

        /// <summary>
        /// If True, the spectrogram will flow top-down (oldest to newest) rather than left-right.
        /// </summary>
        public bool IsRotated = false;

        public ImageMaker()
        {

        }
        
        public SKBitmap GetBitmap(List<double[]> ffts)
        {
            if (ffts.Count == 0)
                throw new ArgumentException("Not enough data in FFTs to generate an image yet.");

            int width = IsRotated ? ffts[0].Length : ffts.Count;
            int height = IsRotated ? ffts.Count : ffts[0].Length;

            var imageInfo = new SKImageInfo(width, height, SKColorType.Gray8);
            var bitmap = new SKBitmap(imageInfo);

            int pixelCount = width * height;
            byte[] pixelBuffer = new byte[pixelCount];

            Parallel.For(0, width, col =>
            {
                int sourceCol = col;
                if (IsRoll)
                {
                    sourceCol += width - RollOffset % width;
                    if (sourceCol >= width)
                        sourceCol -= width;
                }

                for (int row = 0; row < height; row++)
                {
                    double value = IsRotated
                        ? ffts[height - row - 1][sourceCol]
                        : ffts[sourceCol][row];

                    if (IsDecibel)
                        value = 20 * Math.Log10(value * DecibelScaleFactor + 1);

                    value *= Intensity;
                    value = Math.Min(value, 255);

                    int bytePosition = (height - 1 - row) * width + col;
                    pixelBuffer[bytePosition] = (byte)value;
                }
            });

            IntPtr pixelPtr = bitmap.GetPixels();
            Marshal.Copy(pixelBuffer, 0, pixelPtr, pixelBuffer.Length);

            return bitmap;
        }
    }
}
