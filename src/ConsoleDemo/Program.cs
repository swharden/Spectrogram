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
            var spec = new Spectrogram.Spectrogram(stepSize: 100);

            float[] values = Spectrogram.WavFile.Read("mozart.wav");

            float[] valuesSmaller = new float[8000 * 10];
            Array.Copy(values, 0, valuesSmaller, 0, valuesSmaller.Length);

            spec.Add(values);
            spec.SaveBitmap("mozart.png");
        }
    }
}
