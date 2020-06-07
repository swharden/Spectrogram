using NuGet.Frameworks;
using NUnit.Framework;
using Spectrogram.Colormaps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Spectrogram.Tests
{
    class ColormapTests
    {
        [Test]
        public void Test_Colormap_ExtendedFractionsReturnEdgeValues()
        {
            var cmap = Colormap.Viridis;

            Random rand = new Random(0);
            for (double frac = -3; frac < 3; frac += rand.NextDouble() * .2)
            {
                Console.WriteLine($"{frac}: {cmap.GetRGB(frac)}");

                if (frac <= 0)
                    Assert.AreEqual(cmap.GetRGB(0), cmap.GetRGB(frac));

                if (frac >= 1)
                    Assert.AreEqual(cmap.GetRGB(1.0), cmap.GetRGB(frac));
            }
        }

        [Test]
        public void Test_Colormap_IntegerMatchesRGBColors()
        {
            Colormap cmap = Colormap.Viridis;

            byte pixelIntensity = 123;
            var (r, g, b) = cmap.GetRGB(pixelIntensity);
            int int32 = cmap.GetInt32(pixelIntensity);

            Color color1 = Color.FromArgb(255, r, g, b);
            Color color2 = Color.FromArgb(int32);
            Color color3 = cmap.GetColor(pixelIntensity);

            Assert.AreEqual(color1, color2);
            Assert.AreEqual(color1, color3);
        }
    }
}
