using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram
{
    public interface IColormap
    {
        (byte r, byte g, byte b) GetRGB(byte value);
    }
}
