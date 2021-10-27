using NUnit.Framework;
using System;
using System.Diagnostics;

namespace Spectrogram.Tests
{
    class ColormapExamples
    {
        [Test]
        public void Test_Make_CommonColormaps()
        {
            (double[] audio, int sampleRate) = AudioFile.ReadWAV("../../../../../data/cant-do-that-44100.wav");
            int fftSize = 1 << 12;
            var spec = new SpectrogramGenerator(sampleRate, fftSize, stepSize: 700, maxFreq: 2000);
            var window = new FftSharp.Windows.Hanning();
            spec.SetWindow(window.Create(fftSize / 3)); // sharper window than typical
            spec.Add(audio);

            // delete old colormap files
            foreach (var filePath in System.IO.Directory.GetFiles("../../../../../dev/graphics/", "hal-*.png"))
                System.IO.File.Delete(filePath);

            foreach (var cmap in Colormap.GetColormaps())
            {
                spec.Colormap = cmap;
                spec.SaveImage($"../../../../../dev/graphics/hal-{cmap.Name}.png");
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
