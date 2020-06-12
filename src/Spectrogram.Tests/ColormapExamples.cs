using NuGet.Frameworks;
using NUnit.Framework;
using Spectrogram.Colormaps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace Spectrogram.Tests
{
    class ColormapExamples
    {
        [Test]
        public void Test_Make_CommonColormaps()
        {
            double[] audio = Read.WavInt16mono("../../../../../data/cant-do-that-44100.wav");
            int sampleRate = 44100;
            int fftSize = 1 << 12;
            var spec = new Spectrogram(sampleRate, fftSize, stepSize: 700, maxFreq: 2000);
            spec.SetWindow(FftSharp.Window.Hanning(fftSize / 3)); // sharper window than typical
            spec.Add(audio);

            foreach(var cmap in Colormap.GetColormaps())
            {
                spec.SetColormap(cmap);
                spec.SaveImage($"../../../../../dev/graphics/hal-{cmap.Name}.png", intensity: .5);
                Debug.WriteLine($"![](dev/graphics/hal-{cmap.Name}.png)");
            }
        }
    }
}
