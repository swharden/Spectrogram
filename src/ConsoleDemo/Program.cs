using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoMozart();
            DemoQRSS();
        }

        static void DemoMozart()
        {
            var spec = new Spectrogram.Spectrogram(sampleRate: 8000, fftSize: 2048, step: 700);
            float[] values = Spectrogram.Tools.ReadWav("mozart.wav");
            spec.AddExtend(values);
            Bitmap bmp = spec.GetBitmap(intensity: 2, freqHigh: 2500);
            spec.SaveBitmap(bmp, "mozart.jpg");
        }

        static void DemoQRSS()
        {
            var spec = new Spectrogram.Spectrogram(sampleRate: 8000, fftSize: 16384, step: 8000);
            float[] values = Spectrogram.Tools.ReadMp3("qrss-w4hbk.mp3");
            spec.AddExtend(values);
            Bitmap bmp = spec.GetBitmap(intensity: 1.5, freqLow: 1100, freqHigh: 1500,
                showTicks: true, tickSpacingHz: 50, tickSpacingSec: 60);
            spec.SaveBitmap(bmp, "qrss.png");
        }
    }
}
