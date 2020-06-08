using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram.Colormaps
{
    class GrayscaleReversed : IColormap
    {
        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            value = (byte)(255 - value);
            return (value, value, value);
        }
    }
}
