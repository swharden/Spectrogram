using System;

namespace Spectrogram
{
    [Obsolete("This class has been replaced by SpectrogramGenerator")]
    public class Spectrogram : SpectrogramGenerator
    {
        public Spectrogram(int sampleRate, int fftSize, int stepSize, double minFreq = 0, double maxFreq = double.PositiveInfinity, int? fixedWidth = null, int offsetHz = 0) :
            base(sampleRate, fftSize, stepSize, minFreq, maxFreq, fixedWidth, offsetHz)
        { }
    }
}
