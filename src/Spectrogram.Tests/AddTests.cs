using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrogram.Tests
{
    internal class AddTests
    {
        [Test]
        public void Test_No_Data()
        {
            SpectrogramGenerator sg = new(44100, 2048, 1000);
            Assert.Throws<InvalidOperationException>(() => sg.GetBitmap());
        }
    }
}
