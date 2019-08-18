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
            var spec = new Spectrogram.Spectrogram(fftSize: 2048, stepSize: 500, 
                pixelUpper: 300, intensity: 2);
            float[] values = Spectrogram.WavFile.Read("mozart.wav");
            spec.Add(values);
            spec.SaveBitmap("mozart.jpg");
        }
    }
}
