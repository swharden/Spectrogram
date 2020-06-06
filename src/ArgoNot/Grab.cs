using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgoNot
{
    public class Grab
    {
        public int padLeft = 10;
        public int padRight = 140;
        public int padTop = 10;
        public int padBottom = 40;
        public Color bgColor;

        private readonly Bitmap bmpSpec;

        public Grab(Spectrogram.SpectrogramLive spec, double freqMin, double freqMax)
        {
            // Don't hold onto the spectrogram.
            // Copy what you need then drop it.

            Console.WriteLine(spec.GetDetails());

            int indexLow = (int)(freqMin / spec.fftResolution);
            int indexHigh = (int)(freqMax / spec.fftResolution);
            int height = indexHigh - indexLow;
            Console.WriteLine($"Output height: {height}");

            bmpSpec = (Bitmap)spec.GetBitmap().Clone();

            var (r, g, b) = spec.cmap.Lookup(0);
            bgColor = Color.FromArgb(r, g, b);
        }

        public Bitmap GetBitmap()
        {
            int width = bmpSpec.Width + padLeft + padRight;
            int height = bmpSpec.Height + padTop + padBottom;
            Bitmap bmp = new Bitmap(width, height);

            using (var gfx = Graphics.FromImage(bmp))
            using (var pen = new Pen(Color.White))
            {
                gfx.Clear(bgColor);
                gfx.DrawImage(bmpSpec, padLeft, padTop);
                gfx.DrawRectangle(pen, padLeft, padTop, bmpSpec.Width, bmpSpec.Height);
            }

            return bmp;
        }
    }
}
