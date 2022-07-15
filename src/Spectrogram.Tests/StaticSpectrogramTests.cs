using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrogram.Tests;

internal class StaticSpectrogramTests
{
    [Test]
    public void Test_StaticSpectrogram_Process()
    {
        (double[] audio, int sampleRate) = AudioFile.ReadWAV("../../../../../data/cant-do-that-44100.wav");

        StaticSpectrogram sg = new(audio);
        sg.SaveImage("test.png", .2);
    }
}
