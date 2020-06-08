using NuGet.Frameworks;
using NUnit.Framework;
using Spectrogram.Colormaps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Spectrogram.Tests
{
    class ColormapExamples
    {
        private void HalSpectrogram(Colormap cmap)
        {
            double[] audio = Read.WavInt16mono("../../../../../data/cant-do-that-44100.wav");
            var spec = new Spectrogram(sampleRate: 44100, fftSize: 1 << 12, stepSize: 500, maxFreq: 3000);
            spec.Add(audio);
            spec.SetColormap(cmap);
            spec.SaveImage($"../../../../../dev/graphics/hal-{cmap.Name}.png", intensity: .2);
        }
        [Test]

        public void Test_Make_CommonColormaps()
        {
            HalSpectrogram(Colormap.Argo);
            HalSpectrogram(Colormap.Blues);
            HalSpectrogram(Colormap.Cividis);
            HalSpectrogram(Colormap.Grayscale);
            HalSpectrogram(Colormap.GrayscaleReversed);
            HalSpectrogram(Colormap.Greens);
            HalSpectrogram(Colormap.Inferno);
            HalSpectrogram(Colormap.Jet);
            HalSpectrogram(Colormap.Magma);
            HalSpectrogram(Colormap.Plasma);
            HalSpectrogram(Colormap.Viridis);
        }
    }
}
