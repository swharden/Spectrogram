using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //DemoMozart();
            DemoQRSS();
        }

        static void DemoMozart()
        {
            using (var benchmark = new Spectrogram.Benchmark())
            {
                var spec = new Spectrogram.Spectrogram(
                    fftSize: 2048, 
                    stepSize: 500,
                    pixelUpper: 300, 
                    intensity: 2
                    );
                float[] values = Spectrogram.WavFile.Read("mozart.wav");
                spec.Add(values);
                spec.SaveImage("mozart.jpg");
            }
        }

        static void DemoQRSS()
        {
            using (var benchmark = new Spectrogram.Benchmark())
            {
                var spec = new Spectrogram.Spectrogram(
                    fftSize: 8192, 
                    stepSize: 5000,
                    intensity: 2, 
                    pixelLower: 1250, 
                    pixelUpper: 1500
                    );
                float[] values = Spectrogram.WavFile.Read("qrss.wav");
                spec.Add(values);
                spec.SaveImage("qrss.jpg");
            }
        }
    }
}
