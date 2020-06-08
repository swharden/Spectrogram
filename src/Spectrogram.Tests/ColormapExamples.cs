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
            int sampleRate = 44100;
            int fftSize = 1 << 13;
            var spec = new Spectrogram(sampleRate, fftSize, stepSize: 200, maxFreq: 3000);
            spec.SetWindow(FftSharp.Window.Hanning(fftSize/3)); // sharper window than typical
            spec.Add(audio);
            spec.SetColormap(cmap);
            spec.SaveImage($"../../../../../dev/graphics/hal-{cmap.Name}.png", intensity: .5);
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
