using System;
using System.Drawing;

namespace Spectrogram.Dev
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] audio = Read.WavInt16mono("../../../../../data/qrss-10min.wav");
            double[] window = FftSharp.Window.Hanning(1 << 14);

            //Test_1x(audio, 6000, window);
            //Test_4x(audio, 6000, window);
        }

        static void Test_1x(double[] audio, int sampleRate, double[] window)
        {
            int fftSize = 1 << 15;
            int stepSize = audio.Length / 1000;

            var spec = new Spectrogram(sampleRate, fftSize, stepSize, 1200, 1300);
            spec.SetWindow(window);

            spec.Add(audio);
            spec.Process();
            Bitmap bmp = spec.GetBitmap(intensity: 2);
            bmp.Save("test-1x.bmp");
        }

        static void Test_4x(double[] audio, int sampleRate, double[] window)
        {
            int fftSize = 1 << 17;
            int stepSize = audio.Length / 1000;

            var spec = new Spectrogram(sampleRate, fftSize, stepSize, 1200, 1300);
            spec.SetWindow(window);

            spec.Add(audio);
            spec.Process();
            Bitmap bmp = spec.GetBitmapMax(intensity: 2 * 3, reduction: 4);
            bmp.Save("test-4x.bmp");
        }
    }
}
