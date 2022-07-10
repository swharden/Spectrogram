using NUnit.Framework;

namespace Spectrogram.Tests;

internal class ImageTests
{
    [Test]
    public void Test_Image_Rotations()
    {
        string filePath = $"../../../../../data/cant-do-that-44100.wav";
        (double[] audio, int sampleRate) = AudioFile.ReadWAV(filePath);
        SpectrogramGenerator sg = new(sampleRate, 4096, 500, maxFreq: 3000);
        sg.Add(audio);

        System.Drawing.Bitmap bmp1 = sg.GetBitmap(rotate: false);
        bmp1.Save("test-image-original.png");

        System.Drawing.Bitmap bmp2 = sg.GetBitmap(rotate: true);
        bmp2.Save("test-image-rotated.png");
    }
}
