using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram
{
    public static class WavFile
    {
        private static string FindFile(string filePath)
        {
            // look for it in this folder
            filePath = System.IO.Path.GetFullPath(filePath);
            if (System.IO.File.Exists(filePath))
                return filePath;

            // look for it in the package data folder
            string fileName = System.IO.Path.GetFileName(filePath);
            string fileFolder = System.IO.Path.GetDirectoryName(filePath);
            fileFolder = System.IO.Path.GetFullPath(fileFolder+"/../../../../data/");
            filePath = System.IO.Path.Combine(fileFolder, fileName);
            Console.WriteLine($"looking in {filePath}");
            if (System.IO.File.Exists(filePath))
                return filePath;
            
            // give up
            return null;
        }

        public static float[] Read(string wavFilePath, int? sampleLimit = null)
        {
            // quick and drity WAV file reader (16-bit signed format)
            string actualPath = FindFile(wavFilePath);
            if (actualPath == null)
                throw new ArgumentException("file not found: " + actualPath);
            byte[] bytes = System.IO.File.ReadAllBytes(actualPath);
            int sampleCount = bytes.Length / 2;
            if (sampleLimit != null)
                sampleCount = Math.Min(sampleCount, (int)sampleLimit);
            float[] pcm = new float[sampleCount];
            int firstDataByte = 44;
            for (int i = firstDataByte; i < bytes.Length - 2; i += 2)
            {
                if (i / 2 >= pcm.Length)
                    break;
                pcm[i / 2] = BitConverter.ToInt16(bytes, i);
            }
            return pcm;
        }
    }
}
