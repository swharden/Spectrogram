using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Spectrogram
{
    static class Scale
    {
        public static Bitmap Vertical(int width, Settings settings, int offsetHz = 0, int tickSize = 3)
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
                if (pxBetweenTicks >= minSpacingPx)
                    break;
            }

            Bitmap bmp = new Bitmap(width, settings.Height, PixelFormat.Format32bppPArgb);

            using (var gfx = Graphics.FromImage(bmp))
            using (var pen = new Pen(Color.Black))
            using (var brush = new SolidBrush(Color.Black))
            using (var font = new Font(FontFamily.GenericMonospace, 10))
            using (var sf = new StringFormat() { LineAlignment = StringAlignment.Center })
            {
                gfx.Clear(Color.White);

                for (double f = settings.FreqMin; f < settings.FreqMax; f += tickHz)
                {
                    int pxY = (int)((f - settings.FreqMin + settings.HzPerPixel) * settings.PxPerHz);

                    if (pxY < 10 || settings.Height - pxY < 10)
                        continue;

                    pxY = settings.Height - pxY - 1;

                    gfx.DrawLine(pen, 0, pxY, tickSize, pxY);
                    gfx.DrawString($"{f + offsetHz:N0} Hz", font, brush, tickSize, pxY, sf);
                }
            }
            return bmp;
        }
    }
}
