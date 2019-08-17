using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrogram
{

    public static class Operations
    {
        public enum WindowFunction { none, hanning, triangle };

        public static bool IsPowerOfTwo(int number)
        {
            for (int i = 0; i < 20; i++)
            {
                int powerOfTwo = (1 << i);
                if (number == powerOfTwo)
                    return true;
                else if (powerOfTwo > number)
                    return false;
            }
            return false;
        }

        public static double TriangleWindow(int n, int frameSize)
        {
            int pointsFromCenter = Math.Abs(frameSize / 2 - n);
            int pointsFromEdge = frameSize / 2 - pointsFromCenter;
            double fractionFromEdge = (double)pointsFromEdge / (frameSize / 2);
            return fractionFromEdge;
        }

        public static float[] FFT(float[] values, WindowFunction window = WindowFunction.triangle, bool decibels = true, bool plotOutput = false)
        {
            int fftSize = values.Length;
            if (!IsPowerOfTwo(fftSize))
                throw new ArgumentException("FFT Size must be a power of 2");

            NAudio.Dsp.Complex[] fft_buffer = new NAudio.Dsp.Complex[fftSize];
            for (int i = 0; i < fftSize; i++)
            {
                fft_buffer[i].X = (float)values[i];
                fft_buffer[i].Y = 0;

                switch (window)
                {
                    case WindowFunction.none:
                        break;
                    case WindowFunction.hanning:
                        fft_buffer[i].X *= (float)NAudio.Dsp.FastFourierTransform.HammingWindow(i, fftSize);
                        break;
                    case WindowFunction.triangle:
                        fft_buffer[i].X *= (float)TriangleWindow(i, fftSize);
                        break;
                    default:
                        throw new NotImplementedException("unsupported window function");
                }
            }

            if (plotOutput)
            {
                float[] justX = new float[fft_buffer.Length];
                for (int i = 0; i < justX.Length; i++)
                    justX[i] = fft_buffer[i].X;
                Reports.plot(justX, "windowed.png");
            }

            // perform the FFT
            NAudio.Dsp.FastFourierTransform.FFT(true, (int)Math.Log(fftSize, 2.0), fft_buffer);

            // a list with FFT values
            float[] fft = new float[fftSize / 2];

            for (int i = 0; i < fft.Length; i++)
            {
                // should this be sqrt(X^2+Y^2)? log10?

                var fftPointLeft = fft_buffer[i];
                var fftPointRight = fft_buffer[fft_buffer.Length - i - 1];

                fft[i] = 0;
                fft[i] += fftPointLeft.X + fftPointLeft.Y;
                fft[i] += fftPointRight.X + fftPointRight.Y;
                fft[i] /= 2;

                fft[i] = Math.Abs(fft[i]);

                if (decibels)
                    fft[i] = (float)(Math.Log(fft[i]) * 20);
            }

            return fft;
        }

    }
}
