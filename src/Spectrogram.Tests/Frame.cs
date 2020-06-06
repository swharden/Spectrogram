using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram.Tests
{
    public class Frame
    {
        [Test]
        public void Test_Frame_Works()
        {
            int sampleRate = 6000;
            var audio = FftSharp.SampleData.WhiteNoise(sampleRate * 60 * 10);
        }
    }
}
