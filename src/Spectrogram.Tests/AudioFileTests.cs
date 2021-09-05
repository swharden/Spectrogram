using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram.Tests
{
    class AudioFileTests
    {
        /// <summary>
        /// Compare values read from the WAV reader against those read by Python's SciPy module (see script in /dev folder)
        /// </summary>
        [Test]
        public void Test_AudioFile_KnownValues()
        {
            (double[] audio, int sampleRate) = AudioFile.ReadWAV("../../../../../data/cant-do-that-44100.wav", multiplier: 32_000);

            Assert.AreEqual(44100, sampleRate);
            Assert.AreEqual(166671, audio.Length);
            Assert.AreEqual(4435, audio[12345], 1000);
        }
    }
}
