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
            DemoTiming();
        }

        static void DemoTiming()
        {
            var spec = new Spectrogram.Spectrogram(sampleRate: 8000, fftSize: 2048);

            float[] values = Spectrogram.WavFile.Read("mozart.wav");
            spec.Add(values);

            Console.WriteLine(spec);
            Console.WriteLine(spec.GetConfigDetails());
        }

        static void DemoMozart()
        {
            using (var benchmark = new Spectrogram.Benchmark())
            {
                var spec = new Spectrogram.Spectrogram(sampleRate: 8000, fftSize: 2048);

                float[] values = Spectrogram.WavFile.Read("mozart.wav");
                spec.Add(values);

                Bitmap bmp = spec.GetBitmap();
                spec.SaveBitmap(bmp, "mozart.jpg");
            }
        }

        static void DemoQRSS()
        {
            using (var benchmark = new Spectrogram.Benchmark())
            {
                var spec = new Spectrogram.Spectrogram(sampleRate: 8000, fftSize: 8192*2);

                float[] values = Spectrogram.WavFile.Read("qrss.wav");
                spec.Add(values, stepSize: spec.fftSize/4);

                Bitmap bmp = spec.GetBitmap(intensity: 2,
                    pixelLower: spec.GetFftIndex(1200),
                    pixelUpper: spec.GetFftIndex(1500)
                    );
                spec.SaveBitmap(bmp, "qrss.jpg");
            }
        }
    }
}
