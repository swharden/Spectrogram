using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Spectrogram.Colormaps
{
    class GrayscaleInverted : Colormap
    {
        public override string GetName()
        {
            return "Grayscale Inverted";
        }

        public override void Apply(Bitmap bmp)
        {
            ColorPalette pal = bmp.Palette;

            for (int i = 0; i < 256; i++)
                pal.Entries[i] = Color.FromArgb(255, 255 - i, 255 - i, 255 - i);

            bmp.Palette = pal;
        }

    }
}
