using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Spectrogram.Colormaps
{
    public abstract class Colormap
    {
        public abstract string GetName();
        public abstract void Apply(Bitmap bmp);
    }
}
