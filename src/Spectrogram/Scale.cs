using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Spectrogram
{
    static class Scale
    {
        public static Bitmap Vertical(int width, Settings settings, int offsetHz = 0, int tickSize = 3, int reduction = 1)
        {
            double tickHz = 1;
            int minSpacingPx = 50;
            double[] multipliers = { 2, 2.5, 2 };
            int multiplier = 0;
            while (true)
            {
                tickHz *= multipliers[multiplier++ % multipliers.Length];
                double tickCount = settings.FreqSpan / tickHz;
                double pxBetweenTicks = settings.Height / tickCount;
                if (pxBetweenTicks >= minSpacingPx * reduction)
                    break;
            }

            Bitmap bmp = new Bitmap(width, settings.Height / reduction, PixelFormat.Format32bppPArgb);

            using (var gfx = Graphics.FromImage(bmp))
            using (var pen = new Pen(Color.Black))
            using (var brush = new SolidBrush(Color.Black))
            using (var font = new Font(FontFamily.GenericMonospace, 10))
            using (var sf = new StringFormat() { LineAlignment = StringAlignment.Center })
            {
                gfx.Clear(Color.White);

                List<double> freqs = new List<double>();

                for (double f = settings.FreqMin; f <= settings.FreqMax; f += tickHz)
                    freqs.Add(f);

                // don't show first or last tick
                if (freqs.Count >= 2)
                {
                    freqs.RemoveAt(0);
                    freqs.RemoveAt(freqs.Count - 1);
                }

                foreach (var freq in freqs)
                {
                    int y = settings.PixelY(freq) / reduction;
                    gfx.DrawLine(pen, 0, y, tickSize, y);
                    gfx.DrawString($"{freq + offsetHz:N0} Hz", font, brush, tickSize, y, sf);
                }
            }

            return bmp;
        }
    }
}