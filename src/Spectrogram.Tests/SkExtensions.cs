using SkiaSharp;

namespace Spectrogram.Tests;

internal static class SkExtensions
{
    internal static void SaveTo(this SKBitmap bitmap, string fileName, SKEncodedImageFormat format, int quality = 100)
    {
        using var data = bitmap.Encode(format, quality);
        using var stream = System.IO.File.OpenWrite(fileName);
        data.SaveTo(stream);
    }
}