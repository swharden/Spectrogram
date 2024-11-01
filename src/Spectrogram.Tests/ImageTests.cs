using NUnit.Framework;
using SkiaSharp;

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

        SKBitmap bmp1 = sg.GetBitmap(rotate: false); 
        bmp1.SaveTo("test-image-original.png", SKEncodedImageFormat.Png);

        SKBitmap bmp2 = sg.GetBitmap(rotate: true);
        bmp2.SaveTo("test-image-rotated.png", SKEncodedImageFormat.Png);
    }
}
