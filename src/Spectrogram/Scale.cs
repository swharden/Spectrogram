using SkiaSharp;
using System.Collections.Generic;

namespace Spectrogram;

static class Scale
{
    public static SKBitmap Vertical(int width, Settings settings, int offsetHz = 0, int tickSize = 3, int reduction = 1)
    {
        double tickHz = 1;
        int minSpacingPx = 50;
        double[] multipliers = { 2, 2.5, 2 };
        int multiplier = 0;
        
        while (true)
        {
            tickHz *= multipliers[multiplier++ % multipliers.Length];
            double tickCount = settings.FreqSpan / tickHz;
            double pxBetweenTicks = settings.Height / tickCount;
            if (pxBetweenTicks >= minSpacingPx * reduction)
                break;
        }

        var imageInfo = new SKImageInfo(width, settings.Height / reduction, SKColorType.Rgba8888);
        var bitmap = new SKBitmap(imageInfo);
        using var canvas = new SKCanvas(bitmap);
        canvas.Clear(SKColors.White);

        var paint = new SKPaint
        {
            Color = SKColors.Black,
            TextSize = 10,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Monospace")
        };
            
        List<double> freqs = new List<double>();
        for (double f = settings.FreqMin; f <= settings.FreqMax; f += tickHz)
            freqs.Add(f);

        if (freqs.Count >= 2)
        {
            freqs.RemoveAt(0);
            freqs.RemoveAt(freqs.Count - 1);
        }

        foreach (var freq in freqs)
        {
            int y = settings.PixelY(freq) / reduction;
            canvas.DrawLine(0, y, tickSize, y, paint);

            var text = $"{freq + offsetHz:N0} Hz";
            canvas.DrawText(text, tickSize + 2, y + 5, paint);
        }

        return bitmap;
    }
}
