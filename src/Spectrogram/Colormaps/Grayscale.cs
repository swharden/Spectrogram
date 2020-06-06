using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Spectrogram.Colormaps
{
    public class Grayscale : IColormap
    {
        public void Apply(Bitmap bmp)
        {
            ColorPalette pal = bmp.Palette;
            for (int i = 0; i < 256; i++)
                pal.Entries[i] = Color.FromArgb(255, i, i, i);
            bmp.Palette = pal;
        }

        public string GetName()
        {
            return "Grayscale";
        }

        public (byte r, byte g, byte b) Lookup(int value)
        {
            return ((byte)value, (byte)value, (byte)value);
        }
    }
}
