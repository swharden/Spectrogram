using NuGet.Frameworks;
using NUnit.Framework;
using Spectrogram.Colormaps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Spectrogram.Tests
{
    class ColormapExamples
    {
        [Test]
        public void Test_Make_CommonColormaps()
        {
            (int sampleRate, double[] audio) = WavFile.ReadMono("../../../../../data/cant-do-that-44100.wav");
            int fftSize = 1 << 12;
            var spec = new SGram(sampleRate, fftSize, stepSize: 700, maxFreq: 2000);
            spec.SetWindow(FftSharp.Window.Hanning(fftSize / 3)); // sharper window than typical
            spec.Add(audio);

            // delete old colormap files
            foreach (var filePath in System.IO.Directory.GetFiles("../../../../../dev/graphics/", "hal-*.png"))
                System.IO.File.Delete(filePath);

            foreach (var cmap in Colormap.GetColormaps())
            {
                spec.SetColormap(cmap);
                spec.SaveImage($"../../../../../dev/graphics/hal-{cmap.Name}.png", intensity: .5);
                Debug.WriteLine($"![](dev/graphics/hal-{cmap.Name}.png)");
            }
        }

        [Test]
        public void Test_Colormaps_ByName()
        {
            string[] names = Colormap.GetColormapNames();
            Console.WriteLine(string.Join(", ", names));

            Colormap viridisCmap = Colormap.GetColormap("viridis");
            Assert.AreEqual("Viridis", viridisCmap.Name);
        }
    }
}
