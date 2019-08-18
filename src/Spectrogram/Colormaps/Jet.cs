using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Spectrogram.Colormaps
{
    class Jet : Colormap
    {
        public override string GetName()
        {
            return "Grayscale";
        }

        public override void Apply(Bitmap bmp)
        {
            ColorPalette pal = bmp.Palette;

            bmp.Palette = pal;
        }
    }
}
