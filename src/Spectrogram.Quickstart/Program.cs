using Spectrogram.Colormaps;
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
            Console.WriteLine($"Saving output in: {System.IO.Path.GetFullPath("./")}");

            // Get PCM audio values by decoding a MP3 file
            string audioFilePath = "../../../../../data/cant-do-that.mp3";
            (double[] audio, int sampleRate) = Read.MP3(audioFilePath);

            // Load the data and save the result of the initial calculation
            var spec = new Spectrogram(audio, sampleRate, 
                fftSize: 4096, stepSize: 500, freqMax: 2_500, 
                multiplier: 1.5, dB: false);
            spec.SaveJPG("output.jpg");

            // Save a new image using recalculated pixel values
            spec.Recalculate(multiplier: 5, dB: true, cmap: new Colormaps.Grayscale());
            spec.SaveJPG("output2.jpg");
        }
    }
}
