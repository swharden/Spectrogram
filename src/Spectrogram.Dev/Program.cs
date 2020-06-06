using System;
using System.Drawing;

namespace Spectrogram.Dev
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] audio = Read.WavInt16mono("../../../../../data/qrss-10min.wav");
            Console.WriteLine("loaded");

            int sampleRate = 6000;
            int fftSize = 1 << 15;
            int stepSize = (audio.Length / 1000);

            var spec = new Spectrogram(sampleRate, fftSize, stepSize, 1200, 1300);
            spec.Add(audio);
            spec.Process();
            Bitmap bmp = spec.GetBitmap();
            bmp.Save("test.bmp");
        }
    }
}
