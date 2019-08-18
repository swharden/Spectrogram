using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram.Settings
{
    public class DisplaySettings
    {
        // The Spectrograph library does two things:
        //   1) convert a signal to a FFT List
        //   2) convert a FFT list to a Bitmap

        // This class stores settings that control how the Bitmap looks (#2)

        public double lastRenderMsec;
    }
}
