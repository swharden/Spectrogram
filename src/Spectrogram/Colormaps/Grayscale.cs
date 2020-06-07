using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram.Colormaps
{
    class Grayscale : IColormap
    {
        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            return (value, value, value);
        }
    }
}
