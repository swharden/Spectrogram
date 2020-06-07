using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Spectrogram.Colormaps
{
    public abstract class Colormap
    {
        // each colormap must implement its own lookup method
        public abstract (byte r, byte g, byte b) GetRGB(byte value);

        public string GetName()
        {
            return GetType().Name;
        }

        public override string ToString()
        {
            return $"Colormap {GetName()}";
        }

        public (byte r, byte g, byte b) GetRGB(int value)
        {
            value = Math.Max(value, 0);
            value = Math.Min(value, 255);
            return GetRGB((byte)value);
        }

        public (byte r, byte g, byte b) GetRGB(double fraction)
        {
            return GetRGB((int)(fraction * 255));
        }

        public Color GetColor(byte value)
        {
            var (r, g, b) = GetRGB(value);
            return Color.FromArgb(255, r, g, b);
        }

        public Color GetColor(int value)
        {
            var (r, g, b) = GetRGB(value);
            return Color.FromArgb(255, r, g, b);
        }

        public Color GetColor(double fraction)
        {
            var (r, g, b) = GetRGB(fraction);
            return Color.FromArgb(255, r, g, b);
        }

        public int GetInt32(byte value)
        {
            var (r, g, b) = GetRGB(value);
            Int32 argb = 255 << 24 | r << 16 | g << 8 | b;
            return argb;
        }

        public int GetInt32(int value)
        {
            value = Math.Max(value, 0);
            value = Math.Min(value, 255);
            return GetInt32((byte)value);
        }

        public int GetInt32(double fraction)
        {
            return GetInt32((int)(fraction * 255));
        }

        public void Apply(Bitmap bmp)
        {
            ColorPalette pal = bmp.Palette;
            for (int i = 0; i < 256; i++)
                pal.Entries[i] = GetColor((byte)i);
            bmp.Palette = pal;
        }
    }
}
