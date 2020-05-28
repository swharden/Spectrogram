using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;

namespace Spectrogram.Quickstart
{
    class Program
    {
        static void Main(string[] args)
        {
            Quickstart();
        }

        static void Quickstart()
        {
            //string audioFilePath = "../../../../../data/Handel - Air and Variations.mp3";
            string audioFilePath = "../../../../../data/cant-do-that.mp3";

            Console.WriteLine("loaing audio file...");
            (double[] audio, int sampleRate) = Read.MP3(audioFilePath);

            Console.WriteLine("computing FFTs...");
            var spec = new Spectrogram(fftSize: 4096, sampleRate, freqMax: 5_000);
            spec.AddSignal(audio);
            spec.ProcessAll(stepSize: 4096 / 8);

            Console.WriteLine("converting PSDs to Image...");
            spec.SaveJPG("test123.jpg", 7, 0);

            Console.WriteLine($"Output in: {System.IO.Path.GetFullPath("./")}");
        }
    }
}
