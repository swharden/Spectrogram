using NuGet.Frameworks;
using NUnit.Framework;
using Spectrogram.Colormaps;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram.Tests
{
    class ColormapTests
    {
        [Test]
        public void Test_Colormap_ExtendedFractionsReturnEdgeValues()
        {
            var cmap = Colormap.GetColormap();

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
        public void Test_Colormap_EveryColorIsImplementedAndUnique()
        {
            Colormap.Name[] cmapNames = (Colormap.Name[])Enum.GetValues(typeof(Colormap.Name));

            byte pixelIntensity = 123;
            List<int> colorValuesSeen = new List<int>();

            foreach (Colormap.Name cmapName in cmapNames)
            {
                Colormaps.Colormap cmap = Colormap.GetColormap(cmapName);
                int cmapInt = cmap.GetInt32(pixelIntensity);
                var cmapRGB = cmap.GetRGB(pixelIntensity);

                Console.WriteLine($"{cmap}: value 123 RGB={cmapRGB}, Int32={cmapInt}");

                Assert.That(colorValuesSeen, Has.No.Member(cmapInt));
                colorValuesSeen.Add(cmapInt);
            }
        }

        [Test]
        public void Test_Colormap_IntegerColorsMatchRGBColors()
        {
            Colormap.Name[] cmapNames = (Colormap.Name[])Enum.GetValues(typeof(Colormap.Name));

            byte pixelIntensity = 123;

            foreach (Colormap.Name cmapName in cmapNames)
            {
                Colormaps.Colormap cmap = Colormap.GetColormap(cmapName);

                var (r, g, b) = cmap.GetRGB(pixelIntensity);
                var colorFromRGB = System.Drawing.Color.FromArgb(255, r, g, b);

                int cmapInt = cmap.GetInt32(pixelIntensity);
                var colorFromInt = System.Drawing.Color.FromArgb(cmapInt);

                Console.WriteLine($"{cmap}: value 123 RGB={colorFromRGB}, Int32={cmapInt}");

                Assert.AreEqual(colorFromRGB, colorFromInt);
            }
        }
    }
}
