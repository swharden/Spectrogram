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

        public static float[] FFT(float[] values, WindowFunction window = WindowFunction.hanning, bool decibels = false)
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

            NAudio.Dsp.FastFourierTransform.FFT(true, (int)Math.Log(fftSize, 2.0), fft_buffer);

            float[] fft = new float[fftSize / 2];
            for (int i = 0; i < fft.Length; i++)
            {
                var fftL = fft_buffer[i];
                var fftR = fft_buffer[fft_buffer.Length - i - 1];

                // note that this is different than just taking the absolute value
                float absL = (float)Math.Sqrt(fftL.X * fftL.X + fftL.Y * fftL.Y);
                float absR = (float)Math.Sqrt(fftR.X * fftR.X + fftR.Y * fftR.Y);

                fft[i] = (absL + absR) / 2;

                if (decibels)
                    fft[i] = (float)(Math.Log(fft[i]) * 20);
            }

            return fft;
        }

    }
}
