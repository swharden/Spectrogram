using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgoNot
{
    public class Ticks
    {
        public static void DrawTicks(Bitmap bmp, int width, int height, double freqMin, double freqMax, double dialFreq, double freqStep = 25, int tickSize = 5)
        {
            double pxPerHz = height / (freqMax - freqMin);

            using (var gfx = Graphics.FromImage(bmp))
            using (var pen = new Pen(Color.Black))
            using (var brush = new SolidBrush(Color.Black))
            using (var font = new Font(FontFamily.GenericMonospace, 10))
            using (var sf = new StringFormat())
            {
                gfx.Clear(Color.White);

                // frequency ticks
                sf.LineAlignment = StringAlignment.Center;
                for (double f = freqMin; f < freqMax; f += freqStep)
                {
                    int pxY = (int)((f - freqMin) * pxPerHz);

                    if (pxY < 10 || height - pxY < 10)
                        continue;

                    pxY = height - pxY;
                    gfx.DrawLine(pen, width, pxY, width + tickSize, pxY);
                    gfx.DrawString($"{f + dialFreq:N0} Hz", font, brush, width + tickSize, pxY, sf);
                }

                // minute ticks
                double durationSec = 60 * 10;
                sf.LineAlignment = StringAlignment.Near;
                sf.Alignment = StringAlignment.Center;
                for (int i = 0; i < durationSec; i += 60)
                {
                    if (i < 30 || durationSec - i < 30)
                        continue;

                    int xPx = (int)(width * i / durationSec);
                    xPx = width - xPx;

                    gfx.DrawLine(pen, xPx, height, xPx, height + tickSize);
                    gfx.DrawString($"{(durationSec - i) / 60:N0}", font, brush, xPx, height + tickSize, sf);
                }
            }
        }
    }
}
