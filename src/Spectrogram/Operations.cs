using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrogram
{

    public static class Operations
    {
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
        
        public static float[] FFT(float[] values, bool useHammingWindow = true)
        {
            int fftSize = values.Length;
            if (!IsPowerOfTwo(fftSize))
                throw new ArgumentException("FFT Size must be a power of 2");

            NAudio.Dsp.Complex[] fft_buffer = new NAudio.Dsp.Complex[fftSize];
            for (int i = 0; i < fftSize; i++)
            {
                fft_buffer[i].X = (float)values[i];
                fft_buffer[i].Y = 0;

                if (useHammingWindow)
                    fft_buffer[i].X *= (float)NAudio.Dsp.FastFourierTransform.HammingWindow(i, fftSize);
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
            }

            return fft;
        }

    }
}
