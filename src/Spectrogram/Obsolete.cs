using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram
{
    [Obsolete("The Spectrogram class has been renamed to SGram")]
    public class Spectrogram : SGram
    {
        public Spectrogram
            (int sampleRate, int fftSize, int stepSize, double minFreq = 0, double maxFreq = double.PositiveInfinity, int? fixedWidth = null, int offsetHz = 0) :
            base(sampleRate, fftSize, stepSize, minFreq, maxFreq, fixedWidth, offsetHz)
        { }
    }
}
