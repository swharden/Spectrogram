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
            //DemoQRSS();
        }

        static void DemoMozart()
        {
            using (var benchmark = new Spectrogram.Benchmark())
            {
                var spec = new Spectrogram.Spectrogram(sampleRate: 8000, fftSize: 2048);

                float[] values = Spectrogram.WavFile.Read("mozart.wav");
                spec.AddExtend(values);

                Bitmap bmp = spec.GetBitmap(frequencyMax: 2500, intensity: 5);
                spec.SaveBitmap(bmp, "mozart.jpg");
            }
        }

        static void DemoQRSS()
        {
            using (var benchmark = new Spectrogram.Benchmark())
            {
                var spec = new Spectrogram.Spectrogram(sampleRate: 8000, fftSize: 16384, segmentSize: 5000);

                float[] values = Spectrogram.WavFile.Read("qrss.wav");
                spec.AddExtend(values);
                Bitmap bmp = spec.GetBitmap(intensity: 2, frequencyMin: 1200, frequencyMax: 1500);
                spec.SaveBitmap(bmp, "qrss.jpg");
            }
        }
    }
}
