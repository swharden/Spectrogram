using NUnit.Framework;
using System;
using SkiaSharp;

namespace Spectrogram.Tests
{
    class Mel
    {
        [Test]
        public void Test_MelSpectrogram_MelScale()
        {
            (double[] audio, int sampleRate) = AudioFile.ReadWAV("../../../../../data/cant-do-that-44100.wav");
            int fftSize = 4096;
            var sg = new SpectrogramGenerator(sampleRate, fftSize, stepSize: 500);
            sg.Add(audio);

            // Ottieni l'immagine Mel-scaled come SKBitmap
            SKBitmap bmpMel = sg.GetBitmapMel(250);  // Presuppone che sg abbia un metodo GetSKBitmapMel
            using (var image = SKImage.FromBitmap(bmpMel))
            using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
            {
                // Salva l'immagine Mel-scaled
                using (var stream = System.IO.File.OpenWrite("../../../../../dev/graphics/halMel-MelScale.png"))
                {
                    data.SaveTo(stream);
                }
            }

            // Ottieni l'immagine originale come SKBitmap
            SKBitmap bmpRaw = sg.GetBitmap();  // Presuppone che sg abbia un metodo GetSKBitmap
            SKBitmap bmpCropped = new SKBitmap(bmpRaw.Width, bmpMel.Height);

            // Disegna bmpRaw su bmpCropped usando SKCanvas
            using (var canvas = new SKCanvas(bmpCropped))
            {
                canvas.Clear(SKColors.Transparent);
                canvas.DrawBitmap(bmpRaw, new SKRect(0, bmpMel.Height - bmpRaw.Height, bmpRaw.Width, bmpMel.Height));
            }

            using (var imageCropped = SKImage.FromBitmap(bmpCropped))
            using (var dataCropped = imageCropped.Encode(SKEncodedImageFormat.Png, 100))
            {
                // Salva l'immagine croppata
                using (var streamCropped = System.IO.File.OpenWrite("../../../../../dev/graphics/halMel-LinearCropped.png"))
                {
                    dataCropped.SaveTo(streamCropped);
                }
            }
        }

        [Test]
        public void Test_Mel_Graph()
        {
            int specPoints = 4096;
            double maxFreq = 50_000;
            double maxMel = 2595 * Math.Log10(1 + maxFreq / 700);

            Random rand = new Random(1);
            double[] freq = ScottPlot.Generate.Consecutive(specPoints, maxFreq / specPoints);
            double[] power = ScottPlot.Generate.RandomWalk(specPoints, .02, .5);

            var plt1 = new ScottPlot.Plot();
            plt1.Add.ScatterLine(freq, power);

            int filterSize = 25;

            // generate scales
            double[] pointsLinear = new double[filterSize + 1];
            double[] pointsMel = new double[filterSize + 1];
            for (int i = 0; i < filterSize + 1; i++)
            {
                double thisFreq = maxFreq * i / filterSize;
                double thisMel = maxMel * i / filterSize;
                pointsLinear[i] = thisFreq;
                pointsMel[i] = 700 * (Math.Pow(10, thisMel / 2595d) - 1);
            }

            // draw rectangles
            double[] binStartFreqs = pointsMel;
            for (int binIndex = 0; binIndex < binStartFreqs.Length - 2; binIndex++)
            {
                double freqLow = binStartFreqs[binIndex];
                double freqCenter = binStartFreqs[binIndex + 1];
                double freqHigh = binStartFreqs[binIndex + 2];

                double[] xs = [freqLow, freqCenter, freqHigh];
                double[] ys = [0, 1, 0];
                var sctr = plt1.Add.ScatterLine(xs, ys);

                int indexLow = (int)(specPoints * freqLow / maxFreq);
                int indexHigh = (int)(specPoints * freqHigh / maxFreq);
                int indexSpan = indexHigh - indexLow;
                Console.WriteLine($"bin {binIndex}: [{freqLow} Hz - {freqHigh} Hz] = [{indexLow}:{indexHigh}]");

                double binValue = 0;
                double binScaleSum = 0;
                for (int i = 0; i < indexSpan; i++)
                {
                    double frac = (double)i / indexSpan;
                    frac = (frac < .5) ? frac * 2 : 1 - frac;
                    binScaleSum += frac;
                    binValue += power[indexLow + i] * frac;
                }
                binValue /= binScaleSum;
                plt1.Add.Marker(freqCenter, binValue, ScottPlot.MarkerShape.FilledCircle, 10, sctr.Color);
            }

            plt1.SavePng("mel1.png", 800, 300);
        }

        [Test]
        public void Test_SaveEmpty_Throws()
        {
            (double[] audio, int sampleRate) = AudioFile.ReadWAV("../../../../../data/cant-do-that-44100.wav");
            int fftSize = 4096;
            var spec = new SpectrogramGenerator(sampleRate, fftSize, stepSize: 500);
            //spec.Add(audio);
            Assert.Throws<InvalidOperationException>(() => { spec.SaveImage("empty.png"); });
        }
    }
}
