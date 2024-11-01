using FluentAssertions;
using NUnit.Framework;

namespace Spectrogram.Tests
{
    class AudioFileTests
    {
        /// <summary>
        /// Compare values read from the WAV reader against those read by Python's SciPy module (see script in /dev folder)
        /// </summary>
        [TestCase("cant-do-that-44100.wav", 44_100, 166_671, 1)]
        [TestCase("03-02-03-01-02-01-19.wav", 48_000, 214_615, 1)]
        [TestCase("qrss-10min.wav", 6_000, 3_600_000, 1)]
        [TestCase("cant-do-that-11025-stereo.wav", 11_025, 41668, 2)]
        [TestCase("asehgal-original.wav", 40_000, 1_600_000, 1)]
        public void Test_AudioFile_LengthAndSampleRate(string filename, int knownRate, int knownLength, int channels)
        {
            string filePath = $"../../../../../data/{filename}";
            (double[] audio, int sampleRate) = AudioFile.ReadWAV(filePath);

            sampleRate.Should().Be(knownRate);
            (audio.Length / channels).Should().Be(knownLength);
        }
    }
}
